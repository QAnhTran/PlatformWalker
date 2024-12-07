using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public HealthBar healthBar; 

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

  private void OnCollisionEnter2D(Collision2D collision)
{
    // Kiểm tra nếu va chạm với tag "Bullet"
    if (collision.gameObject.CompareTag("Bullet"))
    {
        TakeDamage(10);
    }

    // Kiểm tra nếu va chạm với tag "Player" và Transform có tên cụ thể
    if (collision.gameObject.CompareTag("Player") && collision.transform.name == "Transform")
    {
        // Không nhận damage
        Debug.Log("Collided with Player and Specific Transform, no damage taken.");
        return;
    }

    // Kiểm tra nếu chỉ va chạm với tag "Player"
    if (collision.gameObject.CompareTag("Player"))
    {
        TakeDamage(20);
    }
}

 
}
