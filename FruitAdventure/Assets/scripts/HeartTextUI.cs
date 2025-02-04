using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartTextUI : MonoBehaviour
{
   [SerializeField] private TMPro.TextMeshProUGUI heartText;

    // Start is called before the first frame update
    void Start()
    {
      PlayerTriggerManager.instance.OnHealthCollected += UpdateHeartText;
    }

    void UpdateHeartText(int heartValue)
    {
        UpdateUI(heartValue);
    }

    void UpdateUI(int heartValue)
    {
        heartText.text = " " + heartValue.ToString();
    }
}
