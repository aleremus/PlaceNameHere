using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacterstics : MonoBehaviour
{
    public UnityEvent onDeathEvents;

    public float playerSpeed = 5;

    [SerializeField, Range(0, 1000)] public int maxHealth;
    [SerializeField, Range(0, 1000)] public float healthRegenerationPerSecond;
    private float currentHealth;

    [SerializeField, Range(0, 1000)] public int maxMana;
    [SerializeField, Range(0, 1000)] public float manaRegenerationPerSecond;
    private float currentMana;

    public HealthBar healthBar;
    public ManaBar manaBar;
    public bool isDead => currentHealth <= 0;

    private void Update()
    {
        RegenerateHealth(healthRegenerationPerSecond  *Time.deltaTime);
        RegenerateMana(manaRegenerationPerSecond * Time.deltaTime);
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxAndCurrentHeath(maxHealth);

        currentMana = maxMana;
        manaBar.SetMaxAndCurrentMana(maxMana);
    }

    public void DealDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetCurrentHealth(currentHealth);
        Debug.Log("You have lost " + damage + "HP!");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void RegenerateHealth(float health)
    {
        currentHealth += health;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        healthBar.SetCurrentHealth(currentHealth);
    }


    private void Die()
    {
        onDeathEvents.Invoke();
        Destroy(gameObject);
    }

    public bool IsEnoughMana(int neededMana)
    {
        return neededMana <= currentMana;
    }

    public void SpendMana(int mana)
    {
        currentMana = IsEnoughMana(mana) ? currentMana - mana : 0;
        manaBar.SetCurrentMana(currentMana);
    }

    public void RegenerateMana(float mana)
    {
        currentMana += mana;
        if (currentMana > maxMana) currentMana = maxMana;
        manaBar.SetCurrentMana(currentMana);
    }
}
