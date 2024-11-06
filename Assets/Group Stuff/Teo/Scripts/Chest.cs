using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour

   
{
    [SerializeField] private GameObject ball
        ;
 


    public GameObject Door2;
    public AudioSource door;
    public AudioClip open;
    [SerializeField] private GameObject chest1;
    [SerializeField] private GameObject chest2;
    [SerializeField] private GameObject lever;
    private void OnTriggerEnter(Collider gem)
    {
        if (gem.CompareTag("Gem"))
        {
            Destroy(Door2);
            door.PlayOneShot(open);
            chest1.SetActive(false);
            chest2.SetActive(true);
            lever.SetActive(true);

        }
    }

}
