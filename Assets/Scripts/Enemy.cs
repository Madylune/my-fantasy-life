using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    public override Transform Select()
    {
        canvasGroup.alpha = 1;

        return base.Select();
    }

    public override void DeSelect()
    {
        canvasGroup.alpha = 0;
        
        base.DeSelect();
    }

    private void OnMouseOver() 
    {
        CursorController.instance.ActivateTargetCursor();
    }

    private void OnMouseExit()
    {
        CursorController.instance.ClearCursor();
    }
}
