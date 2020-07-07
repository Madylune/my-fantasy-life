using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{
    [SerializeField]
    private GameObject questPrefab;

    [SerializeField]
    private Transform questParent;

    private Quest selected;

    [SerializeField]
    private Text questDescription;

    [SerializeField]
    private GameObject actionsButtons;

    private static QuestLog instance;

    public static QuestLog MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<QuestLog>();
            }
            return instance;
        }
    }

    public void AcceptQuest(Quest quest)
    {
        GameObject go = Instantiate(questPrefab, questParent);

        QuestScript qs = go.GetComponent<QuestScript>();
        quest.MyQuestScript = qs;
        qs.MyQuest = quest;

        string objectives = string.Empty;

        foreach (Objective obj in quest.MyCollectObjectives)
        {
            objectives = obj.MyType + ": " + obj.MyCurrentAmount + "/" + obj.MyAmount + "\n";
        }

        go.GetComponent<Text>().text = string.Format("{0}\n<size=11>{1}</size>", "[REQUEST] " + quest.MyTitle, objectives);
    }

    public void ShowDescription(Quest quest)
    {
        if (selected != null)
        {
            selected.MyQuestScript.DeSelect();
            actionsButtons.SetActive(false);
        }

        selected = quest;

        questDescription.text = string.Format("<b>{0}</b>\n\n<size=13>{1}</size>", "[ " + quest.MyTitle + " ]", quest.MyDescription);

        actionsButtons.SetActive(true);
    }
}
