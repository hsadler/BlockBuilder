using System.Collections;
using System.Collections.Generic;

public struct Vector3Struct
{

	// SERIALIZABLE VECTOR3 DATA STRUCTURE


	private float x;
	private float y;
	private float z;


	public Vector3Struct(Vector3 v) {
		this.x = v.x;
		this.y = v.y;
		this.z = v.z;
	}


}
