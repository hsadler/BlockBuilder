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
	public GameObject ghostBlock;
	public float ghostBlockRotateDuration = 0.25f;


	private bool evalRunning = false;


	// the static reference to the singleton instance
	public static BlockManager instance { get; private set; }

	void Awake() {
		// singleton pattern
		if(instance == null) {
			instance = this;
			// DontDestroyOnLoad(gameObject);
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
			CommitBlockMutations();
			// CODE EXAMPLE: multithreading
			// do async block mutation evaluations
			Thread workerThread = new Thread(AsyncBlockEval);
			workerThread.Start();
		} else {
			Debug.Log("Skipped block evaluations at tick since one is still running");
		}
	}

	private void AsyncBlockEval() {
		evalRunning = true;
		List<Block> blocks = new List<Block>(coordsToBlockDict.Values);
		foreach (Block block in blocks) {
			block.script.EvaluateAtTick();
		}
		evalRunning = false;
	}

	private void CommitBlockMutations() {
		List<Block> blocks = new List<Block>(coordsToBlockDict.Values);
		foreach (Block block in blocks) {
			block.script.CommitMutationsAtTick();
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

	// GHOST BLOCK METHODS

	public void UpdateGhostBlockType(GameObject blockPrefab) {
		Destroy(ghostBlock);
		ghostBlock = Instantiate(
			blockPrefab,
			Vector3.zero,
			transform.rotation,
			blocksContainer.transform
		);
		ghostBlock.GetComponent<BaseBlockScript>().TransformToGhostBlock();
	}

	public void UpdateGhostBlockPosition(Vector3 position) {
		ghostBlock.transform.position = position;
	}

	public void RotateGhostBlock(Vector3 direction) {
		// TODO: rotates based on initial orientation, so not intuitive
		// rotation should be done where origin is current orientation
		if(ghostBlock.activeSelf) {
			BaseBlockScript bs = ghostBlock.GetComponent<BaseBlockScript>();
			Quaternion r = Quaternion.Euler(direction * 90.0f);
			bs.RotateBlock(r, ghostBlockRotateDuration);
		}
	}

	public void ActivateGhostBlock() {
		ghostBlock.SetActive(true);
	}

	public void DeactivateGhostBlock() {
		ghostBlock.SetActive(false);
	}


}
