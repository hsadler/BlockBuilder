using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float thrust;
    public float topSpeed;
    
    public float torque;
    public float topRotationalSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKey(KeyCode.W)) {
            GetComponent<Rigidbody>().AddForce(transform.forward * thrust);
        }
        if(Input.GetKey(KeyCode.S)) {
            GetComponent<Rigidbody>().AddForce(transform.forward * -thrust);
        }
        if(Input.GetKey(KeyCode.D)) {
            GetComponent<Rigidbody>().AddForce(transform.right * thrust);
        }
        if(Input.GetKey(KeyCode.A)) {
            GetComponent<Rigidbody>().AddForce(transform.right * -thrust);
        }
    }
}
