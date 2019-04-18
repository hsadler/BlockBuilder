using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBlockScript : MonoBehaviour {

    // scene management references
	protected EnvironmentGeneration environmentGeneration;
	protected BlockManager blockManager;

	protected IDictionary<string, Vector3> sensorTagToDirectionVector3 = new Dictionary<string, Vector3>()
	{
		{ "TopSensor", new Vector3(0, 1, 0) },
		{ "LeftSensor", new Vector3(-1, 0, 0) },
		{ "RightSensor", new Vector3(1, 0, 0) },
		{ "FrontSensor", new Vector3(0, 0, -1) },
		{ "BackSensor", new Vector3(0, 0, 1) },
		{ "BottomSensor", new Vector3(0, -1, 0) }
	};


	// Use this for initialization
	public void Start () {
		// set references
		environmentGeneration = EnvironmentGeneration.instance;
        blockManager = BlockManager.instance;
	}
	
	// Update is called once per frame
	public void Update () {}

	public bool DestroyBlock() {
		blockManager.UnsetBlock(gameObject);
		Destroy(gameObject);
		return true;
	}

	public GameObject AddNewBlockAsNeighbor(GameObject blockPrefab, Vector3 direction) {
		Vector3 newBlockPosition = transform.position + direction;
		return environmentGeneration.CreateBlock(blockPrefab, newBlockPosition);
	}

	private void SetGhostBlockAsNeighbor(GameObject blockPrefab, Vector3 direction) {
		Vector3 ghostBlockPosition = transform.position + direction;
		blockManager.UpdateGhostBlock(blockPrefab, ghostBlockPosition);
	}

	public void PlayerRayHitInteraction(PlayerToBlockMessage message) {
		// get player data 
		GameObject player = message.player;
		GameObject blockPrefab = player.GetComponent<PlayerInventoryScript>().currentSelected;
		// get object hit data
		GameObject objectHit = message.objectHit;
		if(sensorTagToDirectionVector3.ContainsKey(objectHit.tag)) {
			Vector3 direction = sensorTagToDirectionVector3[objectHit.tag];
			SetGhostBlockAsNeighbor(blockPrefab, direction);
		}
	}

	public void PlayerLeftClickInteraction(PlayerToBlockMessage message) {
		DestroyBlock();
	}

	public void PlayerRightClickInteraction(PlayerToBlockMessage message) {
		// get player data 
		GameObject player = message.player;
		GameObject blockPrefab = player.GetComponent<PlayerInventoryScript>().currentSelected;
		// get object hit data
		GameObject objectHit = message.objectHit;
		if(sensorTagToDirectionVector3.ContainsKey(objectHit.tag)) {
			Vector3 direction = sensorTagToDirectionVector3[objectHit.tag];
			AddNewBlockAsNeighbor(blockPrefab, direction);
		}
	}

}
