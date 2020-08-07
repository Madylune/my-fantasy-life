using UnityEngine;

enum ArmorType { Body, LeftHand, RightHand, Garment, Shoes, Accessory, Head }

[CreateAssetMenu(fileName = "Armor", menuName = "Items/Armor", order = 2)]
public class Armor : Item
{
    [SerializeField]
    private ArmorType armorType;

    [SerializeField]
    private int defense;

    [SerializeField]
    private int magicDefense;

    internal ArmorType MyArmorType { get => armorType; }

    public override string GetDescription()
    {
        string stats = string.Empty;

        if (defense > 0)
        {
            stats += string.Format("\n+{0} def", defense);
        }
        if (magicDefense > 0)
        {
            stats += string.Format("\n+{0} mdef", magicDefense);
        }

        return base.GetDescription() + stats;
    }
}
