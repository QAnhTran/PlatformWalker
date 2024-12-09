using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
     void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") || 
            collision.collider.CompareTag("Terrain") || 
            collision.collider.CompareTag("Trap"))
        {
            Destroy(gameObject);
        }
    }
}
