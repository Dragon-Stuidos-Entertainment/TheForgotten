using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealthUI : MonoBehaviour
{
    public Slider healthSlider;
    public enemyHealth enemyHealth;

    void Start()
    {
        healthSlider.value = 100f;
        enemyHealth.OnHealthChanged += UpdateHealthSlider;
    }

    void UpdateHealthSlider()
    {
        float healthPercentage = (float)enemyHealth.currentHealth / enemyHealth.maxHealth;
        healthSlider.value = healthPercentage * 100f;
    }
}