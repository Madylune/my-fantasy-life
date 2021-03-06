﻿using System;
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
    private ActionButton[] actionButtons;

    [SerializeField]
    private GameObject targetFrame;

    [SerializeField]
    private GameObject tooltip;

    [SerializeField]
    private CanvasGroup characterPanel;

    private Text tooltipText;

    [SerializeField]
    private RectTransform tooltipRect;

    [SerializeField]
    private Text targetName;

    [SerializeField]
    private Text targetLevel;

    [SerializeField]
    private Image targetIcon;

    [SerializeField]
    private HealthBar targetHealthBar;

    private Stats stats;

    private GameObject[] keybindsButtons;

    private void Awake() 
    {
        keybindsButtons = GameObject.FindGameObjectsWithTag("Keybind");

        tooltipText = tooltip.GetComponentInChildren<Text>();
    }

    private void Start() 
    {   
        stats = targetFrame.GetComponentInChildren<Stats>();
        if (stats != null)
        {
            targetHealthBar.SetMaxHealth(stats.maxHealth);
        }
    }

    private void Update() 
    {   
        if (stats != null)
        {
            targetHealthBar.SetHealth(stats.currentHealth);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TogglePanel(characterPanel);
        }
    }


    public void ShowTargetFrame(Enemy target)
    {
        UpdateTargetFrame(target);

        stats = target.GetComponent<Stats>();

        targetName.text = stats.name;
        targetLevel.text = target.MyLevel.ToString();
        targetIcon.sprite = stats.icon;

        if (target.MyLevel >= Player.MyInstance.MyLevel + 5)
        {
            targetLevel.color = Color.red;
        }
        else if (target.MyLevel == Player.MyInstance.MyLevel + 3 || target.MyLevel == Player.MyInstance.MyLevel + 4)
        {
            targetLevel.color = new Color32(255, 124, 0, 255); //Orange
        }
        else if (target.MyLevel >= Player.MyInstance.MyLevel - 2 && target.MyLevel <= Player.MyInstance.MyLevel + 2)
        {
            targetLevel.color = Color.yellow;
        }
        else if (target.MyLevel <= Player.MyInstance.MyLevel - 3 && target.MyLevel > XPManager.CalculateGrayLevel())
        {
            targetLevel.color = Color.green;
        }
        else
        {
            targetLevel.color = Color.white;
        }

        targetFrame.SetActive(true);
    }

    public void HideTargetFrame()
    {
        targetFrame.SetActive(false);

        stats = null;
    }

    public void UpdateTargetFrame(Enemy target)
    {
        stats = target.GetComponent<Stats>();

        targetHealthBar.SetHealth(stats.currentHealth);
    }

    public void TogglePanel(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;
        canvasGroup.blocksRaycasts = canvasGroup.alpha > 0 ? true : false;
    }

    public void UpdateKeyText(string key, KeyCode code)
    {
        Text tmp = Array.Find(keybindsButtons, x => x.name == key).GetComponentInChildren<Text>();
        tmp.text = code.ToString();
    }

    public void ClickActionButton(string buttonName)
    {
        Array.Find(actionButtons, x => x.gameObject.name == buttonName).MyButton.onClick.Invoke();
    }

    public void UpdateStackSize(IClickable clickable)
    {
        if (clickable.MyCount > 1)
        {
            clickable.MyStackText.text = clickable.MyCount.ToString();
            clickable.MyStackText.color = Color.white;
            clickable.MyIcon.color = Color.white;
        }
        else
        {
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
            clickable.MyIcon.color = Color.white;
        }

        if (clickable.MyCount == 0) // Hide the icon and stack text if the slot is empty
        {
            clickable.MyIcon.color = new Color(0, 0, 0, 0);
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
        }
    }

    public void ClearStackCount(IClickable clickable)
    {
        clickable.MyStackText.color = new Color(0, 0, 0, 0);
        clickable.MyIcon.color = Color.white;
    }

    public void ShowTooltip(Vector2 pivot, Vector3 position, IDescribable description)
    {
        tooltipRect.pivot = pivot;
        tooltip.SetActive(true);
        tooltip.transform.position = position;
        tooltipText.text = description.GetDescription();
    }

    public void HideTooltip()
    {
        tooltip.SetActive(false);
    }

    public void RefreshTooltip(IDescribable description)
    {
        tooltipText.text = description.GetDescription();
    }
}
