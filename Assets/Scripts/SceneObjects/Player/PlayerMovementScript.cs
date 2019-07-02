using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    // TAKEN FROM: https://drive.google.com/drive/folders/0B6SCkaV_VaTyaG1DZ2phOU1Yekk
    

	public float moveSpeed = 10.0f;
    public float jumpSpeed = 4.0f;
	public float gravity = 9.8f;
	public float terminalVelocity = 100f;
    
	private Player _selfPlayer;
	private CharacterController _charCont;
	private Rigidbody _rb;
	private Vector3 _moveDirection = Vector3.zero;


	void Start() {
		_charCont = GetComponent<CharacterController>();
		_rb = GetComponent<Rigidbody>();
	}
	
	void Update() {
		HandlePlayerMove();
	}

    public void SetSelfPlayer(Player player) {
		_selfPlayer = player;
	}

	private void HandlePlayerMove() {
		// Move direction directly from axes
		float deltaX = Input.GetAxis("Horizontal") * moveSpeed;
		float deltaZ = Input.GetAxis("Vertical") * moveSpeed;
		_moveDirection = new Vector3(deltaX, _moveDirection.y, deltaZ);
		// Accept jump input if grounded
		if (_charCont.isGrounded) {
            if (Input.GetButton("Jump")) {
                _moveDirection.y = jumpSpeed;
            } else {
				_moveDirection.y = 0f;
			}
			// handle footsteps sfx
			if(deltaX != 0 || deltaZ != 0) {
				if(!SfxManager.instance.footstepsSfx.isPlaying) {
					SfxManager.instance.footstepsSfx.Play();
				}
			} else if(SfxManager.instance.footstepsSfx.isPlaying) {
				SfxManager.instance.footstepsSfx.Stop();
			}
        } else {
			// always stop footsteps sfx if not grounded
			if(SfxManager.instance.footstepsSfx.isPlaying) {
				SfxManager.instance.footstepsSfx.Stop();
			}
		}

		_moveDirection = transform.TransformDirection(_moveDirection);
        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        _moveDirection.y -= gravity * Time.deltaTime;
        // Move the controller
        _charCont.Move(_moveDirection * Time.deltaTime);
	}


}
