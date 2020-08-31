using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField]
    private SavedCharacter[] saveChars;

    private void Awake()
    {
        foreach (SavedCharacter saved in saveChars)
        {
            ShowSavedFiles(saved);
        }
    }

    private void ShowSavedFiles(SavedCharacter savedCharacter)
    {
        //string path = Application.persistentDataPath + "/" + savedCharacter.gameObject.name + ".fun";
        string path = Application.persistentDataPath + "/" + "SaveTest.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            SaveData data = (SaveData)formatter.Deserialize(file);

            file.Close();
            savedCharacter.ShowInfo(data);
        }
    }

    public void SelectCharacter(int index)
    {
        // Load character data
    }
}
