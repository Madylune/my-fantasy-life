using UnityEngine;

public enum Quality {  Common, Uncommon, Rare, Epic }

public abstract class Item : ScriptableObject, IMoveable, IDescribable //ScriptableObject: Not having attached GO 
{
    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private int stackSize;

    [SerializeField]
    private string title;

    [SerializeField]
    private Quality quality;

    private SlotScript slot;

    public Sprite MyIcon { get => icon; }

    public int MyStackSize { get => stackSize; }

    public SlotScript MySlot { get => slot; set => slot = value; }

    public virtual string GetDescription()
    {
        string color = string.Empty;

        switch (quality)
        {
            case Quality.Common:
                color = "#FFF390"; // yellow
                break;
            case Quality.Uncommon:
                color = "#9BE783"; // green
                break;
            case Quality.Rare:
                color = "#FC9552"; // orange
                break;
            case Quality.Epic:
                color = "#D82D2D"; // red
                break;
            default:
                break;
        }

        return string.Format("<color={0}>{1}</color>", color, title);
    }

    public void Remove()
    {
        if (MySlot != null)
        {
            MySlot.RemoveItem(this);
        }
    }
}
