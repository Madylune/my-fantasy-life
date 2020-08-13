using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour, IInteractable
{
    [SerializeField]
    private CanvasGroup vendorPanel;

    public void Interact()
    {
        vendorPanel.alpha = 1;
        vendorPanel.blocksRaycasts = true;
    }

    public void StopInteract()
    {
        vendorPanel.alpha = 0;
        vendorPanel.blocksRaycasts = false;
    }
}
