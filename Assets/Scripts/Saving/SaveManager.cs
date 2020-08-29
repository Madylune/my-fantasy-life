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
            SavePlayer(data);
            SaveChests(data);

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
                LoadPlayer(data);
                LoadChests(data);

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
}
