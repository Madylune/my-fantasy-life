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
    private GameObject actionButtons, questDescription;

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
            GameObject go = Instantiate(questPrefab, questArea);
            go.GetComponent<Text>().text = quest.MyTitle;

            go.GetComponent<QuestGiverQuestScript>().MyQuest = quest;

            quests.Add(go);
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

        actionButtons.SetActive(true);
        questArea.gameObject.SetActive(false);
        questDescription.SetActive(true);

        string title = quest.MyTitle;
        string description = quest.MyDescription;
        string objectives = string.Empty;

        foreach (Objective obj in quest.MyCollectObjectives)
        {
            objectives = obj.MyType + ": " + obj.MyCurrentAmount + "/" + obj.MyAmount + "\n";
        }

        questDescription.GetComponent<Text>().text = string.Format("<b>{0}</b>\n\n<size=13>{1}</size>", "[ " + title + " ]", description);
    }

    public void Cancel()
    {
        actionButtons.SetActive(false);
        ShowQuests(questGiver);
    }

    public void AcceptQuest()
    {
        QuestLog.MyInstance.AcceptQuest(selectedQuest);
        Cancel();
    }
}
