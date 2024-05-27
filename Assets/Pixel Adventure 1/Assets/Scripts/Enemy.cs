using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public GameObject healthBarPrefab;
    private HealthBar healthBar;



    void Start()
    {
        currentHealth = maxHealth;

        // Instantiate the health bar and set its parent to the canvas
        GameObject canvas = GameObject.Find("Canvas");
        GameObject healthBarObject = Instantiate(healthBarPrefab, canvas.transform);

        // Position the health bar relative to the enemy
        healthBarObject.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1, 0)); // Adjust the offset as needed

        // Get the HealthBar component
        healthBar = healthBarObject.GetComponent<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        // Update the health bar position to follow the enemy
        if (healthBar != null)
        {
            healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1, 0));
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        if (healthBar != null)
        {
            Destroy(healthBar.gameObject);
        }
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
