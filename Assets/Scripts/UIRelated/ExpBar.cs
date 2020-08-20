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

            expText.text = currentValue + "/" + MyMaxValue;
        }
    }

    private float currentValue;

    private void Start()
    {

    }

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

    private void HandleBar()
    {
        if (currentFill != slider.value)
        {
            // Smooth bar's fill
            slider.value = Mathf.MoveTowards(slider.value, currentFill, Time.deltaTime * lerpSpeed);
        }
    }
}
