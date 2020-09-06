using UnityEngine;

public class DialogueScript : MonoBehaviour
{
    [SerializeField]
    private Dialogue dialogue;

    private bool canOpenDialogue;

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Space) && canOpenDialogue)
        {
            ShowDialogue();
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
        DialogueManager.MyInstance.StartDialogue(dialogue);
    }

    public void HideDialogue()
    {
        DialogueManager.MyInstance.EndDialogue();
    }
}
