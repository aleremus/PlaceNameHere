using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float hudRegenerateCoefficient = 1;

    protected int maxHealth;
    protected float currentHealth;

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    void Start()
    {
        slider.normalizedValue = 1;
    }

    public void SetMaxHeath(int maxHealth)
    {
        this.maxHealth = maxHealth;
        slider.maxValue = maxHealth;
    }

    public void SetMaxAndCurrentHeath(int maxHealth, float currentHealth)
    {
        SetMaxHeath(maxHealth);
        SetCurrentHealth(currentHealth);
    }

    public void SetMaxAndCurrentHeath(int health)
    {
        SetMaxHeath(health);
        SetCurrentHealth(health);
    }

    public void SetCurrentHealth(float health)
    {
        currentHealth = health;
        slider.value = Mathf.Lerp(slider.value, currentHealth, Time.deltaTime * hudRegenerateCoefficient * maxHealth/ (slider.value * currentHealth));

        fill.color = gradient.Evaluate(currentHealth / maxHealth);
    }
}
