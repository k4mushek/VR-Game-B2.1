using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationRaycast : MonoBehaviour
{
    public float angle = 45f;
    public float distance = 10f;
    public string targetObjectName = "TargetObject";

    void Update()
    {
        // Calculate the direction of the ray based on the angle
        Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;

        // Perform the raycast
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, distance))
        {
            // Draw the ray up to the point it hit
            Debug.DrawRay(transform.position, direction * hit.distance, Color.green);

            // Check if the ray hit the specific target object
            if (hit.collider.gameObject.name == targetObjectName)
            {
                Debug.Log("Raycast hit the target object: " + targetObjectName);
            }
        }
        else
        {
            // Draw the full ray if it didn't hit anything
            Debug.DrawRay(transform.position, direction * distance, Color.red);
        }
    }
}
