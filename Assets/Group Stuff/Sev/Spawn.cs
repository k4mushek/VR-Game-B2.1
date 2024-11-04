using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnMultipleDestroy : MonoBehaviour
{
    public GameObject[] targetObjects;      // Array of target objects to monitor for destruction
    public GameObject[] objectsToActivate;  // Array of objects to activate once all targets are destroyed

    private bool hasActivated = false;      // Tracks if objects have been activated

    void Update()
    {
        // Check if all target objects are destroyed and activation has not yet occurred
        if (!hasActivated && AllTargetsDestroyed())
        {
            // Activate each object in the objectsToActivate array
            foreach (GameObject obj in objectsToActivate)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }

            // Mark as activated to prevent re-triggering
            hasActivated = true;
        }
    }

    // Function to check if all target objects are destroyed
    private bool AllTargetsDestroyed()
    {
        foreach (GameObject target in targetObjects)
        {
            if (target != null)
            {
                return false;  // If any target still exists, return false
            }
        }
        return true;  // All targets are null, meaning they are destroyed
    }
}
