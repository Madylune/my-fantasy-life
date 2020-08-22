using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CombatTextType { DAMAGE, HEAL, HIT, EXP, LVL }

public class CombatTextManager : MonoBehaviour
{
    private static CombatTextManager instance;

    public static CombatTextManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CombatTextManager>();
            }
            return instance;
        }
    }

    [SerializeField]
    private GameObject combatText;

    public void CreateText(Vector2 position, string text, CombatTextType type, bool crit)
    {
        position.y += 0.3f; // Offset

        Text txt = Instantiate(combatText, transform).GetComponent<Text>();
        txt.transform.position = position;

        string after = string.Empty; 

        switch (type)
        {
            case CombatTextType.DAMAGE:
                txt.color = Color.red;
                break;
            case CombatTextType.HEAL:
                txt.color = Color.green;
                break;
            case CombatTextType.HIT:
                txt.color = crit ? Color.yellow : Color.white;
                break;
            case CombatTextType.EXP:
                after = " XP";
                break;
            case CombatTextType.LVL:
                txt.color = new Color32(255, 124, 0, 255); // orange
                break;
            default:
                break;
        }

        txt.text = text + after;

        if (crit)
        {
            txt.GetComponent<Animator>().SetBool("Crit", crit);
        }
    }
}
