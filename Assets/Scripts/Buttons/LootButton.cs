using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LootButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text title;

    private LootPanel lootPanel;

    public Image MyIcon { get => icon; }

    public Text MyTitle { get => title; }

    public Item MyLoot { get; set; }

    private void Awake()
    {
        lootPanel = GetComponentInParent<LootPanel>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Loot item and put it into inventory
        if (InventoryScript.MyInstance.AddItem(MyLoot))
        {
            gameObject.SetActive(false);
            lootPanel.TakeLoot(MyLoot);
            UIManager.MyInstance.HideTooltip();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.MyInstance.ShowTooltip(new Vector2(1,0), transform.position, MyLoot);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }
}
