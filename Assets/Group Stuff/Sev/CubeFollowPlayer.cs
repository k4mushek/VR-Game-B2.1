using UnityEngine;
using System.Collections;

public class FollowAndSwipeBox : MonoBehaviour
{
    public Transform playerCamera;            // Reference to the player's camera
    public float followSpeed = 2f;            // Speed at which the cube follows the camera
    public Vector3 offset;                    // Offset from the camera's position
    public float minSwipeDistance = 0.2f;     // Minimum swipe distance along local x-axis
    public float swipeTimeWindow = 0.5f;      // Time window to complete a swipe
    public Material collisionMaterial;        // Material to apply when colliding with controller
    public float fadeDuration = 1f;           // Duration for the fade-out effect
    public Vector2 screenLimitX = new Vector2(-5f, 5f);  // X-axis screen limit for random positioning
    public Vector2 screenLimitY = new Vector2(1f, 5f);   // Y-axis screen limit for random positioning

    private bool isFollowing = true;
    private bool isRightControllerColliding = false;
    private bool isLeftControllerColliding = false;
    private Vector3 initialControllerPosition;
    private Vector3 initialBoxPosition;
    private bool swipeStarted = false;
    private float swipeStartTime;

    private Renderer boxRenderer;
    private Material[] originalMaterials;
    private Color originalColor;

    void Start()
    {
        // Randomize the x and y components of the offset within the range -0.2 to 0.2
        offset.x = Random.Range(-0.2f, 0.2f);
        offset.y = Random.Range(-0.2f, 0.2f);

        // Initialize the box position with the new offset
        transform.position = playerCamera.position + playerCamera.rotation * offset;

        // Get the renderer and original materials
        boxRenderer = GetComponent<Renderer>();
        originalMaterials = boxRenderer.materials;  // Store original materials
        originalColor = originalMaterials[0].color;  // Assuming the first material is the main one
    }

    void Update()
    {
        if (isRightControllerColliding || isLeftControllerColliding)
        {
            OVRInput.Controller activeController = isRightControllerColliding ? OVRInput.Controller.RTouch : OVRInput.Controller.LTouch;
            Vector3 currentControllerPosition = OVRInput.GetLocalControllerPosition(activeController);

            HandleSwipe(currentControllerPosition);
        }
        else if (playerCamera != null && isFollowing)
        {
            Vector3 targetPosition = playerCamera.position + playerCamera.rotation * offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
            transform.LookAt(playerCamera);
        }
    }

    private void HandleSwipe(Vector3 currentControllerPosition)
    {
        if (!swipeStarted)
        {
            initialBoxPosition = transform.position;
            initialControllerPosition = currentControllerPosition;
            swipeStartTime = Time.time;
            swipeStarted = true;
            isFollowing = false;
        }
        else
        {
            Vector3 controllerMovement = currentControllerPosition - initialControllerPosition;
            float swipeDistance = Vector3.Dot(controllerMovement, transform.right);

            transform.position = initialBoxPosition + transform.right * swipeDistance;

            if (Mathf.Abs(swipeDistance) > minSwipeDistance && (Time.time - swipeStartTime) < swipeTimeWindow)
            {
                StartCoroutine(FadeOutAndDestroy());
            }
            else if (Time.time - swipeStartTime >= swipeTimeWindow)
            {
                ResetSwipe();
            }
        }
    }

    private IEnumerator FadeOutAndDestroy()
    {
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            Color newColor = originalColor;
            newColor.a = Mathf.Lerp(1, 0, normalizedTime);
            originalMaterials[0].color = newColor;
            yield return null;
        }

        boxRenderer.materials = originalMaterials;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ControllerR"))
        {
            isRightControllerColliding = true;
            ApplyCollisionMaterial();
        }
        else if (other.CompareTag("ControllerL"))
        {
            isLeftControllerColliding = true;
            ApplyCollisionMaterial();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ControllerR"))
        {
            isRightControllerColliding = false;
            ResetSwipe();
        }
        else if (other.CompareTag("ControllerL"))
        {
            isLeftControllerColliding = false;
            ResetSwipe();
        }

        boxRenderer.materials = originalMaterials;
    }

    private void ApplyCollisionMaterial()
    {
        Material[] materialsWithOutline = new Material[originalMaterials.Length + 1];
        materialsWithOutline[0] = collisionMaterial;
        originalMaterials.CopyTo(materialsWithOutline, 1);
        boxRenderer.materials = materialsWithOutline;
    }

    private void ResetSwipe()
    {
        swipeStarted = false;
        isFollowing = true;
    }
}