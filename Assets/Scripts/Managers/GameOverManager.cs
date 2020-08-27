using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public string savePoint;

    public static GameOverManager instance;

    private void Awake() 
    {
        if (instance != null)
        {
            Debug.LogWarning("The scene already has a GameOverManager");
            return;
        }
        instance = this;
    }

    public void OnPlayerDeath()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f; // Freeze time and game
    }

    public void CloseGameOverPanel()
    {
        gameOverUI.SetActive(false);
    }

    public void Respawn()
    {
        CloseGameOverPanel();
        SceneManager.LoadScene(savePoint);
        Time.timeScale = 1f;
    }
}
