using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lifeplayerLV1 : MonoBehaviour
{
    public thanhmau thanhmau;
    public float luongmauhientai;
    public float luongmautoida = 10;
    public bool hasShield = false;
    public bool isShieldActivated = false;
    private bool isAlive = true;
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] AudioSource SoundDeathEffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        luongmauhientai = luongmautoida;
        thanhmau.capnhatthanhmau(luongmauhientai, luongmautoida);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (luongmauhientai > 0)) 
        {
            luongmauhientai -= 1;
            thanhmau.capnhatthanhmau(luongmauhientai, luongmautoida);
            if (luongmauhientai <= 0)
            {
                Die();
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && hasShield == true) 
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

    private void Die()
    {
        SoundDeathEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
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
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Shield"))
        {
            hasShield = true;
            Debug.Log("Shield activated!");
            Destroy(other.gameObject); 
        }
    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}