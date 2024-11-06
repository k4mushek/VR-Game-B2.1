using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapping : MonoBehaviour
{
    [SerializeField] private GameObject script;
    bool snapped = false;
    public GameObject snapparent; // the gameobject this transform will be snapped to
    public Vector3 offset; // the offset of this object's position from the parent

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
        if (col.tag == "parentblock")
        {
            snapped = true;
            snapparent = col.gameObject;
            offset = transform.position - snapparent.transform.position; //store relation to parent
        }
    }

}








