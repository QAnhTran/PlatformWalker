using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public Transform entity; 
    public Vector3 offset; 

    private RectTransform healthBarRect;
    private Camera mainCamera;

    void Start()
    {
        healthBarRect = GetComponent<RectTransform>();
        mainCamera = Camera.main;
    }

    public void SetHealth(float health, float maxHealth)
    {
        Image healthBarFill = healthBarRect.Find("HealthBarFill").GetComponent<Image>();
        Debug.Log(!healthBarFill);
        healthBarFill.fillAmount = health / maxHealth;
    } 
}
