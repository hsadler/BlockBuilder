using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockInteractionScript : MonoBehaviour
{


    private Player selfPlayer;


	void Start() {
	}
	
	void Update() {
        if(selfPlayer.playerActive) {
            CheckBlockInteraction();
            CheckGhostBlockInput();
        }
	}

    public void SetSelfPlayer(Player player) {
		selfPlayer = player;
	}

    private void CheckBlockInteraction() {
		RaycastHit hit;
		Ray ray = selfPlayer.lookScript.playerCameraComponent.ViewportPointToRay(
            new Vector3(0.5F, 0.5F, 0)
        );
		if(Physics.Raycast(ray, out hit)) {
			GameObject objectHit = hit.transform.gameObject;
			PlayerToBlockMessage payload = new PlayerToBlockMessage(gameObject, objectHit);
			// send standard ray hit message
			objectHit.SendMessageUpwards(
				"PlayerRayHitInteraction",
				payload,
				SendMessageOptions.DontRequireReceiver
			);
			if(Input.GetMouseButtonDown(0)) {
				objectHit.SendMessageUpwards(
					"PlayerLeftClickInteraction",
					payload,
					SendMessageOptions.DontRequireReceiver
				);
			} else if(Input.GetMouseButtonDown(1)) {
				objectHit.SendMessageUpwards(
					"PlayerRightClickInteraction",
					payload,
					SendMessageOptions.DontRequireReceiver
				);
			} else if(Input.GetKeyDown(KeyCode.F)) {
				objectHit.SendMessageUpwards(
					"PlayerFKeyInteraction",
					payload,
					SendMessageOptions.DontRequireReceiver
				);
			}
		} else {
			selfPlayer.inventoryScript.DeactivateGhostBlock();
		}
	}
    
    private void CheckGhostBlockInput() {
		Vector3 direction = Vector3.zero;
		if(Input.GetKeyDown(KeyCode.UpArrow)) {
			direction = Vector3.right;
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)) {
			direction = Vector3.left;
		}
		if(Input.GetKeyDown(KeyCode.LeftArrow)) {
			direction = Vector3.down;
		}
		if(Input.GetKeyDown(KeyCode.RightArrow)) {
			direction = Vector3.up;
		}
		if(direction != Vector3.zero) {
			selfPlayer.inventoryScript.RotateGhostBlock(direction);
		}
	}


}
