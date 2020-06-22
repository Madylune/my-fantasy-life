using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSettings : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
