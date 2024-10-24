using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindowsFollowPlayer : MonoBehaviour
{
    public Transform player; // Player transform to follow
    public List<RectTransform> uiWindows; // List of all UI windows
    public float distanceFromPlayer = 0.6f; // Distance of UI from player
    public float heightOffset = 200f; // Adjust this to place UI at eye level or appropriate height
    public float rotationSpeed = 5f; // Speed of rotation when facing the player
    public float angularSpacing = 50f; // Angular spacing between UI windows

    private Camera mainCamera;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player transform is not assigned.");
            return;
        }

        mainCamera = Camera.main;

        // If UI windows are not set via the inspector, find all the RectTransforms under the parent object
        if (uiWindows == null || uiWindows.Count == 0)
        {
            uiWindows = new List<RectTransform>(GetComponentsInChildren<RectTransform>());
        }
    }

    void Update()
    {
        PositionUIWindows();
    }

    void PositionUIWindows()
    {
        int windowCount = uiWindows.Count;

        // Calculate the angle increment to position the windows in a circular or arc pattern
        float totalAngle = Mathf.Min(360f, windowCount * angularSpacing); // Limit total angle to 360 if there are many windows
        float startAngle = -totalAngle / 2f; // Start from the left side of the player and go around

        for (int i = 0; i < windowCount; i++)
        {
            RectTransform window = uiWindows[i];

            // Calculate the angle for this UI window
            float angle = startAngle + i * angularSpacing;

            // Convert the angle to radians and calculate the position in 3D space
            Vector3 offset = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), 180, Mathf.Cos(Mathf.Deg2Rad * angle)) * distanceFromPlayer;
            Vector3 worldPos = player.position + offset;

            // Apply height offset in world space, account for UI height
            worldPos.y = player.position.y + heightOffset;

            // Make sure we are in world space and set the position
            window.position = worldPos;

            // Debug: Draw a line in the Scene view to visualize window position
            Debug.DrawLine(player.position, window.position, Color.red);

            // Make the UI window face the player
            RotateWindowTowardsPlayer(window);
        }
    }

    void RotateWindowTowardsPlayer(RectTransform window)
    {
        // Direction to the player
        Vector3 directionToPlayer = player.position - window.position;

        // Adjust the direction to align the UI window's forward direction
        directionToPlayer.y = 0; // Ignore Y-axis to only rotate on the horizontal plane

        if (directionToPlayer != Vector3.zero)
        {
            // Calculate the rotation needed to look at the player
            Quaternion targetRotation = Quaternion.LookRotation(-directionToPlayer); // Use the negative to flip

            // Smoothly rotate the UI window towards the player
            window.rotation = Quaternion.Slerp(window.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}