using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float force;
    private GameObject player;
    private Rigidbody2D rigidbody2D;
    private float timer = 0f;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rigidbody2D.velocity = new Vector2(direction.x, direction.y).normalized * force;

    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 5)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")) 
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(20);
            Destroy(gameObject);
        }
    }

}
