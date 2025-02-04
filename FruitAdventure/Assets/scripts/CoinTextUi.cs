using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTextUi : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI coinText;
    // Start is called before the first frame update
    void Start()
    {
        PlayerTriggerManager.instance.OnCoinCollected += UpdateCoinText;
    }

   void UpdateCoinText(int coinValue)
    {
        UpdateUI(coinValue);
    }

    void UpdateUI(int coinValue)
    {
        coinText.text = "Coins: " + coinValue.ToString();
    }
}
