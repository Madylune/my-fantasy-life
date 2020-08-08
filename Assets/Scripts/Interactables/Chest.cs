using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite openSprite, closeSprite;

    private bool isOpen;

    [SerializeField]
    private CanvasGroup chestPanel;

    public void Interact()
    {
        if (isOpen)
        {
            StopInteract();
        }
        else
        {
            isOpen = true;
            spriteRenderer.sprite = openSprite;
            chestPanel.alpha = 1;
            chestPanel.blocksRaycasts = true;
        }
    }

    public void StopInteract()
    {
        isOpen = false;
        spriteRenderer.sprite = closeSprite;
        chestPanel.alpha = 0;
        chestPanel.blocksRaycasts = false;
    }
}
