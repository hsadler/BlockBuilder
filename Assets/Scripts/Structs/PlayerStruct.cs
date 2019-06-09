using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct PlayerStruct
{

	// SERIALIZABLE PLAYER DATA STRUCTURE


	public Vector3Struct position;
	public Vector3Struct rotation;


	public PlayerStruct(
		Vector3Struct position,
		Vector3Struct rotation
	) {
		this.position = position;
		this.rotation = rotation;
	}


}
