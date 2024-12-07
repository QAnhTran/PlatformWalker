using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra va chạm với vật thể có tag "Enemy", "Terrain", hoặc "Trap"
        if (collision.collider.CompareTag("Enemy") || 
            collision.collider.CompareTag("Terrain") || 
            collision.collider.CompareTag("Trap"))
        {
            // Xóa đạn sau khi va chạm với vật thể có tag Enemy, Terrain, hoặc Trap
            Destroy(gameObject);
        }
    }
}
