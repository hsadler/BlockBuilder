using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryScript : MonoBehaviour
{

	// RESPONSIBLE FOR INTERFACE AND MANAGEMENT OF PLAYER INVENTORY


	public GameObject currentSelected;
	public GameObject ghostBlockGO;
	public float ghostBlockRotateDuration = 0.25f;


	void Start() {
		// do initializations
		InitInventorySelection();
	}

	void Update() {
		CheckInventorySelection();
	}

	private void InitInventorySelection() {
		// set default block selection
		currentSelected = BlockTypes.instance.testBlock;
		UpdateGhostBlockType(currentSelected);
		ActivateGhostBlock();
	}

	private void CheckInventorySelection() {
		// TODO: come up with a better system for this
		GameObject oldSelected = currentSelected;
		if(Input.GetKey(KeyCode.Alpha1)) {
			currentSelected = BlockTypes.instance.plainBlock;
		} else if(Input.GetKey(KeyCode.Alpha2)) {
			currentSelected = BlockTypes.instance.moverBlock;
		} else if(Input.GetKey(KeyCode.Alpha3)) {
			currentSelected = BlockTypes.instance.rotatorBlock;
		} else if(Input.GetKey(KeyCode.Alpha4)) {
			currentSelected = BlockTypes.instance.conveyorBlock;
		} else if(Input.GetKey(KeyCode.Alpha5)) {
			currentSelected = BlockTypes.instance.powerBlock;
		} else if(Input.GetKey(KeyCode.Alpha6)) {
			currentSelected = BlockTypes.instance.lightBlock;
		} else if(Input.GetKey(KeyCode.Alpha7)) {
			currentSelected = BlockTypes.instance.testBlock;
		} else if(Input.GetKey(KeyCode.Alpha8)) {
			// currentSelected = BlockTypes.instance.conveyorBlock;
		} else if(Input.GetKey(KeyCode.Alpha9)) {
			// currentSelected = BlockTypes.instance.conveyorBlock;
		} else if(Input.GetKey(KeyCode.Alpha0)) {
			// currentSelected = BlockTypes.instance.testBlock;
		}
		// if inventory item was changed, update the current ghost block type
		if(oldSelected != currentSelected) {
			UpdateGhostBlockType(currentSelected);
		}
	}

	// GHOST BLOCK METHODS

	public void UpdateGhostBlockType(GameObject blockPrefab) {
		Vector3 position = Vector3.zero;
		Quaternion rotation = Quaternion.Euler(Vector3.zero);
		if(ghostBlockGO != null) {
			position = ghostBlockGO.transform.position;
			rotation = ghostBlockGO.transform.rotation;
		}
		Destroy(ghostBlockGO);
		ghostBlockGO = Instantiate(
			blockPrefab,
			position,
			rotation,
			BlockManager.instance.blocksContainer.transform
		);
		ghostBlockGO.GetComponent<BaseBlockScript>().TransformToGhostBlock();
	}

	public void UpdateGhostBlockPosition(Vector3 position) {
		ghostBlockGO.transform.position = position;
	}

	public void RotateGhostBlock(Vector3 direction) {
		// TODO: rotates based on initial orientation, so not intuitive
		// rotation should be done where origin is current orientation
		if(ghostBlockGO.activeSelf) {
			Quaternion addRotation = Quaternion.Euler(direction * 90.0f);
			Quaternion newRotation = ghostBlockGO.transform.rotation * addRotation;
			ghostBlockGO.GetComponent<BaseBlockScript>().RotateBlock(
				ghostBlockGO.transform.rotation, 
				newRotation, 
				ghostBlockRotateDuration
			);
		}
	}

	public void ActivateGhostBlock() {
		ghostBlockGO.SetActive(true);
		// fix position and rotation to be discrete
		ghostBlockGO.transform.position = Functions.RoundVector3ToDiscrete(
			ghostBlockGO.transform.position
		);
		ghostBlockGO.transform.rotation = Functions.RoundQuaternionToDiscrete(
			ghostBlockGO.transform.rotation
		);
	}

	public void DeactivateGhostBlock() {
		ghostBlockGO.SetActive(false);
	}


}
