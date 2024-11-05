using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    Animator _slide;

    private void Start()
    {
        _slide = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider Col)
    {
       if (Col.gameObject.CompareTag("Door"))
        {
            _slide.SetTrigger("plate");
        }
    }

    private void OnTriggerExit(Collider Col)
    {
        if (Col.gameObject.CompareTag("Door"))
        {
            _slide.SetTrigger("plate");
        }
    }
}


