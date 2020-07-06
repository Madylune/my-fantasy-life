using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    [SerializeField]
    private string title;

    [SerializeField]
    [TextArea(3,10)]
    private string description;

    [SerializeField]
    private CollectObjective[] collectObjectives;

    public QuestScript MyQuestScript { get; set; }

    public string MyTitle
    {
        get
        {
            return title;
        }
        set
        {
            title = value;
        }
    }

    public string MyDescription
    {
        get
        {
            return description;
        }
        set
        {
            description = value;
        }
    }

    public CollectObjective[] MyCollectObjectives
    {
        get
        {
            return collectObjectives;
        }
    }
}

[System.Serializable]
public abstract class Objective
{
    [SerializeField]
    private int amount;

    private int currentAmount;

    [SerializeField]
    private string type;

    public int MyAmount
    {
        get
        {
            return amount;
        }
    }

    public int MyCurrentAmount
    {
        get
        {
            return currentAmount;
        }
        set
        {
            currentAmount = value;
        }
    }

    public string MyType
    {
        get
        {
            return type;
        }
    }
}

[System.Serializable]
public class CollectObjective : Objective
{

}