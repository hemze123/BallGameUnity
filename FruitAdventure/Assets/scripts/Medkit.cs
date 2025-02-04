using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour, IPlayerTrigger
{
    public void TriggerItem()
    {
        PlayerTriggerManager.instance.AddHealth();
        Debug.Log("Health + 1");
        Destroy(gameObject);
    }
}
