using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Transform entity; // Reference to the entity the health bar is following
    public Vector3 offset; // Offset from the entity position

    private RectTransform healthBarRect;
    private Camera mainCamera;

    void Start()
    {
        healthBarRect = GetComponent<RectTransform>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (entity != null)
        {
            // Convert the entity's world position to screen position
      //      Vector3 screenPos = mainCamera.WorldToScreenPoint(entity.position + offset);

            // Update the health bar position
         //   healthBarRect.position = screenPos;
        }
    }

    public void SetHealth(float health, float maxHealth)
    {
        // Assume HealthBarFill is a child with an Image component
        Image healthBarFill = healthBarRect.Find("HealthBarFill").GetComponent<Image>();
        Debug.Log(!healthBarFill);
        healthBarFill.fillAmount = health / maxHealth;
    }
}
