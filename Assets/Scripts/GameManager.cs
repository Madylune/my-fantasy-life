using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerAttack player;

    private GameObject targetIcon;

    private void Update() 
    {
        ClickTarget();
    }

    private void ClickTarget()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 512);

            if (hit.collider != null)
            {
                player.MyTarget = hit.transform;

                targetIcon = hit.transform.GetChild(0).gameObject;
                targetIcon.SetActive(true);
            }
            else 
            {
                //Detarget's the target
                player.MyTarget = null;
                targetIcon.SetActive(false);
            }
        }
    }
}
