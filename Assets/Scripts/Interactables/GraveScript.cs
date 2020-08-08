using UnityEngine;
using UnityEngine.EventSystems;

public class GraveScript : MonoBehaviour
{
    private LootTable loot;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Interact();
        }
    }

    public void SetLoot(LootTable lootTable)
    {
        loot = lootTable;
    }

    public void Interact()
    {
        loot.ShowLoot();

        Destroy(gameObject);
    }
}
