using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour, IPlayerTrigger
{
    public void TriggerItem()
    {
        PlayerTriggerManager.instance.FinishLevel();
        Debug.Log("Level Complete");
    }
}
