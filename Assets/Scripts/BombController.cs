using UnityEngine;

public class BombController : MonoBehaviour
{
    private float explosionDelay;
    private float explosionRadius;
    private int damage;
    public GameObject selectedItemPrefab;
    public PlayerMove player;
    public int throwForce;

    public GameObject explosionEffect; // Visual effect for the explosion

    public void SetupBomb(BombItem bombItem)
    {
        Invoke(nameof(Explode), explosionDelay);
    }

    public void ThrowBomb()
    {
        if (selectedItemPrefab != null && selectedItemPrefab.GetComponent<BombItem>() != null)
        {
            GameObject bombInstance = Instantiate(selectedItemPrefab, player.handTransform.position, Quaternion.identity);
            Rigidbody2D bombRb = bombInstance.GetComponent<Rigidbody2D>();

            // Apply force to throw the bomb (adjust direction and force as needed)
            Vector2 throwDirection = new Vector2(1f, 1f);  // Example: Right and slightly up
            bombRb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);

            // Start the explosion countdown
            Explode(bombInstance);
        }
    }

    private void Explode(GameObject bombInstance)
    {
        BombItem bombScript = bombInstance.GetComponent<BombItem>();

        // Find all colliders in the explosion radius
        Collider2D[] objectsToAffect = Physics2D.OverlapCircleAll(bombInstance.transform.position, bombScript.explosionRadius);

        foreach (Collider2D obj in objectsToAffect)
        {
            // Check if the object is a trap or enemy
            if (obj.CompareTag("Trap"))
            {
                Destroy(obj.gameObject);  // Destroy the trap
            }
            else if (obj.CompareTag("Enemy"))
            {
                Enemy enemyScript = obj.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.TakeDamage(bombScript.damage);  // Deal damage to the enemy
                }
            }
        }

        // Destroy the bomb after the explosion
        Destroy(bombInstance);
    }


    void OnDrawGizmosSelected()
    {
        // Draw the explosion radius in the editor for visualization
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
