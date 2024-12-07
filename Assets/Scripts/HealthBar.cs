using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public Transform entity; // Reference to the entity the health bar is following
    public Vector3 offset; // Offset from the entity position

    private RectTransform healthBarRect;
    private Camera mainCamera;

    void Start()
    {
        healthBarRect = GetComponent<RectTransform>();
        mainCamera = Camera.main;
    }

    public void SetHealth(float health, float maxHealth)
    {
        // Assume HealthBarFill is a child with an Image component
        Image healthBarFill = healthBarRect.Find("HealthBarFill").GetComponent<Image>();
        Debug.Log(!healthBarFill);
        healthBarFill.fillAmount = health / maxHealth;
    } 
}
