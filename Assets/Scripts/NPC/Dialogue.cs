using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueName;
    public string characterName;
    public Text dialogueText;
    [TextArea(3,10)]
    public string dialogue;

    private bool canOpenDialogue;

    [SerializeField]
    private CanvasGroup dialogueCanvas;

    private void Awake() 
    {
        dialogueBox = GameObject.FindGameObjectWithTag("DialogueBox");
        dialogueName = GameObject.FindGameObjectWithTag("DialogueName").GetComponent<Text>();
        dialogueText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<Text>();
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Space) && canOpenDialogue)
        {
            ShowDialogue();
            dialogueText.text = dialogue;
            dialogueName.text = "[ " + characterName.ToUpper() + " ]";
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
            HideDialogue();
        }
    }

    public void ShowDialogue()
    {
        dialogueCanvas.alpha = 1;
        dialogueCanvas.blocksRaycasts = true;
    }

    public void HideDialogue()
    {
        dialogueCanvas.alpha = 0;
        dialogueCanvas.blocksRaycasts = false;
    }
}
