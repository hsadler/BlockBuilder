using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Vector3Struct
{

	// SERIALIZABLE VECTOR3 DATA STRUCTURE


	public float x;
	public float y;
	public float z;


	public Vector3Struct(Vector3 v) {
		this.x = v.x;
		this.y = v.y;
		this.z = v.z;
	}


}
