using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private ArmorType armorType;

    private Armor armor;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text title;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (HandScript.MyInstance.MyMoveable is Armor)
            {
                Armor tmp = (Armor)HandScript.MyInstance.MyMoveable;

                if (tmp.MyArmorType == armorType)
                {
                    EquipArmor(tmp);
                }
            }
        }
    }

    public void EquipArmor(Armor armor)
    {
        icon.enabled = true;
        icon.sprite = armor.MyIcon;
        icon.color = Color.white;

        title.text = armor.MyTitle;

        this.armor = armor; // Reference to the equipped armor

        HandScript.MyInstance.DeleteItem();
    }
}
