using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct BlockStruct
{

	// SERIALIZABLE BLOCK DATA STRUCTURE


	public string type;
	public Vector3Struct position;
	public Vector3Struct rotation;


	public BlockStruct(
		string type,
		Vector3Struct position,
		Vector3Struct rotation
	) {
		this.type = type;
		this.position = position;
		this.rotation = rotation;
	}


}
