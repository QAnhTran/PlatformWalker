using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;    
    public Transform bulletSpawnPoint; 
    public float bulletSpeed = 10f;    
    public float shootCooldown = 0.5f; 

    private float shootTimer;          
    private int direction = 1;         // Hướng bắn: 1 = phải, -1 = trái

    void Update()
    {
        shootTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.A))
        {
            direction = -1; // Hướng trái
            UpdateBulletSpawnRotation(); 
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = 1; // Hướng phải
            UpdateBulletSpawnRotation(); 
        }

        if (Input.GetButtonDown("Fire1") && shootTimer <= 0)
        {
            Shoot();
            shootTimer = shootCooldown;
        }
    }

    void Shoot()
    {
        // Tạo đạn tại vị trí BulletSpawnPoint
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(direction * bulletSpeed, 0);

        Destroy(bullet, 5f);
    }

    void UpdateBulletSpawnRotation()
    {
        bulletSpawnPoint.localScale = new Vector3(direction, 1, 1);
    }
}
