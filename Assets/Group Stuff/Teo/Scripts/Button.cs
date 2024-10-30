using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Transform buttonTransform;  // 
    public float pressDepth = 0.1f;    // How far down the button is pressed
    public float pressThreshold = 0.05f; // How deep to consider it pressed
  
    private bool isPressed = false;     
    public GameObject Door;
    private Vector3 initialPosition;    // Store the initial position of the button
   
    private void Start()
    {
        initialPosition = buttonTransform.localPosition;  // Set the button's initial position
    }

    private void OnTriggerEnter(Collider press)
    {
        if (!isPressed && press.CompareTag("PlayerHand"))
        {
            PressButton();
        }
    }

    private void OnTriggerExit(Collider release)
    {
        if (isPressed && release.CompareTag("PlayerHand"))
        {
            ReleaseButton();
        }
    }

    private void PressButton()
    {
        isPressed = true;
        Debug.Log("Button Pressed!");

        // Move the button down
        buttonTransform.localPosition = new Vector3(buttonTransform.localPosition.x,
                                                    buttonTransform.localPosition.y ,
                                                    buttonTransform.localPosition.z- pressDepth);

        // Trigger any event, such as opening a door, here
    }

    private void ReleaseButton()
    {
        isPressed = false;

        Debug.Log("Button Released!");

        // Move the button back to its original position
        buttonTransform.localPosition = initialPosition;
        if (Door != null)
        {
            Destroy(Door);
            Debug.Log(Door.name + "isDestroyed");

        }
        // Trigger release events here, if needed
    }
}
