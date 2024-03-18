using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int blockChancePercent = 30; // Adjust the block chance percentage as needed
    private GameObject objectToThrow;
    public float bounceForce = 10f; // Adjust the force of the bounce as needed

    void Update()
    {
        if (objectToThrow != null)
        {
            // Check if the enemy blocks the object based on the block chance percentage
            if (Random.Range(0, 100) < blockChancePercent)
            {
                Debug.Log("Enemy blocked the object");
                HandleBlockedObject();
            }
        }
    }

    void HandleBlockedObject()
    {
        // Add logic here to handle the blocked object
        Debug.Log("Object blocked by the enemy. Implement your logic here.");

        // Apply a force to bounce the object off the enemy
        Rigidbody objectRigidbody = objectToThrow.GetComponent<Rigidbody>();
        Vector3 bounceDirection = (objectToThrow.transform.position - transform.position).normalized;
        objectRigidbody.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("canPickUp"))
        {
            Debug.Log("Enemy tried to catch the object but failed");
            Destroy(gameObject); // Destroy the object if the enemy fails to catch it
        }
    }
}