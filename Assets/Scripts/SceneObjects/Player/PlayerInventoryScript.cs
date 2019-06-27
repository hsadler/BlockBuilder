using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryScript : MonoBehaviour
{

	// RESPONSIBLE FOR INTERFACE AND MANAGEMENT OF PLAYER INVENTORY


	public GameObject currentSelected;
	public GameObject ghostBlockGO;
	public float ghostBlockRotateDuration = 0.25f;

    private Player selfPlayer;


	void Start() {
		// do initializations
		InitInventorySelection();
	}

	void Update() {
		CheckInventorySelection();
	}

	public void SetSelfPlayer(Player player) {
		selfPlayer = player;
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
			currentSelected = BlockTypes.instance.scratchBlock;
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
		if(ghostBlockGO.activeSelf) {
			Quaternion addRotation = Quaternion.Euler(direction * 90.0f);
			// CODE EXAMPLE: Putting addRotation before the GhostBlock rotation causes the 
			// resulting rotation to be from a world perspective instead of local. I have
			// no idea why this works
			// RESOURCE: https://answers.unity.com/questions/512667/smooth-rotation-about-global-axis-instead-of-local.html
			Quaternion newRotation = addRotation * ghostBlockGO.transform.rotation;
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
