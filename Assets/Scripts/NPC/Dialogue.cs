using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueName;
    public string name;
    public Text dialogueText;
    [TextArea(3,10)]
    public string dialogue;
    public bool canOpenDialogue;

    private void Awake() 
    {
        dialogueBox = GameObject.FindGameObjectWithTag("DialogueBox");
        dialogueName = GameObject.FindGameObjectWithTag("DialogueName").GetComponent<Text>();
        dialogueText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<Text>();
    }

    private void Start() 
    {
        dialogueBox.SetActive(false);
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Space) && canOpenDialogue)
        {
            dialogueBox.SetActive(true);
            dialogueText.text = dialogue;
            dialogueName.text = "[ " + name.ToUpper() + " ]";
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            canOpenDialogue = true;
        }    
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            canOpenDialogue = false;
            dialogueBox.SetActive(false);
        }
    }
}
