using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    GameObject door;

    void OnTriggerEnter(Collider Col)
    {
        door.transform.position += new Vector3(2, 0, 0);
    }

    private void OnTriggerExit(Collider Col)
    {
        door.transform.position += new Vector3(-2, 0, 0);
    }
}


