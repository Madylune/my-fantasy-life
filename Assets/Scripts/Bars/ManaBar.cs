using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Image fill;

    [SerializeField]
    private Text manaText;

    private float currentValue;

    private float currentFill;

    public float MyMaxValue { get; set; }

    public float MyCurrentValue
    {
        get => currentValue;

        set
        {
            if (value > MyMaxValue)
            {
                currentValue = MyMaxValue;
            }
            else if (value < 0)
            {
                currentValue = 0;
            }
            else
            {
                currentValue = value;
            }

            currentFill = currentValue;
            manaText.text = currentValue + "/" + MyMaxValue;
        }
    }

    public void Initialize(float currentValue, float maxValue)
    {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;

        slider.maxValue = maxValue;
        slider.value = currentValue;
    }

    public void SetMaxMana(float mana)
    {
        MyMaxValue = mana;
        slider.maxValue = mana;
    }

    public void SetMana(float mana)
    {
        slider.value = mana;
        MyCurrentValue = mana;
    }
}
