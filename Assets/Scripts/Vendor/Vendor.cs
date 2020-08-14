using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour, IInteractable
{
    [SerializeField]
    private VendorItem[] items;
     
    [SerializeField]
    private VendorPanel vendorPanel;

    public void Interact()
    {
        vendorPanel.CreatePages(items);
        vendorPanel.Open();
    }

    public void StopInteract()
    {
        vendorPanel.Close();
    }
}
