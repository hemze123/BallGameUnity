using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour, IPlayerTrigger
{
    public void TriggerItem()
    {
        PlayerTriggerManager.instance.DeathZone();
        Debug.Log("Player Died, DeathZone , Restarting Level");
    }
}

