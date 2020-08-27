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
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + "SaveTest.dat", FileMode.Create);

            SaveData data = new SaveData();

            SavePlayer(data);

            if (data != null)
            {
                Debug.Log("Game is saved.");
            }

            bf.Serialize(file, data);

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
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + "SaveTest.dat", FileMode.Open);

            SaveData data = (SaveData)bf.Deserialize(file);

            if (data != null)
            {
                Debug.Log("Game is loaded.");
            }

            LoadPlayer(data);

            file.Close();
        }
        catch (System.Exception err)
        {
            Debug.Log(err);
        }
    }

    private void LoadPlayer(SaveData data)
    {
        Debug.Log("LoadPlayer");
        Player.MyInstance.MyLevel = data.MyPlayerData.MyLevel;

        Player.MyInstance.UpdateLevel();
        Player.MyInstance.health.healthBar.Initialize(data.MyPlayerData.MyHealth, data.MyPlayerData.MyMaxHealth);
    }
}
