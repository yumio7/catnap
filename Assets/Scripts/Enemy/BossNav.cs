using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossNav : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private GameObject[] wanderPoints;
    private NavMeshAgent navMeshAgent;
    private Vector3 nextDestination;

    private void Start()
    {
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        navMeshAgent = GetComponent<NavMeshAgent>();

        RegularMovespeed();

        FindNextPoint();
    }

    private void Update()
    {
        navMeshAgent.stoppingDistance = 0;
        if (Vector3.Distance(transform.position, nextDestination) < 1)
        {
            FindNextPoint();
        }
    }

    public void SlowMovespeed()
    {
        navMeshAgent.speed /= 2;
    }

    public void RegularMovespeed()
    {
        navMeshAgent.speed = _moveSpeed;
    }
    
    private void FindNextPoint()
    {
        nextDestination = wanderPoints[Random.Range(0, wanderPoints.Length)].transform.position;

        // currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;

        navMeshAgent.SetDestination(nextDestination);
    }
}
