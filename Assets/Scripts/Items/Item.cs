using UnityEngine;

public abstract class Item : ScriptableObject // Not having attached GO 
{
    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private int stackSize;

    private SlotScript slot;

    public Sprite MyIcon { get => icon; }

    public int StackSize { get => stackSize; }

    protected SlotScript Slot { get => slot; set => slot = value; }
}
