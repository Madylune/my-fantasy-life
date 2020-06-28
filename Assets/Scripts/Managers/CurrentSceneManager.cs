using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{
    public bool isPlayerPresentByDefault = false;

    public static CurrentSceneManager instance;

    private void Awake() 
    {
        if (instance != null)
        {
            Debug.LogWarning("The scene already has a CurrentSceneManager");
            return;
        }
        instance = this;
    }
}
