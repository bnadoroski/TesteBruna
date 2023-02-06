using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    [SerializeField]
    Gradient gradient;
    [SerializeField]
    Image fill;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public int TakeDamage(int damage, int currentHealth, GameObject target)
    {
        currentHealth -= damage;
        if (currentHealth > 0)
        {
            slider.value = currentHealth;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }

        return currentHealth;
    }
}
