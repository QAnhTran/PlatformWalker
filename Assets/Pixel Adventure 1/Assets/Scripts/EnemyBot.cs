using System.Collections;
using UnityEngine;

public class EnemyBot : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float attackRange = 1.5f;
    public float attackCooldown = 2f;
    private float attackTimer;
    public int damage = 10;
    private Transform player;

    private void Start()
    {
        // Find the player using the tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
        attackTimer = attackCooldown; // Initialize attack timer
    }

    private void Update()
    {
        if (player == null)
            return;

        // Move towards the player
        MoveTowardsPlayer();

        // Attack if in range
        attackTimer -= Time.deltaTime;
        if (Vector3.Distance(transform.position, player.position) <= attackRange && attackTimer <= 0)
        {
            AttackPlayer();
            attackTimer = attackCooldown;  // Reset the attack timer
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void AttackPlayer()
    {
        Debug.Log("Enemy attacks player!");
        // Assuming the player has a script that manages health
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);  // Apply damage to the player
        }
    }
}
