using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{
    private static CurrentSceneManager instance;

    public static CurrentSceneManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CurrentSceneManager>();
            }
            return instance;
        }
    }

    [SerializeField]
    private Vector3 respawnPoint;

    private void Awake() 
    {
        respawnPoint = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}
