using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functions
{

	public static Vector3 RoundVector3ToDiscrete(Vector3 v) {
		v.x = Mathf.Round(v.x);
		v.y = Mathf.Round(v.y);
		v.z = Mathf.Round(v.z);
		return v;
	}

	public static Quaternion RoundQuaternionToDiscrete(Quaternion q) {
		Vector3 v = q.eulerAngles;
		v.x = Mathf.Round(v.x / 90) * 90;
		v.y = Mathf.Round(v.y / 90) * 90;
		v.z = Mathf.Round(v.z / 90) * 90;
		q.eulerAngles = v;
		return q;
	}

	public static Vector3 ApplyBoundsToVector3(Vector3 v, int min, int max) {
		v.x = Mathf.Round(Mathf.Clamp(v.x, min, max));
		v.y = Mathf.Round(Mathf.Clamp(v.y, min, max));
		v.z = Mathf.Round(Mathf.Clamp(v.z, min, max));
		return v;	
	}

}