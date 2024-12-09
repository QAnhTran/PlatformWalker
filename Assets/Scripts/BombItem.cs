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
    public int maxUses = 3;  
    public int usesLeft;

    private void Start()
    {
        usesLeft = maxUses;  
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
            if (nearbyObject.CompareTag("Trap"))
            {
                Debug.Log("Trap hit by explosion");
                Destroy(nearbyObject.gameObject); 
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
        }
        else
        {
            Debug.Log("No more uses left for this bomb!");
        }
    }

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

                if (tilemap.HasTile(currentCell))
                {
                    // Destroy or replace the tile
                    tilemap.SetTile(currentCell, null);  // Set tile to null to "destroy" it
                    Debug.Log("Tile destroyed at: " + currentCell);
                }
            }
        }
    }

    // visualize the explosion radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
