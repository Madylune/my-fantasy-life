using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/Potion", order = 1)]
public class HealthPotion : Item, IUseable
{
    [SerializeField]
    private int health;

    public void Use()
    {
        if (Player.MyInstance.health.currentHealth < Player.MyInstance.health.maxHealth)
        {
            Remove();

            Player.MyInstance.health.HealPlayer(health);
        }
    }

    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\nThis potion was made with red herbs and can restore about {0} HP.", health);
    }
}
