using System.Collections.Generic;
using UnityEngine;

public class BagScript : MonoBehaviour
{
    [SerializeField]
    private GameObject slotPrefab;

    private List<SlotScript> slots = new List<SlotScript>();

    public List<SlotScript> MySlots { get => slots; }

    public void AddSlots(int slotCount)
    {
        for (int i = 0; i < slotCount; i++)
        {
            SlotScript slot = Instantiate(slotPrefab, transform).GetComponent<SlotScript>();
            MySlots.Add(slot);
        }
    }

    public bool AddItem(Item item)
    {
        foreach (SlotScript slot in MySlots)
        {
            if (slot.IsEmpty)
            {
                slot.AddItem(item);

                return true;
            }
        }

        return false;
    }
}
