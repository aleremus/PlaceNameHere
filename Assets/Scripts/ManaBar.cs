using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public float hudRegenerateCoefficient = 1;

    protected int maxMana;
    protected float currentMana;

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    private void Start()
    {
        slider.normalizedValue = 1;
    }

    public void SetMaxMana(int maxMana)
    {
        this.maxMana = maxMana;
        slider.maxValue = maxMana;
    }

    public void SetMaxAndCurrentMana(int maxMana, float currentMana)
    {
        SetMaxMana(maxMana);
        SetCurrentMana(currentMana);
    }

    public void SetMaxAndCurrentMana(int mana)
    {
        SetMaxMana(mana);
        SetCurrentMana(mana);
    }

    public void SetCurrentMana(float mana)
    {
        currentMana = mana;
        slider.value = Mathf.Lerp(slider.value, currentMana, Time.deltaTime * hudRegenerateCoefficient * maxMana /(slider.value * currentMana));

        fill.color = gradient.Evaluate(currentMana / maxMana);
    }
}
