using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiverPanel : Panel
{
    private static QuestGiverPanel instance;

    public static QuestGiverPanel MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<QuestGiverPanel>();
            }
            return instance;
        }
    }

    private QuestGiver questGiver;

    [SerializeField]
    private GameObject cancelBtn, acceptBtn, abandonBtn, completeBtn, questDescription;

    [SerializeField]
    private GameObject questPrefab;

    [SerializeField]
    private Transform questArea;

    private List<GameObject> quests = new List<GameObject>();

    private Quest selectedQuest;

    public void ShowQuests(QuestGiver questGiver)
    {
        this.questGiver = questGiver;

        foreach (GameObject go in quests)
        {
            Destroy(go);
        }

        questArea.gameObject.SetActive(true);
        questDescription.SetActive(false);

        foreach (Quest quest in questGiver.MyQuests)
        {
            if (quest != null)
            {
                GameObject go = Instantiate(questPrefab, questArea);

                go.GetComponent<Text>().text = "[LVL " + quest.MyLevel + "] " + quest.MyTitle;

                go.GetComponent<QuestGiverQuestScript>().MyQuest = quest;

                quests.Add(go);

                if (QuestLog.MyInstance.HasQuest(quest) && quest.IsComplete)
                {
                    go.GetComponent<Text>().text = string.Format("<color=red>{0}</color>", "[LVL " + quest.MyLevel + "] " + quest.MyTitle + " (COMPLETE)");
                }
                else if (QuestLog.MyInstance.HasQuest(quest))
                {
                    // Greys out accepted quest
                    Color c = go.GetComponent<Text>().color;
                    c.a = 0.5f;
                    go.GetComponent<Text>().color = c;
                }
            }
        }
    }

    public override void Open(NPC npc)
    {
        ShowQuests((npc as QuestGiver));
        base.Open(npc);
    }

    public void ShowQuestInfo(Quest quest)
    {
        this.selectedQuest = quest;

        if (QuestLog.MyInstance.HasQuest(quest) && quest.IsComplete)
        {
            acceptBtn.SetActive(false);
            completeBtn.SetActive(true);
        }
        else if (QuestLog.MyInstance.HasQuest(quest))
        {
            abandonBtn.SetActive(true);
        }
        else if (!QuestLog.MyInstance.HasQuest(quest))
        {
            acceptBtn.SetActive(true);
            abandonBtn.SetActive(false);
        }

        cancelBtn.SetActive(true);
        questArea.gameObject.SetActive(false);
        questDescription.SetActive(true);

        string title = quest.MyTitle;
        string description = quest.MyDescription;

        questDescription.GetComponent<Text>().text = string.Format("<b>{0}</b>\n\n<size=13>{1}</size>", "[ " + title + " ]", description);
    }

    public void Cancel()
    {
        acceptBtn.SetActive(false);
        cancelBtn.SetActive(false);
        abandonBtn.SetActive(false);
        completeBtn.SetActive(false);

        ShowQuests(questGiver);
    }

    public void AcceptQuest()
    {
        QuestLog.MyInstance.AcceptQuest(selectedQuest);
        Cancel();
    }

    public void CompleteQuest()
    {
        if (selectedQuest.IsComplete)
        {
            for (int i = 0; i < questGiver.MyQuests.Length; i++)
            {
                if (selectedQuest == questGiver.MyQuests[i])
                {
                    questGiver.MyCompletedQuests.Add(selectedQuest.MyTitle);
                    questGiver.MyQuests[i] = null;
                    selectedQuest.MyQuestGiver.UpdateQuestStatus();
                }
            }

            foreach (CollectObjective o in selectedQuest.MyCollectObjectives)
            {
                InventoryScript.MyInstance.itemCountChangedEvent -= new ItemCountChanged(o.UpdateItemCount);
                o.Complete();
            }

            foreach (KillObjective o in selectedQuest.MyKillObjectives)
            {
                GameManager.MyInstance.killConfirmedEvent -= new KillConfirmed(o.UpdateKillCount);
            }

            Player.MyInstance.GainExp(XPManager.CalculateXP(selectedQuest));

            QuestLog.MyInstance.RemoveQuest(selectedQuest.MyQuestScript);
            Cancel();
        }
    }

    public override void Close()
    {
        completeBtn.SetActive(false);
        cancelBtn.SetActive(false);
        base.Close();
    }
}
