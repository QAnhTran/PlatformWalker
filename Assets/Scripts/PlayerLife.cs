using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public thanhmau thanhmau;
    public float luongmauhientai;
    public float luongmautoida = 10;
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] AudioSource SoundDeathEffect;

    private float damageRate = 1.0f; 
    private bool isAlive = true;

    public bool hasShield = false; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        luongmauhientai = luongmautoida;
        thanhmau.capnhatthanhmau(luongmauhientai, luongmautoida);

        InvokeRepeating("TruMauMoiGiay", 1.0f, 1.0f);
    }

    private void TruMauMoiGiay()
    {
        if (isAlive)
        {
            luongmauhientai -= damageRate;
            thanhmau.capnhatthanhmau(luongmauhientai, luongmautoida);

            if (luongmauhientai <= 0)
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
        if (other.gameObject.CompareTag("Cherry"))
        {
            luongmauhientai += 10;
            if (luongmauhientai > luongmautoida)
            {
                luongmauhientai = luongmautoida;
            }
            thanhmau.capnhatthanhmau(luongmauhientai, luongmautoida);
        }

        if (other.gameObject.CompareTag("Shield"))
        {
            hasShield = true;
            Debug.Log("Shield activated!");
            Destroy(other.gameObject); 
        }
    }

    private void Die()
    {
        if (isAlive)
        {
            Debug.Log("Die method called - Player is now dead.");
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
