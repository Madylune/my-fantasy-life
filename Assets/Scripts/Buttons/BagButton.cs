using UnityEngine;
using UnityEngine.UI;

public class BagButton : MonoBehaviour
{
    private Bag bag;

    [SerializeField]
    private int bagIndex;

    [SerializeField]
    private Sprite full, empty;

    public Bag MyBag
    {
        get => bag;

        set
        {
            if (value != null)
            {
                GetComponent<Image>().sprite = full;
            }
            else
            {
                GetComponent<Image>().sprite = empty;
            }
            bag = value;
        }
    }

    public int MyBagIndex { get => bagIndex; set => bagIndex = value; }
}
