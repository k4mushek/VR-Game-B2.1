using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour

    
{ 
    public GameObject Door2;
    public AudioSource door;
    public AudioClip open;

    private void OnTriggerEnter(Collider gem)
    {
        if (gem.CompareTag("Gem"))
        {
            Destroy(Door2);
            door.PlayOneShot(open);
        }
    }

}
