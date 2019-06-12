using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{


	public GameObject gameObject;
    public PlayerControlScript controlScript;
    public PlayerInventoryScript inventoryScript;


	public Player(
        GameObject gameObject, 
        PlayerControlScript controlScript, 
        PlayerInventoryScript inventoryScript
    ) {
		this.gameObject = gameObject;
		this.controlScript = controlScript;
        this.inventoryScript = inventoryScript;
	}

	public PlayerStruct ToPlayerStruct() {
		Vector3Struct positionStruct = new Vector3Struct(gameObject.transform.position);
		Vector3Struct rotationStruct = new Vector3Struct(gameObject.transform.rotation.eulerAngles);
		return new PlayerStruct(
			positionStruct,
			rotationStruct
		);
	}


}
