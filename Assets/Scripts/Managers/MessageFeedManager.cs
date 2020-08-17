using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageFeedManager : MonoBehaviour
{
    private static MessageFeedManager instance;

    public static MessageFeedManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MessageFeedManager>();
            }
            return instance;
        }
    }

    [SerializeField]
    private GameObject messagePrefab;

    public void WriteMessage(string message)
    {
        GameObject go = Instantiate(messagePrefab, transform);

        go.GetComponent<Text>().text = message;

        go.transform.SetAsFirstSibling();
        Destroy(go, 2);
    }
}
