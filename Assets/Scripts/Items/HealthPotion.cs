using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/Potion", order = 1)]
public class HealthPotion : Item, IUseable
{
    [SerializeField]
    private int health;

    [SerializeField]
    private int mana;

    [SerializeField]
    private bool isRed, isWhite, isGreen, isBlue;

    private string description;

    public void Use()
    {
        if (isRed && Player.MyInstance.health.currentHealth < Player.MyInstance.health.maxHealth)
        {
            Remove();

            Player.MyInstance.health.HealPlayer(health);
        }
    }

    public override string GetDescription()
    {
        if (isRed)
        {
            description = string.Format("\nThis potion was made with red herbs and can restore about {0} HP.", health);
        }

        if (isGreen)
        {
            description = string.Format("\nThis potion was made with green herbs and can cure Poison, Silence, Blind and Confusion effects.");
        }

        if (isBlue)
        {
            description = string.Format("\nThis potion was made with blue herbs and can restore about {0} MP", mana);
        }

        return base.GetDescription() + description;
    }
}
