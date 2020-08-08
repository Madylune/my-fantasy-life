using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private ArmorType armorType;

    private Armor equippedArmor;

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

                UIManager.MyInstance.RefreshTooltip(tmp);
            }
            else if (HandScript.MyInstance.MyMoveable == null && equippedArmor != null)
            {
                HandScript.MyInstance.TakeMoveable(equippedArmor);
                CharacterPanel.MyInstance.MySelectedButton = this;
                icon.color = Color.grey;
            }
        }
    }

    public void EquipArmor(Armor armor)
    {
        // Remove the armor from the inventory
        armor.Remove();

        if (equippedArmor != null)
        {
            if (equippedArmor != armor)
            {
                armor.MySlot.AddItem(equippedArmor);
            }

            UIManager.MyInstance.RefreshTooltip(equippedArmor);
        }
        else
        {
            UIManager.MyInstance.HideTooltip();
        }

        icon.enabled = true;
        icon.sprite = armor.MyIcon;
        icon.color = Color.white;

        title.text = string.Format("<color={0}>{1}</color>", QualityColor.MyColors[armor.MyQuality], armor.MyTitle);

        this.equippedArmor = armor; // Reference to the equipped armor
        this.equippedArmor.MyEquipButton = this;

        if (HandScript.MyInstance.MyMoveable == (armor as IMoveable))
        {
            HandScript.MyInstance.Drop();
        }
    }

    public void DequipArmor()
    {
        icon.color = Color.white;
        icon.enabled = false;
        title.text = equippedArmor.MyArmorType.ToString();

        equippedArmor.MyEquipButton = null;
        equippedArmor = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (equippedArmor != null)
        {
            UIManager.MyInstance.ShowTooltip(new Vector2(0,0), transform.position, equippedArmor);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }
}
