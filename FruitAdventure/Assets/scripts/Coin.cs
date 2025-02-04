using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IPlayerTrigger
{
    public void TriggerItem()
    {
        PlayerTriggerManager.instance.AddCoin();
        Debug.Log("Coin Collected: +1 ");
        Destroy(gameObject);
    }
}

