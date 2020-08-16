using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public delegate void KillConfirmed(Character character);

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    public event KillConfirmed killConfirmedEvent;

    [SerializeField]
    private Player player;

    private Enemy currentTarget;

    private void Update() 
    {
        ClickTarget();
    }

    private void ClickTarget()
    {
        if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            // Make a raycast from the mouse position into the game world
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 512);

            if (hit.collider != null && hit.collider.tag == "Enemy")
            {
                if (currentTarget != null)
                {
                    currentTarget.DeSelect();
                }

                currentTarget = hit.collider.GetComponent<Enemy>();

                player.attack.MyTarget = currentTarget.Select();

                UIManager.MyInstance.ShowTargetFrame(currentTarget);
            }
            else // Deselect target
            {
                UIManager.MyInstance.HideTargetFrame();
                
                if (currentTarget != null)
                {
                    currentTarget.DeSelect();
                }
                
                currentTarget = null;
                player.attack.MyTarget = null;
            }
        }

        else if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 512);

            if (hit.collider != null && (hit.collider.tag == "Enemy" || hit.collider.tag == "Interactable") && hit.collider.gameObject.GetComponent<IInteractable>() == player.MyInteractable)
            {
                player.Interact();
            }
        }
    }

    public void OnKillConfirmed(Character character)
    {
        if (killConfirmedEvent != null)
        {
            killConfirmedEvent(character);
        }
    }
}
