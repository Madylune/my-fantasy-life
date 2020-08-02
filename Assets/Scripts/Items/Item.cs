using UnityEngine;

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
        return string.Format("<color={0}>{1}</color>", QualityColor.MyColors[quality], title);
    }

    public void Remove()
    {
        if (MySlot != null)
        {
            MySlot.RemoveItem(this);
        }
    }
}
