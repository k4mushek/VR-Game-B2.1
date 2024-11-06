using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPull : MonoBehaviour
{
    public GameObject ball; 
    public Transform leverHandle; 
    public float activationAngle = 45f; 
    private bool isLeverActivated = false; 
    private void Start()
    {
        if (ball != null)
        {
            Rigidbody ballRb = ball.GetComponent<Rigidbody>();
            ballRb.constraints = RigidbodyConstraints.FreezeAll; //freeze ball rotation
        }
    }

    private void Update()
    {
        // Track rotation
        float currentAngle = leverHandle.localEulerAngles.z;

        // Handler
        if (currentAngle > 180)
        {
            currentAngle -= 360;
        }
        Debug.Log("Current Lever Angle: " + currentAngle);

        // Activation if Z is -45
        if (!isLeverActivated && currentAngle <= -activationAngle)
        {
            Debug.Log("Lever has been activated!");
            ActivateLever();
        }
    }

    void ActivateLever()
    {
        isLeverActivated = true;
        Debug.Log("Lever activated!");

        if (ball != null)
        {
            Rigidbody ballRb = ball.GetComponent<Rigidbody>();
            ballRb.constraints = RigidbodyConstraints.None; // Ball fall
        }
        
    }
}
