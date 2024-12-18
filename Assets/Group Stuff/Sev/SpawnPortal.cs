using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////public class SpawnPortal : MonoBehaviour
////{
////    public GameObject[] targetObjects;      // Array of target objects to monitor for destruction
////    public GameObject[] objectsToActivate;  // Array of objects to activate once all targets are destroyed

////    private bool hasActivated = false;      // Tracks if objects have been activated
////    public Vector3 targetPosition;
////    public float speed = 1.0f;
////    void Update()
////    {
////        // Check if all target objects are destroyed and activation has not yet occurred
////        if (!hasActivated && AllTargetsDestroyed())
////        {
////            // Activate each object in the objectsToActivate array
////            foreach (GameObject obj in objectsToActivate)
////            {
////                if (obj != null)
////                {
////                    obj.SetActive(true);
////                }
////            }
////            // Mark as activated to prevent re-triggering
////            hasActivated = true;
////        }
//////        if (AllTargetsDestroyed())
//////            {
////                float step = speed * Time.deltaTime;
////                // Move the object gradually towards the target position
////                Vector3.MoveTowards(transform.position, targetPosition, 2);

//////            }
////    }

////    // Function to check if all target objects are destroyed
////    private bool AllTargetsDestroyed()
////    {
////        foreach (GameObject target in targetObjects)
////        {
////            if (target != null)
////            {
////                return false;  // If any target still exists, return false
////            }
////        }
////        return true;  // All targets are null, meaning they are destroyed
////    }
////}
public class SpawnPortal : MonoBehaviour
{
    public Vector3 target; // Set the target position in the Inspector
    public float speed = 5f; // Speed at which the object moves to the target

    void Update()
    {
        // Move the object towards the target position
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }
}

