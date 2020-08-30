using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private Item[] items;

    private Chest[] chests;

    private EquipButton[] equips;

    [SerializeField]
    private ActionButton[] actionsButtons;

    private void Awake()
    {
        chests = FindObjectsOfType<Chest>();
        equips = FindObjectsOfType<EquipButton>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Load();
        }
    }

    private void Save()
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/" + "SaveTest.fun";

            FileStream file = new FileStream(path, FileMode.Create);

            SaveData data = new SaveData();

            SaveEquipment(data);
            SaveBags(data);
            SaveInventory(data);
            SavePlayer(data);
            SaveChests(data);
            SaveActionBar(data);
            SaveQuest(data);
            SaveQuestGivers(data);

            if (data != null)
            {
                Debug.Log("Game is saved.");
            }

            formatter.Serialize(file, data);

            file.Close();
        }
        catch (System.Exception err)
        {
            Debug.Log(err);
            throw;
        }
    }

    private void SavePlayer(SaveData data)
    {
        data.MyPlayerData = new PlayerData(
            Player.MyInstance.MyLevel,
            Player.MyInstance.MyExp.MyCurrentValue,
            Player.MyInstance.MyExp.MyMaxValue,
            Player.MyInstance.health.currentHealth,
            Player.MyInstance.health.maxHealth,
            Player.MyInstance.MyCurrentMana,
            Player.MyInstance.MyMaxMana,
            Player.MyInstance.transform.position
        );
    }

    private void SaveChests(SaveData data)
    {
        for (int i = 0; i < chests.Length; i++)
        {
            data.MyChestData.Add(new ChestData(chests[i].name));

            foreach (Item item in chests[i].MyItems)
            {
                if (chests[i].MyItems.Count > 0)
                {
                    data.MyChestData[i].MyItems.Add(new ItemData(item.MyTitle, item.MySlot.MyItems.Count, item.MySlot.MyIndex));
                }
            }
        }
    }

    private void SaveBags(SaveData data)
    {
        for (int i = 1; i < InventoryScript.MyInstance.MyBags.Count; i++)
        {
            data.MyInventoryData.MyBags.Add(new BagData(InventoryScript.MyInstance.MyBags[i].MySlotCount, InventoryScript.MyInstance.MyBags[i].MyBagButton.MyBagIndex));
        }
    }

    private void SaveEquipment(SaveData data)
    {
        foreach (EquipButton equip in equips)
        {
            if (equip.MyEquippedArmor != null)
            {
                data.MyEquipmentData.Add(new EquipmentData(equip.MyEquippedArmor.MyTitle, equip.name));
            }
        }
    }

    private void SaveActionBar(SaveData data)
    {
        for (int i = 0; i < actionsButtons.Length; i++)
        {
            if (actionsButtons[i].MyUseable != null)
            {
                ActionBarData action;

                if (actionsButtons[i].MyUseable is Spell)
                {
                    action = new ActionBarData((actionsButtons[i].MyUseable as Spell).MyName, false, i);
                }
                else
                {
                    action = new ActionBarData((actionsButtons[i].MyUseable as Item).MyTitle, true, i);
                }

                data.MyActionBarData.Add(action);
            }
        }
    }

    private void SaveInventory(SaveData data)
    {
        List<SlotScript> slots = InventoryScript.MyInstance.GetAllItems();

        foreach (SlotScript slot in slots)
        {
            data.MyInventoryData.MyItems.Add(new ItemData(slot.MyItem.MyTitle, slot.MyItems.Count, slot.MyIndex, slot.MyBag.MyBagIndex));
        }
    }

    private void SaveQuest(SaveData data)
    {
        foreach (Quest quest in QuestLog.MyInstance.MyQuests)
        {
            data.MyQuestData.Add(new QuestData(quest.MyTitle, quest.MyDescription, quest.MyCollectObjectives, quest.MyKillObjectives, quest.MyQuestGiver.MyQuestGiverId));
        }
    }

    private void SaveQuestGivers(SaveData data)
    {
        QuestGiver[] questGivers = FindObjectsOfType<QuestGiver>();

        foreach (QuestGiver questGiver in questGivers)
        {
            data.MyQuestGiverData.Add(new QuestGiverData(questGiver.MyQuestGiverId, questGiver.MyCompletedQuests));
        }
    }

    private void Load()
    {
        try
        {
            string path = Application.persistentDataPath + "/" + "SaveTest.fun";

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream file = new FileStream(path, FileMode.Open);

                SaveData data = (SaveData)formatter.Deserialize(file);

                LoadEquipment(data);
                LoadBags(data);
                LoadInventory(data);
                LoadPlayer(data);
                LoadChests(data);
                LoadActionBar(data);
                LoadQuest(data);
                LoadQuestGivers(data);

                if (data != null)
                {
                    Debug.Log("Game is loaded.");
                }

                file.Close();
            }
            else
            {
                Debug.LogError("Save file not found in " + path);
            }
        }
        catch (System.Exception err)
        {
            Debug.Log(err);
            throw;
        }
    }

    private void LoadPlayer(SaveData data)
    {
        Player.MyInstance.MyLevel = data.MyPlayerData.MyLevel;
        Player.MyInstance.UpdateLevel();
        Player.MyInstance.health.healthBar.Initialize(data.MyPlayerData.MyHealth, data.MyPlayerData.MyMaxHealth);
        Player.MyInstance.MyManaBar.Initialize(data.MyPlayerData.MyMana, data.MyPlayerData.MyMaxMana);
        Player.MyInstance.MyExp.Initialize(data.MyPlayerData.MyXp, data.MyPlayerData.MyMaxXp);
        Player.MyInstance.transform.position = new Vector2(data.MyPlayerData.MyX, data.MyPlayerData.MyY);
    }

    private void LoadChests(SaveData data)
    {
        foreach (ChestData chest in data.MyChestData)
        {
            Chest c = Array.Find(chests, x => x.name == chest.MyName);

            foreach (ItemData itemData in chest.MyItems)
            {
                Item item = Array.Find(items, x => x.MyTitle == itemData.MyTitle);
                item.MySlot = c.MyBag.MySlots.Find(x => x.MyIndex == itemData.MySlotIndex);
                c.MyItems.Add(item);
            }
        }
    }

    private void LoadBags(SaveData data)
    {
        foreach (BagData bagData in data.MyInventoryData.MyBags)
        {
            Bag newBag = (Bag)Instantiate(items[0]);
            newBag.Initialize(bagData.MySlotCount);

            InventoryScript.MyInstance.AddBag(newBag, bagData.MyBagIndex);
        }
    }

    private void LoadEquipment(SaveData data)
    {
        foreach (EquipmentData equipmentData in data.MyEquipmentData)
        {
            EquipButton equip = Array.Find(equips, x => x.name == equipmentData.MyType);

            equip.EquipArmor(Array.Find(items, x => x.MyTitle == equipmentData.MyTitle) as Armor);
        }
    }

    private void LoadActionBar(SaveData data)
    {
        foreach (ActionBarData actionData in data.MyActionBarData)
        {
            if (actionData.IsItem)
            {
                actionsButtons[actionData.MyIndex].SetUseable(InventoryScript.MyInstance.GetUseable(actionData.MyAction));
            }
            else
            {
                actionsButtons[actionData.MyIndex].SetUseable(SpellBook.MyInstance.GetSpell(actionData.MyAction));
            }
        }
    }

    private void LoadInventory(SaveData data)
    {
        foreach (ItemData itemData in data.MyInventoryData.MyItems)
        {
            Item item = Array.Find(items, x => x.MyTitle == itemData.MyTitle);

            for (int i = 0; i < itemData.MyStackCount; i++)
            {
                InventoryScript.MyInstance.PlaceInSpecificSlot(item, itemData.MySlotIndex, itemData.MyBagIndex);
            }
        }
    }

    private void LoadQuest(SaveData data)
    {
        QuestGiver[] questGivers = FindObjectsOfType<QuestGiver>();

        foreach (QuestData questData in data.MyQuestData)
        {
            QuestGiver qg = Array.Find(questGivers, x => x.MyQuestGiverId == questData.MyQuestGiverId);
            Quest q = Array.Find(qg.MyQuests, x => x.MyTitle == questData.MyTitle);

            q.MyQuestGiver = qg;
            q.MyKillObjectives = questData.MyKillObjectives;

            QuestLog.MyInstance.AcceptQuest(q);
        }
    }

    private void LoadQuestGivers(SaveData data)
    {
        QuestGiver[] questGivers = FindObjectsOfType<QuestGiver>();

        foreach (QuestGiverData questGiverData in data.MyQuestGiverData)
        {
            QuestGiver questGiver = Array.Find(questGivers, x => x.MyQuestGiverId == questGiverData.MyQuestGiverId);
            questGiver.MyCompletedQuests = questGiverData.MyCompletedQuests;
            questGiver.UpdateQuestStatus();
        }
    }
}
