
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

}