using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CombatTextType { DAMAGE, HEAL, HIT }

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
        txt.text = text;

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
            default:
                break;
        }

        if (crit)
        {
            txt.GetComponent<Animator>().SetBool("Crit", crit);
        }
    }
}
