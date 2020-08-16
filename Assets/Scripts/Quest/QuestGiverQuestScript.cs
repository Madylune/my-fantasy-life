using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiverQuestScript : MonoBehaviour
{
    public Quest MyQuest { get; set; }

    public void Select()
    {
        QuestGiverPanel.MyInstance.ShowQuestInfo(MyQuest);
    }
}
