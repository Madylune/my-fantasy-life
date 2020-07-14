using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour, IPointerClickHandler
{
    public IUseable MyUseable { get; set; }

    public Button MyButton { get; private set; }

    void Start()
    {
        MyButton = GetComponent<Button>();
        MyButton.onClick.AddListener(OnClick);
    }


    void Update()
    {
        
    }

    public void OnClick()
    {
        if (MyUseable != null)
        {
            MyUseable.Use();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }
}
