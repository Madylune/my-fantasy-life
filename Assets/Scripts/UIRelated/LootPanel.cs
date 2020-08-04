using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootPanel : MonoBehaviour
{
    [SerializeField]
    private LootButton[] lootButtons;

    // Contains pages of items
    private List<List<Item>> pages = new List<List<Item>>();

    private int pageIndex = 0;

    [SerializeField]
    private Text pageNumber;

    [SerializeField]
    private GameObject nextBtn, previousBtn;

    // For debugging
    [SerializeField]
    private Item[] items;

    void Start()
    {
        // For debugging
        List<Item> tmp = new List<Item>();
        for (int i = 0; i < items.Length; i++)
        {
            tmp.Add(items[i]);
        }

        CreatePages(tmp);
    }

    public void CreatePages(List<Item> items)
    {
        List<Item> page = new List<Item>();

        for (int i = 0; i < items.Count; i++)
        {
            page.Add(items[i]);

            if (page.Count == 4 || i == items.Count -1) // Full items on page
            {
                // Add a new page
                pages.Add(page);
                page = new List<Item>();
            }
        }

        AddLoot();
    }

    private void AddLoot()
    {
        if (pages.Count > 0)
        {
            // Handle page numbers
            pageNumber.text = pageIndex + 1 + "/" + pages.Count;

            // Handle previous and next buttons
            previousBtn.SetActive(pageIndex > 0);
            nextBtn.SetActive(pages.Count > 1 && pageIndex < pages.Count - 1);

            for (int i = 0; i < pages[pageIndex].Count; i++) // Check what items 
            {
                if (pages[pageIndex][i] != null)
                {
                    // Set icon
                    lootButtons[i].MyIcon.sprite = pages[pageIndex][i].MyIcon;

                    // Set loot object
                    lootButtons[i].MyLoot = pages[pageIndex][i];

                    // Set active
                    lootButtons[i].gameObject.SetActive(true);

                    // Set title
                    string title = string.Format("<color={0}>{1}</color>", QualityColor.MyColors[pages[pageIndex][i].MyQuality], pages[pageIndex][i].MyTitle);
                    lootButtons[i].MyTitle.text = title;
                }
            }
        }
    }

    public void ClearPage()
    {
        foreach (LootButton btn in lootButtons)
        {
            btn.gameObject.SetActive(false);
        }
    }

    public void NextPage()
    {
        if (pageIndex < pages.Count -1) // Check if we have more next pages
        {
            pageIndex++;
            ClearPage();
            AddLoot();
        }
    }

    public void PreviousPage()
    {
        if (pageIndex > 0) // Check if we have more previous pages
        {
            pageIndex--;
            ClearPage();
            AddLoot();
        }
    }
}
