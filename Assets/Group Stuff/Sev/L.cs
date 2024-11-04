using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L : MonoBehaviour
{
    private bool isControllerColliding1 = false;
    private Vector3 initialControllerPosition1;
    private Vector3 initialBoxPosition1;
    private bool swipeStarted1 = false;

    void Update()
    {
        if (isControllerColliding1)
        {
            Vector3 currentControllerPosition1 = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            Debug.Log("Current Controller Position: " + currentControllerPosition1);

            if (!swipeStarted1)
            {
                initialBoxPosition1 = transform.position;
                initialControllerPosition1 = currentControllerPosition1;
                swipeStarted1 = true;
                Debug.Log("Swipe Started at Position: " + initialControllerPosition1);
            }
            else
            {
                // Detect swipe distance
                float swipeDistance = initialControllerPosition1.x - currentControllerPosition1.x;
                transform.position = initialBoxPosition1 - new Vector3(swipeDistance, 0, 0);
                Debug.Log("Swipe Distance: " + swipeDistance);

                if (swipeDistance > 0.2f) // Adjust threshold if needed
                {
                    Debug.Log("Swipe detected, destroying box.");
                    Destroy(gameObject);
                    swipeStarted1 = false;
                }
            }
        }
        else
        {
            swipeStarted1 = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            isControllerColliding1 = true;
            Debug.Log("Controller entered collision with box.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            isControllerColliding1 = false;
            Debug.Log("Controller exited collision with box.");
        }
    }
}