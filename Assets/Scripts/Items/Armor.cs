using UnityEngine;

enum ArmorType { Armor, Shield, Garment, Footgears, Accessories, Headgears }

[CreateAssetMenu(fileName = "Armor", menuName = "Items/Armor", order = 2)]
public class Armor : Item
{
    [SerializeField]
    private ArmorType armorType;

    [SerializeField]
    private int defense;

    [SerializeField]
    private int magicDefense;

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
