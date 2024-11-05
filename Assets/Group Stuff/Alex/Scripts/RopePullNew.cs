using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePullNew : MonoBehaviour
{
    [SerializeField]  private GameObject ball;
    [SerializeField] private GameObject spawnpoint;
    [SerializeField]
    GameObject ceilingDoor;
    [SerializeField] Transform ropeSegment;
    [SerializeField] float activationHeight;

    private bool isDoorOpen = false;

    // Update is called once per frame
    private void Update()
    { 
        float triggerY = ropeSegment.position.y;

        if (triggerY < activationHeight && !isDoorOpen)
        {
            OpenDoor();
        }

        else if (triggerY >= activationHeight && isDoorOpen)
        {
            CloseDoor();
        }

        if (ball == null)
        {
            Vector3 point = new Vector3(1, 1, 1);
            Instantiate(ball, point, Quaternion.identity);


        }
        
        
        
    }

    private void OpenDoor()
    {
        ceilingDoor.transform.position += new Vector3(2, 0, 0);
        isDoorOpen = true;
        Debug.Log("Ceiling Door Opened");
    }

    private void CloseDoor()
    {
        
        ceilingDoor.transform.position += new Vector3(-2, 0, 0);
        isDoorOpen = false;
        Debug.Log("Ceiling Door Closed");
    }
}
