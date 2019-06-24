using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    // TAKEN FROM: https://drive.google.com/drive/folders/0B6SCkaV_VaTyaG1DZ2phOU1Yekk
    

	public float speed = 4.0f;
	public float gravity = -9.8f;

	private CharacterController _charCont;
    private Player selfPlayer;


	void Start() {
		_charCont = GetComponent<CharacterController>();
	}
	
	void Update() {
		// OLD:
		// arrow keys
		// if(Input.GetKey(KeyCode.W)) {
		// 	rb.AddForce(transform.forward * thrust);
		// }
		// if(Input.GetKey(KeyCode.S)) {
		// 	rb.AddForce(transform.forward * -thrust);
		// }
		// if(Input.GetKey(KeyCode.D)) {
		// 	rb.AddForce(transform.right * thrust);
		// }
		// if(Input.GetKey(KeyCode.A)) {
		// 	rb.AddForce(transform.right * -thrust);
		// }
		// // jump pack
		// if(Input.GetKey(KeyCode.Space)) {
		// 	rb.AddForce(transform.up * thrust);
		// }

		// NEW:
		float deltaX = Input.GetAxis("Horizontal") * speed;
		float deltaZ = Input.GetAxis("Vertical") * speed;
		Vector3 movement = new Vector3 (deltaX, 0, deltaZ);
        // Limits the max speed of the player
		movement = Vector3.ClampMagnitude(movement, speed); 
        movement.y = gravity;
        // Ensures the speed the player moves does not change based on frame rate
		movement *= Time.deltaTime;		
		movement = transform.TransformDirection(movement);
		_charCont.Move(movement);
	}

    public void SetSelfPlayer(Player player) {
		selfPlayer = player;
	}


}
