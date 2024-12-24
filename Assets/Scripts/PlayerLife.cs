using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public thanhmau thanhmau;
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private AudioSource SoundDeathEffect;

    private bool isAlive = true;
    public bool hasShield = false;

    private static bool isDrainingMana = false; // Static flag to ensure only one drain instance

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        thanhmau.capnhatthanhmau(SharedManaSystem.Instance.currentMana, SharedManaSystem.Instance.maxMana);

        // Start draining mana only if no other instance is already doing it
        if (!isDrainingMana)
        {
            isDrainingMana = true;
            InvokeRepeating("TruMauMoiGiay", 1.0f, 1.0f);
        }
    }

    private void TruMauMoiGiay()
    {
        if (isAlive)
        {
            SharedManaSystem.Instance.SpendMana(1);
            thanhmau.capnhatthanhmau(SharedManaSystem.Instance.currentMana, SharedManaSystem.Instance.maxMana);

            if (SharedManaSystem.Instance.currentMana <= 0)
            {
                Die();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            if (hasShield)
            {
                Debug.Log("Shield protected the player from the trap!");
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

    private void OnDestroy()
    {
        // Stop mana draining when this character is destroyed
        isDrainingMana = false;
    }
}
