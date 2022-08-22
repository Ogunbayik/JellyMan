using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public HealthBar healthBar;

    [SerializeField] private int maxHealth;

    private int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    private void OnEnable()
    {
        PlayerTriggered.OnTriggeredArrow += TakeDamage;
        PlayerTriggered.OnTriggeredPotion += AddHealth;
    }
    private void OnDisable()
    {
        PlayerTriggered.OnTriggeredArrow -= TakeDamage;
        PlayerTriggered.OnTriggeredPotion -= AddHealth;
    }
    private void Update()
    {
        Death();
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
    public void AddHealth(int health)
    {
        currentHealth += health;
        healthBar.SetHealth(currentHealth);

        if(currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void Death()
    {
        if(currentHealth <= 0)
        {
            GameManager.Instance.currentState = GameManager.GameStates.GameOver;
        }
    }
}
