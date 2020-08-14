using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private static Player instance;

    public static Player MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
            }
            return instance;
        }
    }

    private IInteractable interactable; // Represents the thing he can interact with (enemy, npc, bank, ...)

    public int MyMoney { get; set; }

    [SerializeField]
    private Text moneyCount;

    public PlayerMovement movement;
    public PlayerAttack attack;
    public PlayerHealth health;

    private void Start() 
    {
        movement = GetComponent<PlayerMovement>();
        attack = GetComponent<PlayerAttack>();
        health = GetComponent<PlayerHealth>();

        MyMoney = 5000;
    }

    private void Update()
    {
        foreach (string action in KeybindsManager.MyInstance.ActionBinds.Keys)
        {
            if (Input.GetKeyDown(KeybindsManager.MyInstance.ActionBinds[action]))
            {
                UIManager.MyInstance.ClickActionButton(action);
            }
        }

        moneyCount.text = MyMoney.ToString();
    }

    public void Interact()
    {
        if (interactable != null)
        {
            interactable.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Interactable")
        {
            interactable = collision.GetComponent<IInteractable>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Interactable")
        {
            if (interactable != null)
            {
                interactable.StopInteract();
                interactable = null;
            }
        }
    }
}
