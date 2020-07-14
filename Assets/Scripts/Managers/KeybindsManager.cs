using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeybindsManager : MonoBehaviour
{
    private static KeybindsManager instance;

    public static KeybindsManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<KeybindsManager>();
            }
            return instance;
        }
    }

    public Dictionary<string, KeyCode> Keybinds { get; private set; }

    public Dictionary<string, KeyCode> ActionBinds { get; private set; }

    private string bindName;

    void Start()
    {
        Keybinds = new Dictionary<string, KeyCode>();
        ActionBinds = new Dictionary<string, KeyCode>();

        BindKey("UP", KeyCode.Z);
        BindKey("LEFT", KeyCode.Q);
        BindKey("RIGHT", KeyCode.S);
        BindKey("DOWN", KeyCode.W);

        BindKey("SKILL1", KeyCode.F1);
        BindKey("SKILL2", KeyCode.F2);
        BindKey("SKILL3", KeyCode.F3);
    }

    public void BindKey(string key, KeyCode keyBind)
    {
        Dictionary<string, KeyCode> currentDictionary = Keybinds;

        if (key.Contains("SKILL"))
        {
            currentDictionary = ActionBinds;
        }

        if (!currentDictionary.ContainsKey(key))
        {
            currentDictionary.Add(key, keyBind);
            UIManager.MyInstance.UpdateKeyText(key, keyBind);
        }
        else if (currentDictionary.ContainsValue(keyBind))
        {
            string myKey = currentDictionary.FirstOrDefault(x => x.Value == keyBind).Key;

            currentDictionary[myKey] = KeyCode.None;
            UIManager.MyInstance.UpdateKeyText(key, KeyCode.None);
        }

        currentDictionary[key] = keyBind;
        UIManager.MyInstance.UpdateKeyText(key, keyBind);
        bindName = string.Empty;
    }
}
