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
        GameObject canvas = GameObject.Find("Canvas");
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
            TakeDamage(10); 
        }
    }

}
