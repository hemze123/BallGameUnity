using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerTriggerManager : MonoBehaviour
{
    public static PlayerTriggerManager instance { get; private set; }

    public Action<int> OnCoinCollected;
    public Action<int> OnHealthCollected;

    [Header("Player Stats")]
    [SerializeField] private int coinValue = 0;
    [SerializeField] private int healthValue = 3;
    [SerializeField] private int maxHealth = 3;

    [Header("Timer Settings")]
    [SerializeField] private float oyunSuresi = 60f;
    [SerializeField] private TextMeshProUGUI timerText;
    private float kalanSure;

    [Header("UI Panels")]
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private GameObject finishParticleEffect;

    [Header("Buttons")]
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button nextLevelButton;

    private bool isGameActive = true;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        InitializeUI();
        InitializeGame();
        healthValue = maxHealth;

        // Butonlara dinleyici ekle
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartLevel);
        }
        if (menuButton != null)
        {
            menuButton.onClick.AddListener(LoadMenu);
        }
        if (nextLevelButton != null)
        {
            nextLevelButton.onClick.AddListener(LoadNextLevel);
        }
    }

    private void InitializeUI()
    {
        // UI bileşenlerini başlat
        if (deathPanel != null)
        {
            deathPanel.SetActive(false);
        }
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
        if (finishPanel != null)
        {
            finishPanel.SetActive(false);
        }
        if (finishParticleEffect != null)
        {
            finishParticleEffect.SetActive(false);
        }
    }

    void Update()
    {
        if (!isGameActive) return;
        UpdateTimer();
    }

    private void InitializeGame()
    {
        kalanSure = oyunSuresi;
        Time.timeScale = 1;
        isGameActive = true;
    }

    private void UpdateTimer()
    {
        if (kalanSure > 0)
        {
            kalanSure -= Time.deltaTime;
            if (timerText != null)
            {
                timerText.text = " " + Mathf.Ceil(kalanSure).ToString();
            }
        }
        else
        {
            ShowGameOver();
        }
    }

    // Yeni metod: Kalan süreyi döndür
    public float GetRemainingTime()
    {
        return kalanSure;
    }

    #region Collectibles
    public void AddCoin()
    {
        coinValue++;
        OnCoinCollected?.Invoke(coinValue);
        Debug.Log("Coin Collected: " + coinValue);
    }

    public void AddHealth()
    {
        if (healthValue < maxHealth)
        {
            healthValue++;
            OnHealthCollected?.Invoke(healthValue);
            Debug.Log("Health Collected: " + healthValue);
        }
    }

    public void RemoveHealth()
    {
        healthValue--;
        OnHealthCollected?.Invoke(healthValue);
        Debug.Log("Health Removed: " + healthValue);

        if (healthValue <= 0)
        {
            ShowGameOver();
        }
    }
    #endregion

    #region Game State Management
    public void ShowGameOver()
    {
        isGameActive = false;
        if (deathPanel != null)
        {
            deathPanel.SetActive(true);
        }
        Time.timeScale = 0;
    }

    public void ShowWinLevel()
    {
        if (finishParticleEffect) finishParticleEffect.SetActive(true);
        StartCoroutine(ShowPanelDelayed(finishPanel));
        GameManager.instance.SaveProgress(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator ShowPanelDelayed(GameObject panel)
    {
        yield return new WaitForSeconds(1f);
        if (panel != null)
        {
            panel.SetActive(true);
            Time.timeScale = 0;
        }
    }
    #endregion

    #region Trigger Events
    public void DeathZone()
    {
        Debug.Log("Player Entered Death Zone");
        ShowGameOver();
    }

    public void FinishLevel()
    {
        Debug.Log("Level Complete");
        ShowWinLevel();
    }
    #endregion

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
    }

    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Tüm seviyeler tamamlandı!");
        }
    }
}
