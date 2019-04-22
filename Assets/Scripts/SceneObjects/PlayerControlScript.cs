using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlScript : MonoBehaviour {

    // RESPONSIBLE FOR PLAYER CONTROL FROM USER INPUT


    public float thrust;
    public float maxSpeed;
    public float rotationSpeed;

    private Transform playerHeadTransform;
    private Transform playerCameraTransform;
    private Camera playerCameraComponent;


    // Start is called before the first frame update
    void Start() {
        // hide cursor on screen
        Cursor.visible = false;
        // set references to body parts for movement
        playerHeadTransform = transform.Find("Head");
        playerCameraTransform = transform.Find("Head/PlayerCamera");
        playerCameraComponent = playerCameraTransform.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update() {
        CheckLookInput();
        CheckBlockInteraction();
        CheckGhostBlockInput();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() {
        CheckMoveInput();
    }

    private void CheckMoveInput() {
        Rigidbody rb = GetComponent<Rigidbody>();
        // arrow keys
        if(Input.GetKey(KeyCode.W)) {
            rb.AddForce(transform.forward * thrust);
        }
        if(Input.GetKey(KeyCode.S)) {
            rb.AddForce(transform.forward * -thrust);
        }
        if(Input.GetKey(KeyCode.D)) {
            rb.AddForce(transform.right * thrust);
        }
        if(Input.GetKey(KeyCode.A)) {
            rb.AddForce(transform.right * -thrust);
        }
        // jump pack
        if(Input.GetKey(KeyCode.Space)) {
            rb.AddForce(transform.up * thrust);
        }
        // enforce max speed
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

    private void CheckLookInput() {
        float horizontalRotation = rotationSpeed * Input.GetAxis("Mouse X");
        float verticalRotation = rotationSpeed * Input.GetAxis("Mouse Y");
        // horizontal input rotates the whole body
        transform.Rotate(Vector3.up * horizontalRotation);
        // vertical input rotates the head
        playerHeadTransform.Rotate(-Vector3.right * verticalRotation, Space.Self);
    }

    private void CheckBlockInteraction() {
        RaycastHit hit;
        Ray ray = playerCameraComponent.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
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
            } else if (Input.GetMouseButtonDown(1)) {
                objectHit.SendMessageUpwards(
                    "PlayerRightClickInteraction", 
                    payload, 
                    SendMessageOptions.DontRequireReceiver
                );
            }
        } else {
            BlockManager.instance.DeactivateGhostBlock();
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
            BlockManager.instance.RotateGhostBlock(direction);
        }
    }

}
