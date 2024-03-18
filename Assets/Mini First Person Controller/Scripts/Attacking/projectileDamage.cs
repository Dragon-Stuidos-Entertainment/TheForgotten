using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileDamage : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    //detects collision with enemy
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyHealth enemy = collision.gameObject.GetComponent<enemyHealth>();
            if (enemy != null)
            {
                // Temporarily set the enemy's Rigidbody to kinematic to prevent pushing
                Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                if (enemyRigidbody != null)
                {
                    enemyRigidbody.isKinematic = true;
                }

                enemy.TakeDamage(damage);

                // Reset the enemy's Rigidbody to non-kinematic after taking damage
                if (enemyRigidbody != null)
                {
                    enemyRigidbody.isKinematic = false;
                }
            }
            Destroy(gameObject); // Destroy the projectile after it hits the enemy
        }
    }
}