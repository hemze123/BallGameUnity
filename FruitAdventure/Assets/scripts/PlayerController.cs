using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   void OnTriggerEnter(Collider other)
   {
       Debug.Log("OnTriggerEnter çalıştı! Çarpışan nesne: " + other.gameObject.name);
       IPlayerTrigger trigger = other.GetComponent<IPlayerTrigger>();
       if (trigger != null)
       {
           trigger.TriggerItem();
       }
   }

   void OnCollisionEnter(Collision other2) {
         IPlayerTrigger trigger = other2.gameObject.GetComponent<IPlayerTrigger>();
         if (trigger != null)
         {
              trigger.TriggerItem();
         }
    
   }
}
