using System.Collections;
using System.Collections.Generic;

public struct PlayerStruct
{

	// SERIALIZABLE PLAYER DATA STRUCTURE


	private Vector3Struct position;
	private Vector3Struct rotation;


	public PlayerStruct(
		Vector3Struct position,
		Vector3Struct rotation
	) {
		this.position = position;
		this.rotation = rotation;
	}


}
