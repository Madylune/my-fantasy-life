using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : NPC
{   
    [SerializeField]
    private Quest[] quests;

    [SerializeField]
    private Sprite interrogationY, interrogationG, exclamation;

    [SerializeField]
    private SpriteRenderer statusRenderer;

    [SerializeField]
    private int questGiverId;

    private List<string> completedQuests = new List<string>();

    public Quest[] MyQuests { get => quests; }

    public int MyQuestGiverId { get => questGiverId; }

    public List<string> MyCompletedQuests
    {
        get => completedQuests;
        set
        {
            completedQuests = value;

            foreach (string title in completedQuests)
            {
                for (int i = 0; i < quests.Length; i++)
                {
                    if (quests[i] != null && quests[i].MyTitle == title)
                    {
                        quests[i] = null;
                    }
                }
            }
        }

    }

    private void Start()
    {
        foreach (Quest quest in quests)
        {
            quest.MyQuestGiver = this;
        }
    }

    public void UpdateQuestStatus()
    {
        int count = 0;

        foreach (Quest quest in quests)
        {
            if (quest != null)
            {
                if (QuestLog.MyInstance.HasQuest(quest))
                {
                    if (!quest.IsComplete)
                    {
                        statusRenderer.sprite = interrogationG;
                    }
                    else
                    {
                        statusRenderer.sprite = interrogationY;
                    }

                }
                else
                {
                    statusRenderer.sprite = exclamation;
                }
            }
            else
            {
                count++;

                if (count == quests.Length)
                {
                    statusRenderer.enabled = false;
                }
            }
        }
    }
}
