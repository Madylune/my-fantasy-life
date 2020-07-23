using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public GameObject itemButton;

    public AudioClip sound;

    private void Start() 
    {

    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.MyInstance.PlayClipAt(sound, transform.position);

            //for (int i = 0; i < inventory.slots.Length; i++)
            //{
            //    if (inventory.isFull[i] == false)
            //    {
            //        //Item can be added to inventory
            //        inventory.isFull[i] = true;
            //        Instantiate(itemButton, inventory.slots[i].transform);
            //        Destroy(gameObject);
            //        break;
            //    }
            //}
        }    
    }
}
