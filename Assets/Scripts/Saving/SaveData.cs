using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public PlayerData MyPlayerData { get; set; }

    public List<ChestData> MyChestData { get; set; }

    public InventoryData MyInventoryData { get; set; }

    public List<EquipmentData> MyEquipmentData { get; set; }

    public List<ActionBarData> MyActionBarData { get; set; }

    public List<QuestData> MyQuestData { get; set; }

    public List<QuestGiverData> MyQuestGiverData { get; set; }

    public SaveData()
    {
        MyChestData = new List<ChestData>();
        MyInventoryData = new InventoryData();
        MyActionBarData = new List<ActionBarData>();
        MyEquipmentData = new List<EquipmentData>();
        MyQuestData = new List<QuestData>();
        MyQuestGiverData = new List<QuestGiverData>();
    }
}

[Serializable]
public class PlayerData
{
    public int MyLevel { get; set; }
    public float MyXp { get; set; }
    public float MyMaxXp { get; set; }
    public float MyHealth { get; set; }
    public float MyMaxHealth { get; set; }
    public float MyMana { get; set; }
    public float MyMaxMana { get; set; }
    public float MyX { get; set; }
    public float MyY { get; set; }


    public PlayerData(int level, float xp, float maxXp, float health, float maxHealth, float mana, float maxMana, Vector2 position)
    {
        MyLevel = level;
        MyXp = xp;
        MyMaxXp = maxXp;
        MyHealth = health;
        MyMaxHealth = maxHealth;
        MyMana = mana;
        MyMaxMana = maxMana;
        MyX = position.x;
        MyY = position.y;
    }
}

[Serializable]
public class ItemData
{
    public string MyTitle { get; set; }
    public int MyStackCount { get; set; }
    public int MySlotIndex { get; set; }
    public int MyBagIndex { get; set; }

    public ItemData(string title, int stackCount = 0, int slotIndex = 0, int bagIndex = 0)
    {
        MyTitle = title;
        MyStackCount = stackCount;
        MySlotIndex = slotIndex;
        MyBagIndex = bagIndex;
    }
}

[Serializable]
public class ChestData
{
    public string MyName { get; set; }
    public List<ItemData> MyItems { get; set; }

    public ChestData(string name)
    {
        MyName = name;
        MyItems = new List<ItemData>();
    }
}

[Serializable]
public class InventoryData
{
    public List<BagData> MyBags { get; set; }
    public List<ItemData> MyItems { get; set; }

    public InventoryData()
    {
        MyBags = new List<BagData>();
        MyItems = new List<ItemData>();
    }
}

[Serializable]
public class BagData
{
    public int MySlotCount { get; set; }
    public int MyBagIndex { get; set; }

    public BagData(int count, int index)
    {
        MySlotCount = count;
        MyBagIndex = index;
    }
}

[Serializable]
public class EquipmentData
{
    public string MyTitle { get; set; }
    public string MyType { get; set; }

    public EquipmentData(string title, string type)
    {
        MyTitle = title;
        MyType = type;
    }
}

[Serializable]
public class ActionBarData
{
    public string MyAction { get; set; }
    public bool IsItem { get; set; }
    public int MyIndex { get; set; }

    public ActionBarData(string action, bool isItem, int index)
    {
        MyAction = action;
        IsItem = isItem;
        MyIndex = index;
    }
}

[Serializable]
public class QuestData
{
    public string MyTitle { get; set; }
    public string MyDescription { get; set; }
    public CollectObjective[] MyCollectObjectives { get; set; }
    public KillObjective[] MyKillObjectives { get; set; }
    public int MyQuestGiverId { get; set; }

    public QuestData(string title, string description, CollectObjective[] collectObjectives, KillObjective[] killObjectives, int questGiverId)
    {
        MyTitle = title;
        MyDescription = description;
        MyCollectObjectives = collectObjectives;
        MyKillObjectives = killObjectives;
        MyQuestGiverId = questGiverId;
    }
}

[Serializable]
public class QuestGiverData
{
    public List<string> MyCompletedQuests { get; set; }
    public int MyQuestGiverId { get; set; }

    public QuestGiverData(int questGiverId, List<string> completedQuests)
    {
        MyQuestGiverId = questGiverId;
        MyCompletedQuests = completedQuests;
    }
}