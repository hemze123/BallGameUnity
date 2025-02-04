using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // TextMeshPro kullanıyorsanız

public class LevelUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText; // Level yazısını gösterecek TextMeshPro referansı

    private void Start()
    {
        if (levelText == null)
        {
            Debug.LogWarning("Level Text is not assigned in the inspector!");
            return;
        }

        UpdateLevelText();
    }

    // Mevcut level numarasını alıp UI'da gösterir
    private void UpdateLevelText()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex ; // Level 1'den başlasın
        levelText.text = "Level " + currentLevel;
    }
}
