using UnityEngine;
using UnityEngine.AI;

public class AI_RoamAndChase : MonoBehaviour
{
    public Transform[] patrolPoints;
    public GameObject player;
    public float detectionAngle = 45f;
    public float detectionRange = 10f;
    public GameObject enemyProjectilePrefab;
    public float throwForce = 10f;
    public float attackRange = 100f;
    public float throwDelay = 2f;
    public Transform spawnPosition; // New field for the spawn position

    private NavMeshAgent agent;
    private bool isChasing = false;
    private int currentPatrolIndex = 0;
    private enemyAttack enemyAttackScript;
    private bool canAttackPlayer = false;
    private float lastThrowTime = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyAttackScript = GetComponent<enemyAttack>();
        enemyAttackScript.SetSpawnPosition(spawnPosition); // Set the spawn position in the enemyAttack script
        if (patrolPoints.Length > 0)
        {
            SetDestinationToNextPatrolPoint();
        }
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            StartChasingPlayer();
            if (Vector3.Distance(transform.position, player.transform.position) <= attackRange && Time.time - lastThrowTime > throwDelay && canAttackPlayer)
            {
                enemyAttackScript.AttackPlayer();
                lastThrowTime = Time.time;
            }
        }
        else if (isChasing)
        {
            if (player != null)
            {
                agent.SetDestination(player.transform.position);
            }

            if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
            {
                canAttackPlayer = true;
            }
            else
            {
                canAttackPlayer = false;
            }

            if (agent.remainingDistance < 1f)
            {
                StopChasingPlayer();
            }
        }
        else
        {
            if (agent.remainingDistance < 0.1f)
            {
                SetDestinationToNextPatrolPoint();
            }
        }
    }

    void SetDestinationToNextPatrolPoint()
    {
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    bool CanSeePlayer()
    {
        if (player == null)
        {
            return false;
        }

        Vector3 directionToPlayer = player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        if (angle < detectionAngle && directionToPlayer.magnitude < detectionRange)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionRange))
            {
                if (hit.transform == player.transform)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void StartChasingPlayer()
    {
        isChasing = true;
    }

    public void StopChasingPlayer()
    {
        isChasing = false;
        SetDestinationToNextPatrolPoint();
        canAttackPlayer = false;
    }
}