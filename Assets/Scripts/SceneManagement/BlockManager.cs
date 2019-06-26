using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using UnityEngine;

public class BlockManager : MonoBehaviour
{

	// DATASTORE AND SERVICE FOR MANAGING GAME BLOCKS


	public IDictionary<string, Block> blockDict = new Dictionary<string, Block>();
	public IDictionary<string, Block> onDeckBlockDict = new Dictionary<string, Block>();
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
		return blockDict.Values.ToList();
	}

	// BLOCK EVALUATION METHODS

	private void BlockTickEval() {
		if(!evalRunning) {
			// Commit the mutations from the last evaluation using 
			// lastBlockState and blockState
			CommitBlockMutationsToUI();
			// Do async block mutation evaluations
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
			// Registers block state mutations per block.
			block.script.AsyncEvalInteractions();
		}
		foreach (Block block in blockList) {
			// Validates aggregated block state mutations and puts them on-deck.
			block.script.AsyncValidateMutations();
		}
		// Validation is done, init on-deck block dict
		InitOnDeckBlockDict();
		foreach (Block block in blockList) {
			// Commits the on-deck mutations to the block states. 
			// Also handles updates to the Block Manager dictionary.
			block.script.AsyncCommitMutations();
		}
		evalRunning = false;
	}

	private void CommitBlockMutationsToUI() {
		foreach (Block block in GetBlocksAsList()) {
			block.script.CommitBlockStateToUI();
		}
	}

	// FORMATTERS

	public string GetFormattedCoordinateFromBlockState(BlockState blockState) {
		string formattedCoordinatesString = blockState.position.ToString();
		return formattedCoordinatesString;
	}

	public string GetFormattedCoordinateString(Vector3 coordinates) {
		string formattedCoordinatesString = coordinates.ToString();
		return formattedCoordinatesString;
	}

	// BLOCK DICT DATASTORE METHODS
	
	public bool BlockExists(Vector3 coordinates) {
		string formattedCoords = GetFormattedCoordinateString(coordinates);
		return blockDict.ContainsKey(formattedCoords);
	}

	public Block GetBlock(Vector3 coordinates) {
		Block block = null;
		if(BlockExists(coordinates)) {
			string formattedCoords = GetFormattedCoordinateString(coordinates);
			block = blockDict[formattedCoords];
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
		blockDict.Add(coordsKey, block);
		return true;
	}

	public bool UnsetBlock(Block block) {
		// remove from coordinates->block dictionary if possible
		BlockState bs = block.script.blockState;
		if(BlockExists(bs.position)) {
			string formattedCoords = GetFormattedCoordinateFromBlockState(bs);
			blockDict.Remove(formattedCoords);
			return true;
		} else {
			string formattedCoords = GetFormattedCoordinateFromBlockState(bs);
			Debug.Log("Unable to unset block at formatted coordinates: " + formattedCoords);
			Debug.Log("blockDict: " + blockDict.ToString());
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

	// ON-DECK BLOCK DICT DATASTORE METHODS

	public void InitOnDeckBlockDict() {
		onDeckBlockDict = new Dictionary<string, Block>();
	}

	public bool SetBlockOnOnDeckBlockDict(Block block) {
		// add to coordinates->on-deck block dictionary
		BlockState onDeckBlockState = block.script.onDeckBlockState;
		string coordsKey = GetFormattedCoordinateFromBlockState(onDeckBlockState);
		onDeckBlockDict.Add(coordsKey, block);
		return true;
	} 

	public bool BlockExistsOnOnDeckBlockDict(Vector3 coordinates) {
		string formattedCoords = GetFormattedCoordinateString(coordinates);
		return onDeckBlockDict.ContainsKey(formattedCoords);
	} 


}
