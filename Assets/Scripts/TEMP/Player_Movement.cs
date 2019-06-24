﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {

	public float speed = 4.0f;
	public float gravity = -9.8f;

	private CharacterController _charCont;

	// Use this for initialization
	void Start () {
		_charCont = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		float deltaX = Input.GetAxis ("Horizontal") * speed;
		float deltaZ = Input.GetAxis ("Vertical") * speed;
		Vector3 movement = new Vector3 (deltaX, 0, deltaZ);
        // Limits the max speed of the player
		movement = Vector3.ClampMagnitude (movement, speed); 
        movement.y = gravity;
        // Ensures the speed the player moves does not change based on frame rate
		movement *= Time.deltaTime;		
		movement = transform.TransformDirection(movement);
		_charCont.Move (movement);
	}
}