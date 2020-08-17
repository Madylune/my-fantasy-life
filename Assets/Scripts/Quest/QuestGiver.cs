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

    public Quest[] MyQuests { get => quests; }

    private void Start()
    {
        foreach (Quest quest in quests)
        {
            quest.MyQuestGiver = this;
        }
    }

    public void UpdateQuestStatus()
    {
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
        }
    }
}
