using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	public float playerSpeedSetting = 0.2f;
	public float cameraSensitivitySetting = 0.5f;

	// regular speed
	private float mainSpeed = 100.0f; 
	// multiplied by how long shift is held
	private float shiftAdd = 250.0f;     
	// maximum speed when holding shift
	private float maxShift = 1000.0f; 
	// mouse look sensitivity
	private float camSens = 0.25f;     
	// kind of in the middle of the screen
	private Vector3 lastMouse = new Vector3(255, 255, 255); 
	private float totalRun= 1.0f;

	// Use this for initialization
	void Start () {
		mainSpeed *= playerSpeedSetting;
		shiftAdd *= playerSpeedSetting;
		maxShift *= playerSpeedSetting;
		camSens *= cameraSensitivitySetting;
	}
	
	// Update is called once per frame
	void Update () {
		CheckFlyNavigation();
		CheckBlockInteraction();
	}

	private void CheckBlockInteraction() {
		if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit)) {
				GameObject objectHit = hit.transform.gameObject;
				if(Input.GetMouseButtonDown(0)) {
					objectHit.SendMessageUpwards(
						"PlayerLeftClickInteraction", 
						objectHit.tag, 
						SendMessageOptions.DontRequireReceiver
					);
				} else if (Input.GetMouseButtonDown(1)) {
					objectHit.SendMessageUpwards(
						"PlayerRightClickInteraction", 
						objectHit.tag, 
						SendMessageOptions.DontRequireReceiver
					);
				}
			}
		}
	}
     
	private void CheckFlyNavigation () {
		// mouse camera angle  
		lastMouse = Input.mousePosition - lastMouse ;
		lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0 );
		lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x , transform.eulerAngles.y + lastMouse.y, 0);
		transform.eulerAngles = lastMouse;
		lastMouse = Input.mousePosition;
		// keyboard commands
		Vector3 p = GetBaseInput();
		if (Input.GetKey (KeyCode.LeftShift)) {
			totalRun += Time.deltaTime;
			p  = p * totalRun * shiftAdd;
			p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
			p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
			p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
		} else {
			totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
			p = p * mainSpeed;
		}
		p = p * Time.deltaTime;
		Vector3 newPosition = transform.position;
		// if player wants to move on X and Z axis only
		if (Input.GetKey(KeyCode.Space)) { 
			transform.Translate(p);
			newPosition.x = transform.position.x;
			newPosition.z = transform.position.z;
			transform.position = newPosition;
		} else {
			transform.Translate(p);
		}
	}
     
	// returns the basic values, if it's 0 than it's not active.
	private Vector3 GetBaseInput() { 
		Vector3 p_Velocity = new Vector3();
		if (Input.GetKey(KeyCode.W)){
			p_Velocity += new Vector3(0, 0 , 1);
		}
		if (Input.GetKey(KeyCode.S)){
			p_Velocity += new Vector3(0, 0, -1);
		}
		if (Input.GetKey(KeyCode.A)){
			p_Velocity += new Vector3(-1, 0, 0);
		}
		if (Input.GetKey(KeyCode.D)){
			p_Velocity += new Vector3(1, 0, 0);
		}
		return p_Velocity;
	}

}
