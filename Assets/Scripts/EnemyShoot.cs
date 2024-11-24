using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bullet;        // The bullet prefab
    public Transform bulletPos;      // Position to fire the bullet

    private float timer;             // Tracks cooldown between shots

    public void FireBullets()
    {
        timer += Time.deltaTime;

        Debug.Log($"EnemyShoot Timer: {timer}"); // Debug timer progress

        if (timer > 2) // Adjust cooldown as needed
        {
            timer = 0;
            Shoot();
        }
    }

    void Shoot()
    {
        if (bullet != null && bulletPos != null)
        {
            Debug.Log("Shooting a bullet!"); // Log the shooting action
            Instantiate(bullet, bulletPos.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Bullet or bullet position is not assigned in EnemyShoot!");
        }
    }
}
