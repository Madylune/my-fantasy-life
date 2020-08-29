using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    [SerializeField]
    private Text healthText;

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
            healthText.text = currentValue + "/" + MyMaxValue;
        }
    }

    private float currentValue;

    public void Initialize(float currentValue, float maxValue)
    {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;

        slider.maxValue = maxValue;
        slider.value = currentValue;
    }

    public void SetMaxHealth(float health)
    {
        MyMaxValue = health;
        slider.maxValue = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        slider.value = health;
        MyCurrentValue = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
