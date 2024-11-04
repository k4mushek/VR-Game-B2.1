using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object collided with has a specific tag or type (optional)
        // For example, if you only want to deactivate when colliding with an object tagged "Obstacle":
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Deactivate this game object
            gameObject.SetActive(false);
        }
    }
}
