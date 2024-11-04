using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsRig : MonoBehaviour
{
    public Transform playerHead;
    public CapsuleCollider playerCollider;

    public float playerHeightMin = 0.5f;
    public float playerHeightMax = 2;

    // Update is called once per frame
    void FixedUpdate()
    {
        playerCollider.height = Mathf.Clamp(playerHead.localPosition.y, playerHeightMin, playerHeightMax);
        playerCollider.center = new Vector3(playerHead.localPosition.x, playerCollider.height / 2, playerHead.localPosition.z);

    }
}
