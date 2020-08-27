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

    [SerializeField]
    private BagScript bag;

    private List<Item> items;

    public List<Item> MyItems { get => items; set => items = value; }
    public BagScript MyBag { get => bag; set => bag = value; }

    private void Awake()
    {
        items = new List<Item>();
    }

    public void Interact()
    {
        if (isOpen)
        {
            StopInteract();
        }
        else
        {
            AddItems();
            isOpen = true;
            spriteRenderer.sprite = openSprite;
            chestPanel.alpha = 1;
            chestPanel.blocksRaycasts = true;
        }
    }

    public void StopInteract()
    {
        if (isOpen)
        {
            StoreItems();
            bag.Clear();
            isOpen = false;
            spriteRenderer.sprite = closeSprite;
            chestPanel.alpha = 0;
            chestPanel.blocksRaycasts = false;
        }
    }

    public void AddItems()
    {
        if (items != null)
        {
            foreach (Item item in items)
            {
                item.MySlot.AddItem(item);
            }
        }
    }

    public void StoreItems()
    {
        items = MyBag.GetItems(); 
    }
}
