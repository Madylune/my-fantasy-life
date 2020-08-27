using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
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

            SavePlayer(data);

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

    private void Load()
    {
        try
        {
            string path = Application.persistentDataPath + "/" + "SaveTest.fun";

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream file = new FileStream(path, FileMode.Open);

                SaveData data = (SaveData)formatter.Deserialize(file) as SaveData;

                LoadPlayer(data);

                if (data != null)
                {
                    Debug.Log("Game is loaded.");
                }

                file.Close();

                //return data;
            }
            else
            {
                Debug.LogError("Save file not found in " + path);
                //return null;
            }
        }
        catch (System.Exception err)
        {
            Debug.Log(err);
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
}
