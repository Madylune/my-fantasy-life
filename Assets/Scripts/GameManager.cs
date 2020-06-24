using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerAttack player;

    private void Update() 
    {
        // ClickTarget();
    }

    // private void ClickTarget()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 512);

    //         if (hit.collider != null)
    //         {
    //             player.MyTarget = hit.transform;
    //         }
    //         else 
    //         {
    //             //Detarget's the target
    //             player.MyTarget = null;
    //         }
    //     }
    // }
}
