using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{


	public GameObject gameObject;
	
	public PlayerMovementScript movementScript;
	public PlayerLookScript lookScript;
    public PlayerInventoryScript inventoryScript;
	public PlayerBlockInteractionScript blockInteractionScript;

	public bool playerActive;


	public Player(
        GameObject gameObject,
		PlayerMovementScript movementScript,
		PlayerLookScript lookScript,
        PlayerInventoryScript inventoryScript,
		PlayerBlockInteractionScript blockInteractionScript
    ) {
		this.gameObject = gameObject;
		this.movementScript = movementScript;
		this.lookScript = lookScript;
        this.inventoryScript = inventoryScript;
		this.blockInteractionScript = blockInteractionScript;
		this.movementScript.SetSelfPlayer(this);
		this.lookScript.SetSelfPlayer(this);
		this.inventoryScript.SetSelfPlayer(this);
		this.blockInteractionScript.SetSelfPlayer(this);
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
