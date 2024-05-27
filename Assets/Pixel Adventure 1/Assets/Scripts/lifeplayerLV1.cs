using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lifeplayerLV1    : MonoBehaviour
{
    public thanhmau thanhmau;
    public float luongmauhientai;
    public float luongmautoida = 10;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }

      


        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (luongmauhientai > 0)) // Kiểm tra nếu người chơi nhấn phím Space và còn máu
        {
            // Trừ lượng máu khi nhảy
            luongmauhientai -= 1;
            thanhmau.capnhatthanhmau(luongmauhientai, luongmautoida);
            if(luongmauhientai <= 0)
            {
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
            // Hồi máu khi ăn cherry
            luongmauhientai += 10;
            if (luongmauhientai > luongmautoida)
            {
                luongmauhientai = luongmautoida; // Đảm bảo máu không vượt quá máu tối đa
            }
            thanhmau.capnhatthanhmau(luongmauhientai, luongmautoida);
            Destroy(other.gameObject); // Xóa cherry sau khi ăn
        }
    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}