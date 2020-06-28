using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static CursorController instance;

    public Texture2D targetCursor;

    private void Awake() 
    {
        instance = this;
    }

    public void ActivateTargetCursor()
    {
        Cursor.SetCursor(targetCursor, Vector2.zero, CursorMode.Auto);
    }

    public void ClearCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
