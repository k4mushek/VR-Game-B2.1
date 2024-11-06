using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationRaycast : MonoBehaviour
{
    [SerializeField] private GameObject door;
    Animator _Anim;
    
    
    
    [SerializeField] private GameObject[] snaps;

    void Update()
    {
        if( snaps == null)
        {
            Destroy(door);
        }
        Debug.Log("CYKA");
    }
}
