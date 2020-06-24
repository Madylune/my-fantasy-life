using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{  
    [SerializeField]
    private Button[] actionButtons;

    private KeyCode action1, action2, action3, action4, action5, action6, action7, action8, action9;

    private void Start() 
    {
        action1 = KeyCode.F1;
        action2 = KeyCode.F2;
        action3 = KeyCode.F3;
        action4 = KeyCode.F4;
        action5 = KeyCode.F5;
        action6 = KeyCode.F6;
        action7 = KeyCode.F7;
        action8 = KeyCode.F8;
        action9 = KeyCode.F9;
    }

    private void Update() 
    {
        if (Input.GetKeyDown(action1))
        {   
            ActionButtonOnClick(0);
        }
        if (Input.GetKeyDown(action2))
        {   
            ActionButtonOnClick(1);
        }
        if (Input.GetKeyDown(action3))
        {   
            ActionButtonOnClick(2);
        }
        if (Input.GetKeyDown(action4))
        {   
            ActionButtonOnClick(3);
        }
        if (Input.GetKeyDown(action5))
        {   
            ActionButtonOnClick(4);
        }
        if (Input.GetKeyDown(action6))
        {   
            ActionButtonOnClick(5);
        }
        if (Input.GetKeyDown(action7))
        {   
            ActionButtonOnClick(6);
        }
        if (Input.GetKeyDown(action8))
        {   
            ActionButtonOnClick(7);
        }
        if (Input.GetKeyDown(action9))
        {   
            ActionButtonOnClick(8);
        }
    }

    private void ActionButtonOnClick(int buttonIndex)
    {
        actionButtons[buttonIndex].onClick.Invoke();
    }
}
