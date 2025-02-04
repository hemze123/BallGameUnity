using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private float remainingTime;

    private void Awake()
    {
        // Singleton kontrolü
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    #region Save System
    public void SaveProgress(int levelIndex)
    {
        PlayerPrefs.SetInt("SavedLevel", levelIndex);
        PlayerPrefs.Save();
        Debug.Log("Progress Saved: Level " + levelIndex);
    }

    public int LoadProgress()
    {
        return PlayerPrefs.GetInt("SavedLevel", 0);
    }
    #endregion

    private void OnSceneUnloaded(Scene scene)
    {
        PlayerPrefs.Save();
    }

    #region Timer System
    public float RemainingTime
    {
        get => remainingTime;
        set => remainingTime = Mathf.Max(0, value);
    }

    public void ResetTimer(float newTime)
    {
        remainingTime = newTime;
    }

    public void DecreaseTime(float deltaTime)
    {
        remainingTime = Mathf.Max(0, remainingTime - deltaTime);
    }
    #endregion
}
