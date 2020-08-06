using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    [SerializeField]
    private Loot[] loot;

    private List<Item> droppedItems = new List<Item>();

    private bool rolled;

    public void ShowLoot()
    {
        if (!rolled)
        {
            RollLoot();
        }

        LootPanel.MyInstance.CreatePages(droppedItems);
    }

    private void RollLoot()
    {
        foreach (Loot item in loot)
        {
            int roll = Random.Range(0, 100);

            if (roll <= item.MyDropChance)
            {
                droppedItems.Add(item.MyItem);
            }
        }

        rolled = true;
    }
}
