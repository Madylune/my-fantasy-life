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
    }

    public void CloseGameOverPanel()
    {
        gameOverUI.SetActive(false);
    }

    public void Respawn()
    {
        SceneManager.LoadScene(savePoint);
        DontDestroyOnLoadScene.instance.DestroyOnLoad();
        CloseGameOverPanel();
    }
}
