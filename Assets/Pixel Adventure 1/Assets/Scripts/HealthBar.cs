using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public Image background;

    void Start()
    {
        // Ensure full opacity for fill and background
        SetFullOpacity(fill);
        SetFullOpacity(background);
    }

    void SetFullOpacity(Image image)
    {
        Color color = image.color;
        color.a = 1.0f;  // Set alpha to fully opaque
        image.color = color;
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
