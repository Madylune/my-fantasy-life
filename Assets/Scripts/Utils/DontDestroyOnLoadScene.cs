using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoadScene : MonoBehaviour
{
    public GameObject[] objects;

    public static DontDestroyOnLoadScene instance;

    private void Awake() 
    {
        if (instance != null)
        {
            Debug.LogWarning("The scene already has a DontDestroyOnLoadScene");
            return;
        }
        instance = this;

        foreach (var element in objects)
        {
            DontDestroyOnLoad(element);
        }
    }

    private void Update() 
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "MainMenu")
        {
            DestroyOnLoad();
        }
    }

    public void DestroyOnLoad()
    {
        foreach (var element in objects)
        {
            Destroy(element);
        }
    }

    public void RemoveFromDontDestroyOnLoad()
    {
        foreach (var element in objects)
        {
            SceneManager.MoveGameObjectToScene(element, SceneManager.GetActiveScene());
        }
    }
}
