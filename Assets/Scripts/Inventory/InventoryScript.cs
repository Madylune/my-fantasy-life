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
}
