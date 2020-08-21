using UnityEngine;
using UnityEngine.UI;

public class QuestScript : MonoBehaviour
{
    public Quest MyQuest { get; set; }

    private bool markedComplete = false;

    public void Select()
    {
        GetComponent<Text>().color = Color.green;
        QuestLog.MyInstance.ShowDescription(MyQuest);
    }

    public void DeSelect()
    {
        GetComponent<Text>().color = Color.white;
    }

    public void SetComplete()
    {
        if (MyQuest.IsComplete && !markedComplete)
        {
            markedComplete = true;
            GetComponent<Text>().text = string.Format("{0}", "[LVL " + MyQuest.MyLevel + "] " + MyQuest.MyTitle + " (COMPLETE)");
            MessageFeedManager.MyInstance.WriteMessage(string.Format("<size=20>{0} (COMPLETE)</size>", MyQuest.MyTitle));
        }
        else if (!MyQuest.IsComplete)
        {
            markedComplete = false;
            GetComponent<Text>().text = string.Format("{0}", "[LVL " + MyQuest.MyLevel + "] " + MyQuest.MyTitle);
        }
    }
}
