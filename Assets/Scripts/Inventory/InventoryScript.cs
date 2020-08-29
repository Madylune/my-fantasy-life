using System.Collections.Generic;
using UnityEngine;

// To create an event to trigger items count
public delegate void ItemCountChanged(Item item);

public class InventoryScript : MonoBehaviour
{
    public event ItemCountChanged itemCountChangedEvent;

    private static InventoryScript instance;

    public static InventoryScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryScript>();
            }
            return instance;
        }
    }

    private SlotScript fromSlot;

    private List<Bag> bags = new List<Bag>();

    [SerializeField]
    private BagButton[] bagButtons;

    // Just for debugging
    [SerializeField]
    private Item[] items;

    public bool CanAddBag
    {
        get
        {
            return MyBags.Count < 5;
        }
    }

    public int MyEmptySlotCount
    {
        get
        {
            int count = 0;

            foreach (Bag bag in MyBags)
            {
                count += bag.MyBagScript.MyEmptySlotCount;
            }

            return count;
        }
    }

    public int MyTotalSlotCount
    {
        get
        {
            int count = 0;

            foreach (Bag bag in MyBags)
            {
                count += bag.MyBagScript.MySlots.Count;
            }

            return count;
        }
    }

    public int MyFullSlotCount
    {
        get
        {
            return MyTotalSlotCount - MyEmptySlotCount;
        }
    }

    public SlotScript FromSlot
    {
        get => fromSlot;

        set
        {
            fromSlot = value;

            if (value != null)
            {
                fromSlot.MyIcon.color = Color.grey;
            }
        }
    }

    public List<Bag> MyBags { get => bags; }

    private void Awake()
    {
        Bag bag = (Bag)Instantiate(items[0]);
        bag.Initialize(6);
        bag.Use();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) // Debugging: Add a bag to the inventory
        {
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initialize(6);
            AddItem(bag);
        }

        if (Input.GetKeyDown(KeyCode.P)) // Debugging: Add a potion to the inventory
        {
            HealthPotion healthPotion = (HealthPotion)Instantiate(items[1]);
            AddItem(healthPotion);
        }

        if (Input.GetKeyDown(KeyCode.G)) // Debugging: Add a potion to the inventory
        {
            HealthPotion curePotion = (HealthPotion)Instantiate(items[11]);
            AddItem(curePotion);
        }

        if (Input.GetKeyDown(KeyCode.H)) // Debugging: Add a potion to the inventory
        {
            HealthPotion manaPotion = (HealthPotion)Instantiate(items[12]);
            AddItem(manaPotion);
        }

        if (Input.GetKeyDown(KeyCode.M)) // Debugging: Add armors
        {
            AddItem((Armor)Instantiate(items[2])); //Armor
            AddItem((Armor)Instantiate(items[3])); //Footgears
            AddItem((Armor)Instantiate(items[4])); //Garment
            AddItem((Armor)Instantiate(items[5])); //Hat
            AddItem((Armor)Instantiate(items[6])); //Shield
            AddItem((Armor)Instantiate(items[7])); //Necklace
            AddItem((Armor)Instantiate(items[8])); //Rod
            AddItem((Armor)Instantiate(items[9])); //Gloves
            AddItem((Armor)Instantiate(items[10])); //Wand
        }
    }

    public void AddBag(Bag bag)
    {
        foreach (BagButton bagButton in bagButtons)
        {
            if (bagButton.MyBag == null)
            {
                bagButton.MyBag = bag;
                MyBags.Add(bag);
                bag.MyBagButton = bagButton;
                bag.MyBagScript.transform.SetSiblingIndex(bagButton.MyBagIndex);

                break;
            }
        }
    }

    public void AddBag(Bag bag, int bagIndex)
    {
        bag.SetupScript();
        MyBags.Add(bag);
        bag.MyBagButton = bagButtons[bagIndex];
        bagButtons[bagIndex].MyBag = bag;
    }

    public bool AddItem(Item item)
    {
        if (item.MyStackSize > 0)
        {
            if (PlaceInStack(item))
            {
                return true;
            }
        }

        return PlaceInEmpty(item);
    }

    // Place item into an empty slot
    private bool PlaceInEmpty(Item item)
    {
        foreach (Bag bag in MyBags)
        {
            if (bag.MyBagScript.AddItem(item))
            {
                OnItemCountChanged(item);
                return true;
            }
        }

        return false; // If inventory is full
    }

    private bool PlaceInStack(Item item)
    {
        foreach (Bag bag in MyBags)
        {
            foreach (SlotScript slots in bag.MyBagScript.MySlots)
            {
                if (slots.StackItem(item)) // Try to stack the item
                {
                    OnItemCountChanged(item);
                    return true;
                }
            }
        }

        return false;
    }

    public Stack<IUseable> GetUseables(IUseable type)
    {
        Stack<IUseable> useables = new Stack<IUseable>();

        foreach (Bag bag in MyBags)
        {
            foreach (SlotScript slot in bag.MyBagScript.MySlots)
            {
                if (!slot.IsEmpty && slot.MyItem.GetType() == type.GetType())
                {
                    foreach (Item item in slot.MyItems)
                    {
                        useables.Push(item as IUseable);
                    }
                }
            }
        }

        return useables;
    }

    public IUseable GetUseable(string type)
    {
        Stack<IUseable> useables = new Stack<IUseable>();

        foreach (Bag bag in MyBags)
        {
            foreach (SlotScript slot in bag.MyBagScript.MySlots)
            {
                if (!slot.IsEmpty && slot.MyItem.MyTitle == type)
                {
                    return (slot.MyItem as IUseable);
                }
            }
        }

        return null;
    }

    public int GetItemCount(string type)
    {
        int itemCount = 0;

        foreach (Bag bag in MyBags)
        {
            foreach (SlotScript slot in bag.MyBagScript.MySlots)
            {
                if (!slot.IsEmpty && slot.MyItem.MyTitle == type)
                {
                    itemCount += slot.MyItems.Count;
                }
            }
        }

        return itemCount;
    }

    public Stack<Item> GetItems(string type, int count)
    {
        Stack<Item> items = new Stack<Item>();

        foreach (Bag bag in MyBags)
        {
            foreach (SlotScript slot in bag.MyBagScript.MySlots)
            {
                if (!slot.IsEmpty && slot.MyItem.MyTitle == type)
                {
                    foreach (Item item in slot.MyItems)
                    {
                        items.Push(item);

                        if (items.Count == count)
                        {
                            return items;
                        }
                    }
                }
            }
        }

        return items;
    }

    public void OnItemCountChanged(Item item)
    {
        if (itemCountChangedEvent != null)
        {
            itemCountChangedEvent.Invoke(item);
        }
    }
}
