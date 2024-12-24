using UnityEngine;

public class SharedManaSystem : MonoBehaviour
{
    public float maxMana = 10f;    // Maximum mana
    public float currentMana;     // Current mana
    public static SharedManaSystem Instance; // Singleton instance for easy access

    private void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentMana = maxMana; // Initialize mana to maximum
    }

    public void SpendMana(float amount)
    {
        if (currentMana >= amount)
        {
            currentMana -= amount;
        }
    }

    public void RestoreMana(float amount)
    {
        currentMana = Mathf.Min(currentMana + amount, maxMana);
    }

    public bool HasEnoughMana(float amount)
    {
        return currentMana >= amount;
    }
}
