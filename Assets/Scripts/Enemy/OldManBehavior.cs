using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OldManBehavior : MonoBehaviour
{
    public float attackRange;
    private EnemyAI.FSMStates currentState;
    private GameObject player;
    private NavMeshAgent agent;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveState()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            currentState = EnemyAI.FSMStates.Attack;
        }
        else
        {
            agent.SetDestination(player.transform.position);   
        }
    }

    void AttackState()
    {
        
    }
    
    private void Initialize()
    {
        currentState = EnemyAI.FSMStates.Patrol;

        player = GameObject.FindGameObjectWithTag("Player");

        agent = GetComponent<NavMeshAgent>();

    }
    
}
