using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverSnap : MonoBehaviour
{
    [SerializeField] private GameObject lever;
    [SerializeField] private GameObject script;
    private bool snapped = false;
    [SerializeField] private GameObject snapparent; // the gameobject this transform will be snapped to
    private Vector3 offset; // the offset of this object's position from the parent
    [SerializeField] private GameObject pickable;

    private void Start()
    {
        
        
    }

    void Update()
    {

        if (snapped == true)
        {

            Destroy(script);
            transform.position = snapparent.transform.position + offset;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        LeverPull leverscrt = lever.GetComponent<LeverPull>();
        if (col.tag == "parentblock2")
        {
            snapped = true;
            snapparent = col.gameObject;
            leverscrt.enabled = true;
            Destroy(pickable);
            offset = transform.position - snapparent.transform.position; //store relation to parent
        }
    }
}
