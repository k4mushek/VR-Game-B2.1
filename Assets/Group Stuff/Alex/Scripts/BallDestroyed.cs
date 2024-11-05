using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDestroyed : MonoBehaviour
{
   private void OnTriggerEnter(Collider wall)
   {
      if (wall.gameObject.tag == "wall")
      {
         Destroy(gameObject);
      }
   }
}
