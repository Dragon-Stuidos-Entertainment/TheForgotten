using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    [SerializeField] private GameObject playerProjectile;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float objectSpeed = 10f;
    [SerializeField] private float objectDamage = 10f;
    [SerializeField] private float throwForce = 15f;
    [SerializeField] private float lifeSpan = 2f;

    private float nextAttackTime = 0f;
    [SerializeField] private float attackRate = 2f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            Attack();
        }
    }

    void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + 1f / attackRate;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Calculate the direction based on the mouse position
            Vector3 direction = ray.direction.normalized;

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // Find all objects with the "Enemy" tag
            if (enemies.Length > 0)
            {
                GameObject enemy = enemies[0]; // Assuming there is only one enemy for simplicity

                GameObject spawnedObject = Instantiate(playerProjectile, attackPoint.position, Quaternion.identity); // Spawn at attackPoint position
                if (spawnedObject != null)
                {
                    Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.AddForce(direction * objectSpeed, ForceMode.Impulse); // Add force in the direction of the mouse look
                        rb.AddForce(direction * throwForce, ForceMode.Impulse); // Add additional force to simulate throwing

                        Destroy(spawnedObject, lifeSpan); // Destroy the projectile after the specified lifespan
                    }
                }
            }
        }
    }
}