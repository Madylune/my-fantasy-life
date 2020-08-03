using UnityEngine;

public class LootPanel : MonoBehaviour
{
    [SerializeField]
    private LootButton[] lootButtons;

    // For debugging
    [SerializeField]
    private Item[] items;

    void Start()
    {
        AddLoot();
    }

    private void AddLoot()
    {
        int itemIndex = 2;

        // Set icon
        lootButtons[itemIndex].MyIcon.sprite = items[itemIndex].MyIcon;

        // Set loot object
        lootButtons[itemIndex].MyLoot = items[itemIndex];

        // Set active
        lootButtons[itemIndex].gameObject.SetActive(true);

        // Set title
        string title = string.Format("<color={0}>{1}</color>", QualityColor.MyColors[items[itemIndex].MyQuality], items[itemIndex].MyTitle);
        lootButtons[itemIndex].MyTitle.text = title;
    }
}
