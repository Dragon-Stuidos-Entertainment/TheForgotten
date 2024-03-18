using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Slider healthBarSlider;
    public Text healthText;
    public P_Health playerHealth;

    void Start()
    {
        healthBarSlider.value = 100f; // Set the initial value of the Slider to 100
    }

    void Update()
    {
        float healthPercentage = (float)playerHealth.currentHealth / playerHealth.maxHealth;
        healthBarSlider.value = healthPercentage * 100f; // Update the Slider value based on health percentage
        healthText.text = playerHealth.currentHealth + " / " + playerHealth.maxHealth;
    }
}