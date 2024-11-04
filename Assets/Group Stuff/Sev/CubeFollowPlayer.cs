using UnityEngine;
using System.Collections;  // Ensure this is included for IEnumerator

public class FollowAndSwipeBox : MonoBehaviour
{
    public Transform playerCamera;           // Reference to the player's camera
    public float followSpeed = 2f;           // Speed at which the cube follows the camera
    public Vector3 offset;                    // Offset from the camera's position
    public float minSwipeDistance = 0.2f;    // Minimum swipe distance along local x-axis
    public float swipeTimeWindow = 0.5f;     // Time window to complete a swipe
    public Material collisionMaterial;        // Material to apply when colliding with controller
    public float fadeDuration = 1f;           // Duration for the fade-out effect

    private bool isFollowing = true;          // Controls whether the box is following the player
    private bool isRightControllerColliding = false;  // Track if the right controller is colliding
    private bool isLeftControllerColliding = false;   // Track if the left controller is colliding
    private Vector3 initialControllerPosition;
    private Vector3 initialBoxPosition;
    private bool swipeStarted = false;
    private float swipeStartTime;

    private Renderer boxRenderer;
    private Material[] originalMaterials;     // Original materials to revert back
    private Color originalColor;               // Original color of the box

    void Start()
    {
        // Initialize the box position with offset
        transform.position = playerCamera.position + playerCamera.rotation * offset;

        // Get the renderer and original materials
        boxRenderer = GetComponent<Renderer>();
        originalMaterials = boxRenderer.materials;  // Store original materials
        originalColor = originalMaterials[0].color;  // Assuming the first material is the main one
    }

    void Update()
    {
        // Check for swipe on the right or left controller
        if (isRightControllerColliding || isLeftControllerColliding)
        {
            OVRInput.Controller activeController = isRightControllerColliding ? OVRInput.Controller.RTouch : OVRInput.Controller.LTouch;
            Vector3 currentControllerPosition = OVRInput.GetLocalControllerPosition(activeController);

            HandleSwipe(currentControllerPosition);
        }
        else
        {
            if (playerCamera != null && isFollowing)
            {
                // Follow the player if no swipe is detected
                Vector3 targetPosition = playerCamera.position + playerCamera.rotation * offset;
                transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
                transform.LookAt(playerCamera);
            }
        }
    }

    private void HandleSwipe(Vector3 currentControllerPosition)
    {
        if (!swipeStarted)
        {
            // Initialize swipe variables
            initialBoxPosition = transform.position;
            initialControllerPosition = currentControllerPosition;
            swipeStartTime = Time.time;
            swipeStarted = true;
            isFollowing = false; // Stop following while swiping
        }
        else
        {
            // Calculate swipe distance along the box's local x-axis
            Vector3 controllerMovement = currentControllerPosition - initialControllerPosition;
            float swipeDistance = Vector3.Dot(controllerMovement, transform.right);

            // Move box with controller movement along local x-axis
            transform.position = initialBoxPosition + transform.right * swipeDistance;

            // Check if swipe distance and time meet criteria
            if (Mathf.Abs(swipeDistance) > minSwipeDistance && (Time.time - swipeStartTime) < swipeTimeWindow)
            {
                Debug.Log("Swipe detected, destroying box.");
                StartCoroutine(FadeOutAndDestroy()); // Start the fade-out coroutine
            }
            // Reset if swipe is too slow or movement is minimal
            else if (Time.time - swipeStartTime >= swipeTimeWindow)
            {
                ResetSwipe();
            }
        }
    }

    private IEnumerator FadeOutAndDestroy()
    {
        // Start fading out
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            Color newColor = originalColor;
            newColor.a = Mathf.Lerp(1, 0, normalizedTime);  // Fade to transparent
            originalMaterials[0].color = newColor; // Apply to main material
            yield return null;
        }

        // Remove the outline material by setting the materials back to the original ones
        boxRenderer.materials = originalMaterials; // Reset to original materials

        // Destroy the box
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ControllerR"))
        {
            isRightControllerColliding = true;
            Debug.Log("Right controller entered collision with box.");
            // Apply the collision material when colliding
            ApplyCollisionMaterial();
        }
        else if (other.CompareTag("ControllerL"))
        {
            isLeftControllerColliding = true;
            Debug.Log("Left controller entered collision with box.");
            // Apply the collision material when colliding
            ApplyCollisionMaterial();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ControllerR"))
        {
            isRightControllerColliding = false;
            Debug.Log("Right controller exited collision with box.");
            ResetSwipe(); // Reset swipe state
        }
        else if (other.CompareTag("ControllerL"))
        {
            isLeftControllerColliding = false;
            Debug.Log("Left controller exited collision with box.");
            ResetSwipe(); // Reset swipe state
        }

        // Remove the outline material when exiting collision
        boxRenderer.materials = originalMaterials; // Reset materials to original
    }

    private void ApplyCollisionMaterial()
    {
        Material[] materialsWithOutline = new Material[originalMaterials.Length + 1];
        materialsWithOutline[0] = collisionMaterial; // Set the outline material as the first element
        originalMaterials.CopyTo(materialsWithOutline, 1); // Copy original materials to the array starting from index 1
        boxRenderer.materials = materialsWithOutline; // Apply the new materials
    }

    private void ResetSwipe()
    {
        swipeStarted = false; // Reset swipe flag
        isFollowing = true;   // Resume following
    }
}