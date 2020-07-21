using UnityEngine;
using UnityEngine.EventSystems;

public class SpellButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private string spellName;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // Pick up the spell
            HandScript.MyInstance.TakeMoveable(SpellBook.MyInstance.GetSpell(spellName));
        }
    }
}
