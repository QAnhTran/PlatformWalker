using UnityEngine;
using UnityEngine.SceneManagement;

public class thanhmaunguoichoi : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] AudioSource SoundDeathEffect;

    public thanhmau thanhmau;
    public float luongmauhientai;
    public float luongmautoida = 10;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        luongmauhientai = luongmautoida;
        thanhmau.capnhatthanhmau(luongmauhientai, luongmautoida);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
       
            luongmauhientai -= 2;
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
     
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
  
   
}
