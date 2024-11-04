using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s : MonoBehaviour
{
    private bool isControllerColliding = false;
    private Vector3 initialControllerPosition;
    private Vector3 initialBoxPosition;
    private bool swipeStarted = false;

    void Update()
    {
        if (isControllerColliding)
        {
            Vector3 currentControllerPositionR = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            Debug.Log("Current Controller Position: " + currentControllerPositionR);

            if (!swipeStarted)
            {
                initialBoxPosition = transform.position;
                initialControllerPosition = currentControllerPositionR;
                swipeStarted = true;
                Debug.Log("Swipe Started at Position: " + initialControllerPosition);
            }
            else
            {
                // Detect swipe distance
                float swipeDistance = initialControllerPosition.x - currentControllerPositionR.x;
                transform.position = initialBoxPosition - new Vector3(swipeDistance, 0, 0);
                Debug.Log("Swipe Distance: " + swipeDistance);

                if (swipeDistance > 0.2f) // Adjust threshold if needed
                {
                    Debug.Log("Swipe detected, destroying box.");
                    Destroy(gameObject);
                    swipeStarted = false;
                }
            }
        }
        else
        {
            swipeStarted = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            isControllerColliding = true;
            Debug.Log("Controller entered collision with box.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            isControllerColliding = false;
            Debug.Log("Controller exited collision with box.");
        }
    }
}