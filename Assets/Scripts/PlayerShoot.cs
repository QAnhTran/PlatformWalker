using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;    // Prefab của đạn
    public Transform bulletSpawnPoint; // Vị trí xuất phát của đạn
    public float bulletSpeed = 10f;    // Tốc độ của đạn
    public float shootCooldown = 0.5f; // Thời gian chờ giữa các lần bắn

    private float shootTimer;          // Bộ đếm thời gian bắn
    private int direction = 1;         // Hướng bắn: 1 = phải, -1 = trái

    void Update()
    {
        // Giảm thời gian chờ giữa các lần bắn
        shootTimer -= Time.deltaTime;

        // Thay đổi hướng bắn khi ấn A hoặc D
        if (Input.GetKeyDown(KeyCode.A))
        {
            direction = -1; // Hướng trái
            UpdateBulletSpawnRotation(); // Xoay hướng bắn
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = 1; // Hướng phải
            UpdateBulletSpawnRotation(); // Xoay hướng bắn
        }

        // Bắn khi nhấn chuột trái
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

        // Gán vận tốc cho đạn
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(direction * bulletSpeed, 0);

        // Hủy đạn sau 5 giây để tránh tràn bộ nhớ
        Destroy(bullet, 5f);
    }

    void UpdateBulletSpawnRotation()
    {
        // Xoay hướng của BulletSpawnPoint dựa trên hướng bắn
        bulletSpawnPoint.localScale = new Vector3(direction, 1, 1);
    }
}
