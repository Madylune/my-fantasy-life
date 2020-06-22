using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private Inventory inventory;
    public int i;
    public Button removeButton;

    private void Start() 
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void Update() 
    {
        if (transform.childCount <= 1)
        {
            inventory.isFull[i] = false;
            removeButton.interactable = false;
        }
        if (transform.childCount == 2)
        {
            removeButton.interactable = true;
        }
    }
    
    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(GetComponent<Transform>().GetChild(1).gameObject);
        }
    }
}
