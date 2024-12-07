using UnityEngine;
using UnityEngine.Tilemaps;

public class BombItem : MonoBehaviour
{
    public GameObject prefab;
    public float explosionRadius = 3f;
    public int damage = 50;
    public GameObject explosionEffect;
    public LayerMask targetLayers;
    public Entity enemy;
    public int maxUses = 3;  // Set the maximum number of uses
    public int usesLeft;

    private void Start()
    {
        usesLeft = maxUses;  // Initialize with the maximum uses
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bomb hit: " + collision.gameObject.name);
        Explode();
    }

    private void Explode()
    {
        Debug.Log("Bomb exploded!");

        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        Collider2D[] objectsToDamage = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D nearbyObject in objectsToDamage)
        {
            // Check if the object has the tag "Trap" and destroy it
            if (nearbyObject.CompareTag("Trap"))
            {
                Debug.Log("Trap hit by explosion");
                Destroy(nearbyObject.gameObject);  // Destroy the trap object
            }

            if (nearbyObject.CompareTag("Enemy"))
            {
                Debug.Log("Enemy hit by explosion");
                enemy.TakeDamage(10);  
            }

            // Handle Tilemap destruction
            Tilemap tilemap = nearbyObject.GetComponent<Tilemap>();
            if (tilemap != null)
            {
                DestroyTiles(tilemap, transform.position);
            }
        }

        Destroy(gameObject);
    }

    public void UseBomb()
    {
        if (usesLeft > 0)
        {
            usesLeft--;
            Debug.Log("Bomb used. Uses left: " + usesLeft);
            // Implement your bomb-throwing logic here
        }
        else
        {
            Debug.Log("No more uses left for this bomb!");
        }
    }


    // Function to destroy tiles within the explosion radius
    private void DestroyTiles(Tilemap tilemap, Vector3 explosionPosition)
    {
        // Convert explosion position from world space to cell (grid) position
        Vector3Int cellPosition = tilemap.WorldToCell(explosionPosition);

        // Iterate over a small area around the explosion (based on explosion radius)
        int radius = Mathf.CeilToInt(explosionRadius);
        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                Vector3Int currentCell = new Vector3Int(cellPosition.x + x, cellPosition.y + y, cellPosition.z);

                // Check if there is a tile at the current position
                if (tilemap.HasTile(currentCell))
                {
                    // Destroy or replace the tile
                    tilemap.SetTile(currentCell, null);  // Set tile to null to "destroy" it
                    Debug.Log("Tile destroyed at: " + currentCell);
                }
            }
        }
    }

    // Optional: To visualize the explosion radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
