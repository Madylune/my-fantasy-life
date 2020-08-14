using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour, IInteractable
{
    [SerializeField]
    private VendorItem[] items;
     
    [SerializeField]
    private VendorPanel vendorPanel;

    public bool IsOpen { get; set; }

    public void Interact()
    {
        if (!IsOpen)
        {
            IsOpen = true;
            vendorPanel.CreatePages(items);
            vendorPanel.Open(this);
        }
    }

    public void StopInteract()
    {
        if (IsOpen)
        {
            IsOpen = false;
            vendorPanel.Close();
        }
    }
}
