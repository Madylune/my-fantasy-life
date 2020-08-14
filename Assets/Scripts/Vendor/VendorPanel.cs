using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorPanel : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup vendorPanel;

    [SerializeField]
    private VendorButton[] vendorButtons;

    public void CreatePages(VendorItem[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            vendorButtons[i].AddItem(items[i]);
        }
    }

    public void Open()
    {
        vendorPanel.alpha = 1;
        vendorPanel.blocksRaycasts = true;
    }

    public void Close()
    {
        vendorPanel.alpha = 0;
        vendorPanel.blocksRaycasts = false;
    }
}
