using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private Transform playerTransform;
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();

        RegularMovespeed();
    }

    private void Update()
    {
            navMeshAgent.SetDestination(playerTransform.position);
    }

    public void SlowMovespeed()
    {
        navMeshAgent.speed /= 2;
    }

    public void RegularMovespeed()
    {
        navMeshAgent.speed = _moveSpeed;
    }
}