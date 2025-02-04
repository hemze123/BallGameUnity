using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IPlayerTrigger
{
    public void TriggerItem()
    {
        PlayerTriggerManager.instance.RemoveHealth();
        Debug.Log(" Health - 1");
        Destroy(gameObject);
    }
}
