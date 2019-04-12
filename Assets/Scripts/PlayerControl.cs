using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float thrust;
    public float maxSpeed;
    
    public float rotationSpeed;


    private Transform playerHeadTransform;
    private Transform playerCameraTransform;
    private Camera playerCameraComponent;


    // Start is called before the first frame update
    void Start() {
        Cursor.visible = false;
        playerHeadTransform = transform.Find("Head");
        playerCameraTransform = transform.Find("Head/PlayerCamera");
        playerCameraComponent = playerCameraTransform.GetComponent<Camera>(); 
    }

    // Update is called once per frame
    void Update() {
        CheckLookInput();
        CheckBlockInteraction();
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

    private void CheckMoveInputAndMoveByTranslation() {
        // NOT USED
        if(Input.GetKey(KeyCode.W)) {
            transform.Translate(Vector3.forward * Time.deltaTime * maxSpeed, Space.Self);
        }
        if(Input.GetKey(KeyCode.S)) {
            transform.Translate(-Vector3.forward * Time.deltaTime * maxSpeed, Space.Self);
        }
        if(Input.GetKey(KeyCode.D)) {
            transform.Translate(Vector3.right * Time.deltaTime * maxSpeed, Space.Self);
        }
        if(Input.GetKey(KeyCode.A)) {
            transform.Translate(-Vector3.right * Time.deltaTime * maxSpeed, Space.Self);
        }
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
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
			RaycastHit hit;
            Ray ray = playerCameraComponent.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
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

}
