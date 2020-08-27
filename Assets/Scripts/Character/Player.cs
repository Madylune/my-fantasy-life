using System.Collections;
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

    public IInteractable MyInteractable { get => interactable; set => interactable = value; }

    public int MyLevel { get => level; set => level = value; }
    public ExpBar MyExp { get => expBar; set => expBar = value; }

    [SerializeField]
    private Text moneyCount;

    [SerializeField]
    private ExpBar expBar;

    [SerializeField]
    private int level;

    [SerializeField]
    private Text levelText;

    [SerializeField]
    private GameObject dingEffect;

    public PlayerMovement movement;
    public PlayerAttack attack;
    public PlayerHealth health;

    private void Start() 
    {
        movement = GetComponent<PlayerMovement>();
        attack = GetComponent<PlayerAttack>();
        health = GetComponent<PlayerHealth>();

        MyMoney = 5000;

        MyExp.Initialize(0, Mathf.Floor(100 * MyLevel * Mathf.Pow(MyLevel, 0.5f)));

        levelText.text = MyLevel.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GainExp(100);
        }

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
        if (MyInteractable != null)
        {
            MyInteractable.Interact();
        }
    }

    public void GainExp(int exp)
    {
        MyExp.MyCurrentValue += exp;
        //CombatTextManager.MyInstance.CreateText(transform.position, exp.ToString(), CombatTextType.EXP, false);

        if (MyExp.MyCurrentValue >= MyExp.MyMaxValue)
        {
            StartCoroutine(Ding());
        }
    }

    private IEnumerator Ding()
    {
        while (!MyExp.IsFull)
        {
            yield return null;
        }

        MyLevel++;

        dingEffect.SetActive(true);
        CombatTextManager.MyInstance.CreateText(transform.position, "LEVEL UP", CombatTextType.LVL, false);

        levelText.text = MyLevel.ToString();

        MyExp.SetMaxValue(Mathf.Floor(100 * MyLevel * Mathf.Pow(MyLevel, 0.5f)));

        MyExp.MyCurrentValue = MyExp.MyOverflow;
        MyExp.Reset();

        if (MyExp.MyCurrentValue >= MyExp.MyMaxValue)
        {
            StartCoroutine(Ding());
        }

        yield return new WaitForSeconds(1);

        dingEffect.SetActive(false);
    }

    public void UpdateLevel()
    {
        levelText.text = MyLevel.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Interactable")
        {
            MyInteractable = collision.GetComponent<IInteractable>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Interactable")
        {
            if (MyInteractable != null)
            {
                MyInteractable.StopInteract();
                MyInteractable = null;
            }
        }
    }
}
