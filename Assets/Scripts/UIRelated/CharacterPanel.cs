using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    private static CharacterPanel instance;

    public static CharacterPanel MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CharacterPanel>();
            }
            return instance;
        }
    }

    [SerializeField]
    private EquipButton head, body, leftHand, rightHand, garment, shoes, leftAccessory, rightAccessory;

    public void EquipArmor(Armor armor)
    {
        switch (armor.MyArmorType)
        {
            case ArmorType.Accessory:
                if (leftAccessory.transform.GetChild(0).GetComponent<Image>().sprite == null)
                {
                    leftAccessory.EquipArmor(armor);
                }
                else
                {
                    rightAccessory.EquipArmor(armor);
                }
                break;
            case ArmorType.Body:
                body.EquipArmor(armor);
                break;
            case ArmorType.Garment:
                garment.EquipArmor(armor);
                break;
            case ArmorType.Head:
                head.EquipArmor(armor);
                break;
            case ArmorType.LeftHand:
                leftHand.EquipArmor(armor);
                break;
            case ArmorType.RightHand:
                rightHand.EquipArmor(armor);
                break;
            case ArmorType.Shoes:
                shoes.EquipArmor(armor);
                break;
            default:
                break;
        }
    }
}
