using System.Collections;
using System.Collections.Generic;

public struct BlockStruct
{

	// SERIALIZABLE BLOCK DATA STRUCTURE


	private string type;
	private Vector3Struct position;
	private Vector3Struct rotation;


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
