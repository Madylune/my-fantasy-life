using UnityEngine;

public abstract class Item : ScriptableObject // Not having attached GO 
{
    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private int stackSize;

    private SlotScript slot;

    public Sprite MyIcon { get => icon; }

    public int MyStackSize { get => stackSize; }

    public SlotScript MySlot { get => slot; set => slot = value; }

    public void Remove()
    {
        if (MySlot != null)
        {
            MySlot.RemoveItem(this);
        }
    }
}
