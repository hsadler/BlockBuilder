using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBlockScript : MonoBehaviour {


	public float ghostBlockColorAlpha = 0.5f;

	
	protected IDictionary<string, Vector3> sensorTagToDirectionVector3 = new Dictionary<string, Vector3>()
	{
		{ "TopSensor", new Vector3(0, 1, 0) },
		{ "LeftSensor", new Vector3(-1, 0, 0) },
		{ "RightSensor", new Vector3(1, 0, 0) },
		{ "FrontSensor", new Vector3(0, 0, -1) },
		{ "BackSensor", new Vector3(0, 0, 1) },
		{ "BottomSensor", new Vector3(0, -1, 0) }
	};


	protected void Start() {}

	public void DestroyBlock() {
		BlockManager.instance.UnsetBlock(gameObject);
		Destroy(gameObject);
	}

	public void DisableInteractions() {
		DisableAllColliders();
	}

	public void EnableInteractions() {
		EnableAllColliders();
	}
	
	public void AddNewBlockAsNeighbor(GameObject blockPrefab) {
		EnvironmentGeneration.instance.CreateBlock(
			blockPrefab, 
			BlockManager.instance.ghostBlock.transform.position,
			BlockManager.instance.ghostBlock.transform.rotation
		);
	}

	// GHOST BLOCK METHODS

	private void SetGhostBlockAsNeighbor(Vector3 direction) {
		Vector3 ghostBlockPosition = transform.position + direction;
		BlockManager.instance.UpdateGhostBlockPosition(ghostBlockPosition);
		BlockManager.instance.ActivateGhostBlock();
	}

	public void TransformToGhostBlock() {
		DisableAllColliders();
		SetAllMaterialColorAlphas(ghostBlockColorAlpha);
	}

	// PLAYER INTERACTION METHODS

	public void PlayerRayHitInteraction(PlayerToBlockMessage message) {
		// get object hit data
		GameObject objectHit = message.objectHit;
		if(sensorTagToDirectionVector3.ContainsKey(objectHit.tag)) {
			// CODE EXAMPLE: get sensor direction vector offset by rotation
			Vector3 direction = transform.rotation * sensorTagToDirectionVector3[objectHit.tag];
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

	public void PlayerFKeyInteraction(PlayerToBlockMessage message) {
		Debug.Log("Player hit action key 'F' for block");
	}

	// IMPLEMENTATION METHODS

	private void DisableAllColliders() {
		foreach (Collider col in GetComponentsInChildren<Collider>()) {
			col.enabled = false;
		}
	}

	private void EnableAllColliders() {
		foreach (Collider col in GetComponentsInChildren<Collider>()) {
			col.enabled = true;
		}
	}

	private void SetAllMaterialColorAlphas(float colorAlpha) {
		foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>()) {
			Color c = mr.material.color;
			c.a = 0.5f;
			mr.material.color = c;
		}
	}

}
