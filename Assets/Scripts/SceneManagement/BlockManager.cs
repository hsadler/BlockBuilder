using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using UnityEngine;

public class BlockManager : MonoBehaviour
{

	// DATASTORE AND SERVICE FOR MANAGING GAME BLOCKS


	public IDictionary<string, Block> coordsToBlockDict = new Dictionary<string, Block>();
	public GameObject blocksContainer;

	private bool evalRunning = false;


	// the static reference to the singleton instance
	public static BlockManager instance { get; private set; }

	void Awake() {
		// singleton pattern
		if(instance == null) {
			instance = this;
		} else {
			Destroy(gameObject);
		}
	}

	void Start() {
		InvokeRepeating(
			"BlockTickEval",
			0,
			SceneConfig.instance.tickDurationSeconds
		);
	}

	void Update() {}

	public List<Block> GetBlocksAsList() {
		return coordsToBlockDict.Values.ToList();
	}

	// BLOCK EVALUATION METHODS

	private void BlockTickEval() {
		if(!evalRunning) {
			// commit the mutations from the last evaluation
			CommitBlockMutationsToUI();
			// do async block mutation evaluations
			Thread workerThread = new Thread(AsyncBlockEval);
			workerThread.Start();
		} else {
			Debug.Log("Skipped block evaluations at tick since one is still running");
		}
	}

	private void AsyncBlockEval() {
		evalRunning = true;
		List<Block> blockList = GetBlocksAsList();
		foreach (Block block in blockList) {
			block.script.AsyncEvalInteractions();
		}
		foreach (Block block in blockList) {
			block.script.AsyncValidateMutations();
		}
		foreach (Block block in blockList) {
			block.script.AsyncCommitMutations();
		}
		evalRunning = false;
	}

	private void CommitBlockMutationsToUI() {
		foreach (Block block in GetBlocksAsList()) {
			block.script.CommitBlockStateToUI();
		}
	}

	// BLOCK DATASTORE METHODS

	public string GetFormattedCoordinateFromBlockState(BlockState blockState) {
		string formattedCoordinatesString = blockState.position.ToString();
		return formattedCoordinatesString;
	}

	public string GetFormattedCoordinateString(Vector3 coordinates) {
		string formattedCoordinatesString = coordinates.ToString();
		return formattedCoordinatesString;
	}

	public bool BlockExists(Vector3 coordinates) {
		string formattedCoords = GetFormattedCoordinateString(coordinates);
		return coordsToBlockDict.ContainsKey(formattedCoords);
	}

	public Block GetBlock(Vector3 coordinates) {
		Block block = null;
		if(BlockExists(coordinates)) {
			string formattedCoords = GetFormattedCoordinateString(coordinates);
			block = coordsToBlockDict[formattedCoords];
		} else {
			Debug.Log("block not found at coordinate x:" +
				coordinates[0] + " y:" + coordinates[1] + " z:" + coordinates[2]);
		}
		return block;
	}

	public bool SetBlock(Block block) {
		// add to coordinates->block dictionary
		BlockState bs = block.script.blockState;
		string coordsKey = GetFormattedCoordinateFromBlockState(bs);
		coordsToBlockDict.Add(coordsKey, block);
		return true;
	}

	public bool UnsetBlock(Block block) {
		// remove from coordinates->block dictionary if possible
		BlockState bs = block.script.blockState;
		if(BlockExists(bs.position)) {
			string formattedCoords = GetFormattedCoordinateFromBlockState(bs);
			coordsToBlockDict.Remove(formattedCoords);
			return true;
		} else {
			string formattedCoords = GetFormattedCoordinateFromBlockState(bs);
			Debug.Log("Unable to unset block at formatted coordinates: " + formattedCoords);
			Debug.Log("CoordsToBlockDict: " + coordsToBlockDict.ToString());
			return false;
		}
	}

	public Block CreateBlock(
		GameObject blockPrefab,
		Vector3 position,
		Quaternion rotation
	) {
		// round position and rotation
		position = Functions.RoundVector3ToDiscrete(position);
		rotation = Functions.RoundQuaternionToDiscrete(rotation);
		// create block from prefab and register on block manager
		if(!BlockExists(position)) {
			GameObject newBlockGO = Instantiate(
				blockPrefab,
				position,
				rotation,
				blocksContainer.transform
			);
			BaseBlockScript newBlockScript = newBlockGO.GetComponent<BaseBlockScript>();
			Block newBlock = new Block(
				newBlockGO,
				newBlockScript
			);
			newBlockScript.SetSelfBlock(newBlock);
			SetBlock(newBlock);
			newBlockScript.OnPlacement();
			return newBlock;
		}
		return null;
	}

	public void ClearAllBlocks() {
		foreach (Block b in GetBlocksAsList()) {
			b.script.DestroyBlock();
		}
	}


}
