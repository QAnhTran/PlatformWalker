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

    private float damageRate = 1.0f; // Mức độ mất máu mỗi giây
    private bool isAlive = true; // Biến kiểm tra xem nhân vật còn sống hay không

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        luongmauhientai = luongmautoida;
        thanhmau.capnhatthanhmau(luongmauhientai, luongmautoida);

        // Gọi hàm TruMauMoiGiay sau mỗi 1 giây, bắt đầu từ sau 1 giây
        InvokeRepeating("TruMauMoiGiay", 1.0f, 1.0f);
    }

    private void TruMauMoiGiay()
    {
        if (isAlive)
        {
            // Trừ lượng máu mỗi giây
            luongmauhientai -= damageRate;
            // Cập nhật thanh máu
            thanhmau.capnhatthanhmau(luongmauhientai, luongmautoida);

            // Kiểm tra nếu máu dưới 0, thì gọi hàm Die
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
            Die();
        }
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
        }
    }

    private void Die()
    {
        SoundDeathEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        isAlive = false; 
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
