using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Camera-Control/Smooth Mouse Look")]
public class SmoothMouseLook : MonoBehaviour 
{
 
    // TAKEN FROM HERE AND MODIFIED: https://forum.unity.com/threads/simple-first-person-camera-script.417611/


    // MY MOD
    private Transform playerHeadTransform;


    // publics
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    public float frameCounter = 20;
    // privates
    private float rotationX = 0F;
    private float rotationY = 0F;
    private List<float> rotArrayX = new List<float>();
    private float rotAverageX = 0F;
    private List<float> rotArrayY = new List<float>();
    private float rotAverageY = 0F;
    private Quaternion originalRotation;
    private Quaternion originalHeadRotation;
    private Quaternion xQuaternion;
    private Quaternion yQuaternion;


    void Start() {
        
        // MY MOD
        playerHeadTransform = transform.Find("Head");
        
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb) {
            rb.freezeRotation = true;
        }
        originalRotation = transform.localRotation;
        originalHeadRotation = playerHeadTransform.localRotation; 

    }

    void Update() {
        CheckLook();
    }

    private void CheckLook() {
		CalculateNewRotations();

        // transform.localRotation = originalRotation * xQuaternion * yQuaternion;

		// horizontal input rotates the whole body
        transform.localRotation = originalRotation * xQuaternion;
		// vertical input rotates the head
        playerHeadTransform.transform.localRotation = originalHeadRotation * yQuaternion;

		// horizontal input rotates the whole body
		// transform.Rotate(Vector3.up * xQuaternion);
		// vertical input rotates the head
		// playerHeadTransform.Rotate(-Vector3.right * yQuaternion, Space.Self);
	}

    private void CalculateNewRotations() {
        
        // Resets the average rotation
        rotAverageY = 0f;
        rotAverageX = 0f;
    
        // Gets rotational input from the mouse
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationX += Input.GetAxis("Mouse X") * sensitivityX;
    
        // Adds the rotation values to their relative array
        rotArrayY.Add(rotationY);
        rotArrayX.Add(rotationX);
    
        // If the arrays length is bigger or equal to the value of 
        // frameCounter remove the first value in the array
        if (rotArrayY.Count >= frameCounter) {
            rotArrayY.RemoveAt(0);
        }
        if (rotArrayX.Count >= frameCounter) {
            rotArrayX.RemoveAt(0);
        }
    
        // Adding up all the rotational input values from each array
        for(int j = 0; j < rotArrayY.Count; j++) {
            rotAverageY += rotArrayY[j];
        }
        for(int i = 0; i < rotArrayX.Count; i++) {
            rotAverageX += rotArrayX[i];
        }
    
        // Standard maths to find the average
        rotAverageY /= rotArrayY.Count;
        rotAverageX /= rotArrayX.Count;
    
        // Clamp the rotation average to be within a specific value range
        rotAverageY = ClampAngle(rotAverageY, minimumY, maximumY);
        rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);
    
        // MY MOD: set the class members instead
        // Get the rotation you will be at next as a Quaternion
        yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
        xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);
    
        // Rotate
        // transform.localRotation = originalRotation * xQuaternion * yQuaternion;

    }

    private float ClampAngle(float angle, float min, float max) {
        angle = angle % 360;
        if ((angle >= -360F) && (angle <= 360F)) {
            if (angle < -360F) {
                angle += 360F;
            }
            if (angle > 360F) {
                angle -= 360F;
            }        
        }
        return Mathf.Clamp(angle, min, max);
    }


}