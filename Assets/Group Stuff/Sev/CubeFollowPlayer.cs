using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFollowPlayer : MonoBehaviour
{
    public Transform playerCamera;  // Reference to the player's camera
    public float followSpeed = 2f;  // Speed at which the cube follows the camera
    public Vector3 offset = new Vector3(0, 1, -2);  // Offset from the camera's position

    void Update()
    {
        if (playerCamera != null)
        {
            // Calculate the target position based on the camera's position and the offset
            Vector3 targetPosition = playerCamera.position + playerCamera.rotation * offset;

            // Smoothly move the cube towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            // Optionally, have the cube look at the camera
            transform.LookAt(playerCamera);
        }
    }
}
