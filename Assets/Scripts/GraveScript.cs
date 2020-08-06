using UnityEngine;
using UnityEngine.EventSystems;

public class GraveScript : MonoBehaviour
{
    private LootTable loot;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Interact();
        }
    }

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
