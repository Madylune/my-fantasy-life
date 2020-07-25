using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
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

    private List<Bag> bags = new List<Bag>();

    [SerializeField]
    private BagButton[] bagButtons;

    // Just for debugging
    [SerializeField]
    private Item[] items;

    public bool canAddBag
    {
        get
        {
            return bags.Count < 5;
        }
    }

    private void Awake()
    {
        Bag bag = (Bag)Instantiate(items[0]);
        bag.Initialize(6);
        bag.Use();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initialize(6);
            bag.Use();
        }

        if (Input.GetKeyDown(KeyCode.V)) // Debugging: Add a bag to the inventory
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
    }

    public void AddBag(Bag bag)
    {
        foreach (BagButton bagButton in bagButtons)
        {
            if (bagButton.MyBag == null)
            {
                bagButton.MyBag = bag;
                bags.Add(bag);

                break;
            }
        }
    }

    public void AddItem(Item item)
    {
        foreach (Bag bag in bags)
        {
            if (bag.MyBagScript.AddItem(item))
            {
                return;
            }
        }
    }
}
