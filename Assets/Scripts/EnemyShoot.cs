using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;

    private float timer;
    private GameObject player;

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
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
