using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float thrust;
    public float maxSpeed;
    
    public float torque;
    public float maxRotationalSpeed;


    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update() {}

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate() {
        CheckMoveInput();
        CheckLookInput();
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
        // TODO
    }

    private void CheckBlockInteraction() {
        // TODO
    }

}
