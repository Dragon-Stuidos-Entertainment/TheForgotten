using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_RoamAndChase : MonoBehaviour
{
    public Transform[] patrolPoints;
    public GameObject player; // Change to GameObject type
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
            // Add additional logic for attacking or other behaviors when chasing the player
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
                    return true; // Player is within sight
                }
            }
        }

        return false; // Player is not within sight
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
}