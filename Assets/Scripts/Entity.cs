using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public HealthBar healthBar; // Reference to the health bar

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth, maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
        healthBar.SetHealth(currentHealth, maxHealth);
    }


    void Die()
    {
        Destroy(gameObject);
        if (healthBar != null)
        {
            Destroy(healthBar.gameObject);
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth, maxHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Assume player deals damage on collision
            TakeDamage(10); // Adjust damage value as needed
        }
    }
}
