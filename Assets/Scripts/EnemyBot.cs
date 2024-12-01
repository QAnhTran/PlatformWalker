using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBot : MonoBehaviour
{
    public enum EnemyType { Melee, Ranged } // Define the types of enemies
    public EnemyType enemyType;            // Set this in the Inspector to choose the type

    public float detectionRange = 10f;     // Range at which the enemy detects the player
    public float attackRange = 2f;         // Range at which melee attacks are performed
    public float moveSpeed = 2f;           // Movement speed
    public float attackCooldown = 2.0f;    // Cooldown time between attacks
    public EnemyShoot enemyShoot;          // Script for ranged enemies to shoot bullets (only used for Ranged type)
    private float lastAttackTime = 0f;

    private Transform player;

    void Start()
    {
        // Find the player by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        UpdatePlayerTarget();
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);


        if (distanceToPlayer <= detectionRange)
        {
            // Follow the player if within detection range but outside attack range
            FollowPlayer();
            if (enemyType == EnemyType.Ranged)
            {
                RangedAttack();
            }
        }
    }

    void UpdatePlayerTarget()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void FollowPlayer()
    {
        // Move towards the player's position
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Rotate the enemy to face the player
        if (direction.x != 0)
        {
            float originalXScale = Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(Mathf.Sign(direction.x) * originalXScale, transform.localScale.y, transform.localScale.z);
        }
    }

    void MeleeAttack()
    {
        Debug.Log("Performing a melee attack on the player!");
        // Add melee attack logic here (e.g., reducing player's health on contact)
    }

    void RangedAttack()
    {
        Debug.Log("Performing a ranged attack on the player!");
        // Trigger the shooting behavior
        enemyShoot.GetComponent<EnemyShoot>().FireBullets();
        FollowPlayer();
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
