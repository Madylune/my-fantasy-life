using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavedCharacter : MonoBehaviour
{
    [SerializeField]
    private Image sprite;

    [SerializeField]
    private Text nameText;

    [SerializeField]
    private Text jobText;

    [SerializeField]
    private Text levelText;

    [SerializeField]
    private GameObject charInfos;

    [SerializeField]
    private int index;

    public int MyIndex { get => index; }

    public void ShowInfo(SaveData saveData)
    {
        charInfos.SetActive(true);

        nameText.text = saveData.MyPlayerData.MyUsername;
        jobText.text = saveData.MyPlayerData.MyJob;
        levelText.text = saveData.MyPlayerData.MyLevel.ToString();
    }
}
