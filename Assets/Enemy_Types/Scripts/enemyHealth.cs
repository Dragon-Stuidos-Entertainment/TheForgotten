using System;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 100;

    public event Action OnHealthChanged; // Define the event

    void Start()
    {
        currentHealth = maxHealth; // Initialize current health to max health
    }

    // Method to simulate enemy taking damage
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        
        if (currentHealth <= 0)
        {
            currentHealth = 0; // Ensure health doesn't go below 0
            Die(); // Call the Die method when health reaches 0
        }

        if (OnHealthChanged != null)
        {
            OnHealthChanged.Invoke(); // Trigger the event when the health changes
        }
    }

    // Method to handle the destruction of the object
    void Die()
    {
        Debug.Log("Enemy has died");
        // Any additional logic before destroying the object can be added here
        Destroy(gameObject); // Destroy the object when health reaches 0
    }
}