using UnityEngine;
using TMPro; // TextMeshPro kütüphanesini ekleyin

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText; // Zamanlayıcı metni için TextMeshPro referansı
    private PlayerTriggerManager playerTriggerManager;

    // Start is called before the first frame update
    void Start()
    {
        // PlayerTriggerManager referansını al
        playerTriggerManager = PlayerTriggerManager.instance;

        // Timer Text kontrolü
        if (timerText == null)
        {
            timerText = GetComponent<TextMeshProUGUI>();
        }

        if (timerText == null)
        {
            Debug.LogError("Timer Text referansı bulunamadı!");
            enabled = false; // Script'i devre dışı bırak
            return;
        }

        // Başlangıçta zamanlayıcıyı güncelle
        UpdateTimerUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTriggerManager != null && timerText != null)
        {
            UpdateTimerUI();
        }
    }

    private void UpdateTimerUI()
    {
        float kalanSure = playerTriggerManager.GetRemainingTime();
        timerText.text = " " + Mathf.Ceil(kalanSure).ToString();
    }
}

