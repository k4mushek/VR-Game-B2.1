using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.Input;

public class RopePull : MonoBehaviour
{
    public GameObject ceilingDoor;
    public float pullForce = 10f;
    public float pullDistance = 2f;

    private bool isPulled = false;

    private void Update()
    {
        if (isPulled)
        {
            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
            {
                PullRope();
            }
            else
            {
                StopPulling();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPulled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopPulling();
        }
    }

    private void PullRope()
    {
        if (ceilingDoor != null)
        {
            ceilingDoor.transform.position += new Vector3(2, 0, 0);
        }
    }

    private void StopPulling()
    {
        isPulled = false;
    }
}