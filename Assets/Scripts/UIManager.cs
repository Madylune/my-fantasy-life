using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{  
    private static UIManager instance;

    public static UIManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    [SerializeField]
    private Button[] actionButtons;

    private KeyCode action1, action2, action3, action4, action5, action6, action7, action8, action9;

    [SerializeField]
    private GameObject targetFrame;

    public Text targetName;
    public Text targetLevel;
    public Image targetIcon;
    public HealthBar targetHealthBar;

    private Stats stats;

    private void Start() 
    {
        stats = targetFrame.GetComponentInChildren<Stats>();
        if (stats != null)
        {
            targetHealthBar.SetMaxHealth(stats.maxHealth);
        }

        action1 = KeyCode.F1;
        action2 = KeyCode.F2;
        action3 = KeyCode.F3;
        action4 = KeyCode.F4;
        action5 = KeyCode.F5;
        action6 = KeyCode.F6;
        action7 = KeyCode.F7;
        action8 = KeyCode.F8;
        action9 = KeyCode.F9;
    }

    private void Update() 
    {   
        if (Input.GetKeyDown(action1))
        {   
            ActionButtonOnClick(0);
        }
        if (Input.GetKeyDown(action2))
        {   
            ActionButtonOnClick(1);
        }
        if (Input.GetKeyDown(action3))
        {   
            ActionButtonOnClick(2);
        }
        if (Input.GetKeyDown(action4))
        {   
            ActionButtonOnClick(3);
        }
        if (Input.GetKeyDown(action5))
        {   
            ActionButtonOnClick(4);
        }
        if (Input.GetKeyDown(action6))
        {   
            ActionButtonOnClick(5);
        }
        if (Input.GetKeyDown(action7))
        {   
            ActionButtonOnClick(6);
        }
        if (Input.GetKeyDown(action8))
        {   
            ActionButtonOnClick(7);
        }
        if (Input.GetKeyDown(action9))
        {   
            ActionButtonOnClick(8);
        }

        if (stats != null)
        {
            targetHealthBar.SetHealth(stats.currentHealth);
        }
    }

    private void ActionButtonOnClick(int buttonIndex)
    {
        actionButtons[buttonIndex].onClick.Invoke();
    }

    public void ShowTargetFrame(NPC target)
    {
        UpdateTargetFrame(target);

        targetFrame.SetActive(true);
    }

    public void HideTargetFrame()
    {
        targetFrame.SetActive(false);
        stats = null;
    }

    public void UpdateTargetFrame(NPC target)
    {
        stats = target.GetComponent<Stats>();

        targetName.text = stats.name;
        targetLevel.text = stats.level.ToString();
        targetIcon.sprite = stats.icon;
        
        targetHealthBar.SetHealth(stats.currentHealth);
    }
}
