using UnityEngine;

public class enemyAttack : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float throwForce = 10f;
    [SerializeField] float forwardForce = 5f;
    [SerializeField] float attackRange = 10f;
    [SerializeField] float throwDelay = 2f;
    [SerializeField] float projectileLifespan = 2f; // New variable for projectile lifespan

    private Transform playerTransform;
    private bool playerInRange = false;
    private float lastThrowTime = 0f;
    private Transform spawnPosition;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (playerInRange && Vector3.Distance(transform.position, playerTransform.position) <= attackRange && Time.time - lastThrowTime > throwDelay)
        {
            AttackPlayer();
            lastThrowTime = Time.time;
        }
    }

    public void SetSpawnPosition(Transform spawnPos)
    {
        spawnPosition = spawnPos;
    }

    public void AttackPlayer()
    {
        if (spawnPosition != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, spawnPosition.position, Quaternion.identity);

            Vector3 direction = (playerTransform.position - spawnPosition.position).normalized;
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(direction * throwForce, ForceMode.Impulse);
            rb.AddForce(transform.forward * forwardForce, ForceMode.Impulse);

            // Destroy the projectile after a specified time
            Destroy(projectile, projectileLifespan);
        }
        else
        {
            Debug.LogWarning("Spawn position is not set. Cannot attack player.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}