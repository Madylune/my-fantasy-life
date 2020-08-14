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

    [SerializeField]
    private int price;

    private SlotScript slot;

    private EquipButton equipButton;

    public Sprite MyIcon { get => icon; }

    public int MyStackSize { get => stackSize; }

    public SlotScript MySlot { get => slot; set => slot = value; }

    public Quality MyQuality { get => quality; }

    public string MyTitle { get => title; }

    public int MyPrice { get => price; }

    public EquipButton MyEquipButton
    {
        get => equipButton;
        set
        {
            MySlot = null;
            equipButton = value;
        }
    }

    public virtual string GetDescription()
    {
        return string.Format("<color={0}>{1}</color>", QualityColor.MyColors[MyQuality], MyTitle);
    }

    public void Remove()
    {
        if (MySlot != null)
        {
            MySlot.RemoveItem(this);
        }
    }
}
