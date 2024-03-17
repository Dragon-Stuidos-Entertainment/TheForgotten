using UnityEngine;
using UnityEngine.AI;
public class AI_RoamAndChase : MonoBehaviour
{
    public Transform[] patrolPoints;
    public GameObject player;
    public float detectionAngle = 45f;
    public float detectionRange = 10f;
    private NavMeshAgent agent;
    private bool isChasing = false;
    private int currentPatrolIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
        }
        else if (isChasing)
        {
            if (player != null)
            {
                agent.SetDestination(player.transform.position);
            }
            
            if (agent.remainingDistance < 1f)
            {
                StopChasingPlayer();
                attackPlayer();
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
    }

    void attackPlayer()
    {
        Destroy(player);
    }
}