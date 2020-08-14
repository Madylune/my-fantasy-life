using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootPanel : MonoBehaviour
{
    private static LootPanel instance;

    public static LootPanel MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LootPanel>();
            }
            return instance;
        }
    }

    [SerializeField]
    private LootButton[] lootButtons;

    private CanvasGroup canvasGroup;

    // Contains pages of items
    private List<List<Item>> pages = new List<List<Item>>();

    private List<Item> droppedLoot = new List<Item>();

    private int pageIndex = 0;

    [SerializeField]
    private Text pageNumber;

    [SerializeField]
    private GameObject nextBtn, previousBtn;

    // For debugging
    [SerializeField]
    private Item[] items;

    private int itemsPerPage = 4;

    public bool IsOpen
    {
        get
        {
            return canvasGroup.alpha > 0;
        }
    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void CreatePages(List<Item> items)
    {
        if (!IsOpen)
        {
            List<Item> page = new List<Item>();

            droppedLoot = items;

            for (int i = 0; i < items.Count; i++)
            {
                page.Add(items[i]);

                if (page.Count == itemsPerPage || i == items.Count -1) // Full items on page
                {
                    // Add a new page
                    pages.Add(page);
                    page = new List<Item>();
                }
            }

            AddLoot();

            OpenPanel();

        }
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

    public void TakeLoot(Item loot)
    {
        pages[pageIndex].Remove(loot);

        droppedLoot.Remove(loot);

        if (pages[pageIndex].Count == 0) // Go back to the previous page if no item
        {
            // Remove the empty page
            pages.Remove(pages[pageIndex]);

            if (pageIndex == pages.Count && pageIndex > 0) // If the page is the last page and is not the first one
            {
                pageIndex--;
            }

            AddLoot();
        }
    }

    public void ClosePanel()
    {
        pages.Clear();

        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;

        ClearPage();
    }


    public void OpenPanel()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
}
