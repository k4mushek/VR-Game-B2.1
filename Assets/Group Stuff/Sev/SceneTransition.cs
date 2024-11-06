using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnCollision : MonoBehaviour
{
    public string sceneToLoad; // Name of the scene to load

    void OnTriggerEnter(Collider other)
    {
        // Check if the player has collided with the box
        if (other.CompareTag("MainCamera"))
        {
            // Load the specified scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}

