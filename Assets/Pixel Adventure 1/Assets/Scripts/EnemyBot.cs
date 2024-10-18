using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBot : MonoBehaviour
{
    public float detectionRange = 10f;  // Distance at which the enemy starts following the player
    public float attackRange = 2f;      // Distance at which the enemy attacks the player
    public float moveSpeed = 2f;        // Movement speed of the enemy
    public float attackCooldown = 2.0f; // Time between attacks
    private float attackTimer = 0f;
    private float lastAttackTime;
    
    private Transform player;
    private bool isAttacking = false;

    void Start()
    {
        // Find the player by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            // Stop moving and attack the player
            if (Time.time > lastAttackTime + attackCooldown)
            {
                AttackPlayer();
                lastAttackTime = Time.time;
            } 
        }
        else if (distanceToPlayer <= detectionRange)
        {
            // Follow the player if outside attack range but within detection range
            FollowPlayer();
        }

        // Cooldown countdown
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    void FollowPlayer()
    {
        // Move towards the player's position
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Optional: rotate the enemy to face the player
        if (direction.x != 0)
        {
            float originalXScale = Mathf.Abs(transform.localScale.x);  // Get original scale
            transform.localScale = new Vector3(Mathf.Sign(direction.x) * originalXScale, transform.localScale.y, transform.localScale.z);
        }
    }

    void AttackPlayer()
    {
        Debug.Log("Attacking the player!");
        // Add attack logic here, such as reducing the player's health
    }

    void OnDrawGizmosSelected()
    {
        // Visualize the detection and attack range in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
