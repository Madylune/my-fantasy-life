using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    public GameObject panel;
    public KeyCode keyCode;

    void Update() 
    {
        if ((Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)) && Input.GetKeyDown(keyCode))
        {
            TogglePanel();
        }
        
    }

    public void TogglePanel()
    {
        if (panel != null)
        {
            bool isActive = panel.activeSelf;
            panel.SetActive(!isActive);
        }
    }
}
