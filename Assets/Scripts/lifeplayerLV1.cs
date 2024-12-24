using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lifeplayerLV1 : MonoBehaviour
{
    public thanhmau thanhmau;
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private AudioSource SoundDeathEffect;
    public bool hasShield = false;
    public bool isShieldActivated = false;
    private bool isAlive = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        thanhmau.capnhatthanhmau(SharedManaSystem.Instance.currentMana, SharedManaSystem.Instance.maxMana);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && SharedManaSystem.Instance.HasEnoughMana(1))
        {
            SharedManaSystem.Instance.SpendMana(1);
            thanhmau.capnhatthanhmau(SharedManaSystem.Instance.currentMana, SharedManaSystem.Instance.maxMana);

            if (SharedManaSystem.Instance.currentMana <= 0)
            {
                Die();
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && hasShield)
        {
            isShieldActivated = true;
            Debug.Log("Shield activated!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            if (isShieldActivated)
            {
                Debug.Log("Shield protected the player from the trap!");
                isShieldActivated = false;
                hasShield = false;
            }
            else if (isAlive)
            {
                Debug.Log("Player hit by trap without shield - calling Die()");
                Die();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cherry"))
        {
            SharedManaSystem.Instance.RestoreMana(10);
            thanhmau.capnhatthanhmau(SharedManaSystem.Instance.currentMana, SharedManaSystem.Instance.maxMana);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Shield"))
        {
            hasShield = true;
            Debug.Log("Shield collected!");
            Destroy(other.gameObject);
        }
    }

    private void Die()
    {
        if (isAlive)
        {
            Debug.Log("Player died!");
            SoundDeathEffect.Play();
            rb.bodyType = RigidbodyType2D.Static;
            anim.SetTrigger("death");
            isAlive = false;
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
