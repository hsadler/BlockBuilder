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

	// MOVEMENT METHODS

	public void DestroyBlock() {
		blockManager.UnsetBlock(gameObject);
		Destroy(gameObject);
	}

	public void AddNewBlockAsNeighbor(GameObject blockPrefab) {
		environmentGeneration.CreateBlock(
			blockPrefab, 
			blockManager.ghostBlock.transform.position,
			blockManager.ghostBlock.transform.rotation
		);
	}

	// GHOST BLOCK METHODS

	private void SetGhostBlockAsNeighbor(Vector3 direction) {
		Vector3 ghostBlockPosition = transform.position + direction;
		blockManager.UpdateGhostBlockPosition(ghostBlockPosition);
		blockManager.ActivateGhostBlock();
	}

	public void TransformToGhostBlock() {
		foreach (Collider col in GetComponentsInChildren<Collider>()) {
			col.enabled = false;
		}
		foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>()) {
			Color c = mr.material.color;
			c.a = 0.5f;
			mr.material.color = c;
		}
	}

	// PLAYER INTERACTION METHODS

	public void PlayerRayHitInteraction(PlayerToBlockMessage message) {
		// get object hit data
		GameObject objectHit = message.objectHit;
		if(sensorTagToDirectionVector3.ContainsKey(objectHit.tag)) {
			Vector3 direction = sensorTagToDirectionVector3[objectHit.tag];
			SetGhostBlockAsNeighbor(direction);
		}
	}

	public void PlayerLeftClickInteraction(PlayerToBlockMessage message) {
		DestroyBlock();
	}

	public void PlayerRightClickInteraction(PlayerToBlockMessage message) {
		// get player data 
		GameObject player = message.player;
		GameObject blockPrefab = player.GetComponent<PlayerInventoryScript>().currentSelected;
		AddNewBlockAsNeighbor(blockPrefab);
	}

}
