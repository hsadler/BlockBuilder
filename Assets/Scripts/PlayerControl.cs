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
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() {
        CheckMoveInput();
        CheckBlockInteraction();
    }

    private void CheckMoveInput() {
        Rigidbody rb = GetComponent<Rigidbody>();
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
        // enforce max speed
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

    private void CheckLookInput() {
        float horizontalRotation = rotationSpeed * Input.GetAxis("Mouse X");
        float verticalRotation = rotationSpeed * Input.GetAxis("Mouse Y");
        // horizontal input rotates the whole body
        transform.Rotate(Vector3.up * horizontalRotation);
        playerHeadTransform.Rotate(-Vector3.right * verticalRotation, Space.Self);
    }

    private void CheckBlockInteraction() {
        // TODO
    }

}
