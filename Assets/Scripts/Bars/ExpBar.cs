using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    public Image fill;

    [SerializeField]
    public Text expText;

    [SerializeField]
    private float lerpSpeed;

    private float currentFill;

    private float overflow;

    public float MyMaxValue { get; set; }

    public float MyCurrentValue
    {
        get => currentValue;

        set
        {
            if (value > MyMaxValue)
            {
                overflow = value - MyMaxValue;
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

            expText.text = currentValue + "/" + MyMaxValue;
        }
    }

    public bool IsFull { get => slider.value == MyMaxValue; }

    public float MyOverflow
    {
        get
        {
            float tmp = overflow;
            overflow = 0;
            return tmp;
        }
    }

    private float currentValue;

    private void Update()
    {
        HandleBar();
    }

    public void Initialize(float currentValue, float maxValue)
    {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;

        slider.maxValue = maxValue;
    }

    public void SetMaxValue(float maxValue)
    {
        MyMaxValue = maxValue;
        slider.maxValue = maxValue;
    }

    private void HandleBar()
    {
        if (currentFill != slider.value)
        {
            // Smooth bar's fill
            slider.value = Mathf.MoveTowards(slider.value, currentFill, Time.deltaTime * lerpSpeed);
        }
    }

    public void Reset()
    {
        slider.value = 0;
    }
}
