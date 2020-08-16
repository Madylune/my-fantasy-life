﻿using System.Collections;
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

    private List<QuestScript> questScripts = new List<QuestScript>();

    private List<Quest> quests = new List<Quest>();

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
        if (quest != null)
        {
            foreach (CollectObjective o in quest.MyCollectObjectives)
            {
                InventoryScript.MyInstance.itemCountChangedEvent += new ItemCountChanged(o.UpdateItemCount);

                o.UpdateItemCount();
            }

            quests.Add(quest);

            GameObject go = Instantiate(questPrefab, questParent);

            QuestScript qs = go.GetComponent<QuestScript>();
            quest.MyQuestScript = qs;
            qs.MyQuest = quest;

            questScripts.Add(qs);

            go.GetComponent<Text>().text = string.Format("{0}", "[REQUEST] " + quest.MyTitle);

            CheckCompletion();
        }
    }

    public void UpdateObjectives()
    {
        ShowDescription(selected);
    }

    public void ShowDescription(Quest quest)
    {
        if (quest != null)
        {
            if (selected != null && selected != quest)
            {
                selected.MyQuestScript.DeSelect();
                actionsButtons.SetActive(false);
            }

            selected = quest;

            string objectives = string.Empty;

            foreach (Objective obj in quest.MyCollectObjectives)
            {
                objectives = obj.MyType + ": " + obj.MyCurrentAmount + "/" + obj.MyAmount + "\n";
            }

            questDescription.text = string.Format("<b>{0}</b>\n\n<size=13>{1}</size>\n\n<size=12><color=red>{2}</color></size>", "[ " + quest.MyTitle + " ]", quest.MyDescription, objectives);

            actionsButtons.SetActive(true);
        }
    }

    public void CheckCompletion()
    {
        foreach (QuestScript qs in questScripts)
        {
            qs.SetComplete();
        }
    }

    public void AbandonQuest()
    {

    }

    public bool HasQuest(Quest quest)
    {
        return quests.Exists(x => x.MyTitle == quest.MyTitle);
    }
}
