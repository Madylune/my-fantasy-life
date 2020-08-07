using UnityEngine;

enum ArmorType { Body, LHand, RHand, Garment, Shoes, Accessory, Head }

[CreateAssetMenu(fileName = "Armor", menuName = "Items/Armor", order = 2)]
public class Armor : Item
{
    [SerializeField]
    private ArmorType armorType;

    [SerializeField]
    private int defense;

    [SerializeField]
    private int magicDefense;

    [SerializeField]
    private int attack;

    [SerializeField]
    private int magicAttack;

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
        if (attack > 0)
        {
            stats += string.Format("\n+{0} atk", attack);
        }
        if (magicAttack > 0)
        {
            stats += string.Format("\n+{0} matk", magicAttack);
        }

        return base.GetDescription() + stats;
    }

    public void Equip()
    {
        CharacterPanel.MyInstance.EquipArmor(this);
    }
}
