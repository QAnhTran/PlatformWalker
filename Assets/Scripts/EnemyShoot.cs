using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bullet;       
    public Transform bulletPos;      

    private float timer;             // Tracks cooldown between shots

    public void FireBullets()
    {
        timer += Time.deltaTime;

        if (timer > 2) 
        {
            timer = 0;
            Shoot();
        }
    }

    void Shoot()
    {
        if (bullet != null && bulletPos != null)
        {
            Instantiate(bullet, bulletPos.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Bullet or bullet position is not assigned in EnemyShoot!");
        }
    }
}
