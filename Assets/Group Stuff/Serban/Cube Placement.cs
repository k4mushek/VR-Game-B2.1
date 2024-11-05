using System;
using System.Collections;
using System.Collections.Generic;
using Meta.XR.BuildingBlocks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class CubePlacement : MonoBehaviour
{ 
     bool snapped = false; 
     public GameObject snapparent;  
     public Vector3 offset; 
 void Update()
 {

//insert teo' part of the code here 

 }
    void OnTriggerEnter(Collider col)
    { 
      if(col.tag == "parentblock")
      {
        snapped = true; 
        snapparent = col.gameObject;
        offset = transform.position - snapparent.transform.position;  
      }
    }
}

