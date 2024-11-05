using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapping2 : MonoBehaviour
{
    [SerializeField] private GameObject script;
    private bool snapped = false;
    [SerializeField] private GameObject snapparent; // the gameobject this transform will be snapped to
    private Vector3 offset; // the offset of this object's position from the parent
    [SerializeField] private GameObject pickable;
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
        if (col.tag == "parentblock1")
        {
            snapped = true;
            snapparent = col.gameObject;
            
            offset = transform.position - snapparent.transform.position; //store relation to parent
        }
    }
}
