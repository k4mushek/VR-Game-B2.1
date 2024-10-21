using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPull : MonoBehaviour
{
    public GameObject ball; // Reference to the ball that will fall or another event trigger
    public Transform leverHandle; // The lever's handle or pivot point
    public float activationAngle = 45f; // Angle at which the lever triggers the event
    private bool isLeverActivated = false; // Track if the lever has been activated
    private void Start()
    {
        if (ball != null)
        {
            Rigidbody ballRb = ball.GetComponent<Rigidbody>();
            ballRb.constraints = RigidbodyConstraints.FreezeAll; // Freeze the ball initially
        }
    }

    private void Update()
    {
        // Track lever's local rotation angle around the Z-axis or desired axis
        float currentAngle = leverHandle.localEulerAngles.z;

        // Handle 360-degree wrap-around by normalizing the angle
        if (currentAngle > 180)
        {
            currentAngle -= 360;
        }

        // Trigger event if lever reaches or exceeds the activation angle
        if (!isLeverActivated && currentAngle <= -activationAngle)
        {
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
            ballRb.constraints = RigidbodyConstraints.None; // Unfreeze the ball to fall
        }
        // Additional events like opening a door can go here
    }
}
