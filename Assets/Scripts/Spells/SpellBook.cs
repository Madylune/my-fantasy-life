using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpellBook : MonoBehaviour
{
    private static SpellBook instance;

    public static SpellBook MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SpellBook>();
            }
            return instance;
        }
    }

    [SerializeField]
    private Image castingBar;

    [SerializeField]
    private Text spellName;

    [SerializeField]
    private Text castTime;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Spell[] spells;

    private Coroutine spellRoutine;
    private Coroutine fadeRoutine;

    public Spell CastSpell(string skillName)
    {
        Spell spell = Array.Find(spells, x => x.MyName == skillName);

        castingBar.fillAmount = 0;

        castingBar.color = spell.MyBarColor;
        spellName.text = spell.MyName;
        icon.sprite = spell.MyIcon;

        spellRoutine = StartCoroutine(Progress(spell));

        fadeRoutine = StartCoroutine(FadeBar());

        return spell;
    }

    private IEnumerator Progress(Spell spell)
    {
        float timePassed = Time.deltaTime;
        float rate = 1.0f / spell.MyCastTime;
        float progress = 0.0f;

        while (progress <= 1.0)
        {
            castingBar.fillAmount = Mathf.Lerp(0, 1, progress);

            progress += rate * Time.deltaTime;

            timePassed += Time.deltaTime;
            castTime.text = (spell.MyCastTime - timePassed).ToString("F2");

            if (spell.MyCastTime - timePassed < 0)
            {
                castTime.text = "0.00";
            }
            
            yield return null;
        }

        StopCasting();
    }

    private IEnumerator FadeBar()
    {
        float rate = 1.0f / 0.25f; //Fade speed
        float progress = 0.0f;

        while (progress <= 1.0)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, progress);

            progress += rate * Time.deltaTime;
            
            yield return null;
        }
    }

    public void StopCasting()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
            fadeRoutine = null;
            canvasGroup.alpha = 0;
        }

        if (spellRoutine != null)
        {
            StopCoroutine(spellRoutine);
            spellRoutine = null;
        }
    }

    public Spell GetSpell(string spellName)
    {
        Spell spell = Array.Find(spells, x => x.MyName == spellName);
        return spell;
    }
}
