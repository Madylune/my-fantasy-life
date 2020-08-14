using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VendorPanel : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup vendorPanel;

    [SerializeField]
    private VendorButton[] vendorButtons;

    [SerializeField]
    private Text pageNumber;

    private List<List<VendorItem>> pages = new List<List<VendorItem>>();

    private int pageIndex;

    private int itemsPerPage = 4;

    public void CreatePages(VendorItem[] items)
    {
        List<VendorItem> page = new List<VendorItem>();

        for (int i = 0; i < items.Length; i++)
        {
            page.Add(items[i]);

            if (page.Count == itemsPerPage || i == items.Length - 1)
            {
                pages.Add(page);
                page = new List<VendorItem>();
            }
        }

        AddItems();
    }

    public void AddItems()
    {
        pageNumber.text = pageIndex + 1 + "/" + pages.Count;

        if (pages.Count > 0)
        {
            for (int i = 0; i < pages[pageIndex].Count; i++)
            {
                if (pages[pageIndex][i] != null)
                {
                    vendorButtons[i].AddItem(pages[pageIndex][i]);
                }
            }
        }
    }

    public void NextPage()
    {
        if (pageIndex < pages.Count)
        {
            pageIndex++;
            ClearPage();
            AddItems();
        }
    }

    public void PreviousPage()
    {
        if (pageIndex > 0)
        {
            pageIndex--;
            ClearPage();
            AddItems();
        }
    }

    public void ClearPage()
    {
        foreach (VendorButton btn in vendorButtons)
        {
            btn.gameObject.SetActive(false);
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
