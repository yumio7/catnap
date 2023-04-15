using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OldManBehavior : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float cooldownDuration = 3;
    [SerializeField] private GameObject gunTip;
    [SerializeField] private float moveSpeed = 5;
    
    private float cooldown;
    private EnemyAI.FSMStates currentState;
    private GameObject player;
    private NavMeshAgent agent;



    void Start()
    {
        cooldown = cooldownDuration;
        currentState = EnemyAI.FSMStates.Attack;
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.isGameOver)
        {
            switch (currentState)
            {
                case EnemyAI.FSMStates.Patrol:
                    MoveState();
                    break;
                case EnemyAI.FSMStates.Attack:
                    AttackState();
                    break;
            }
        }
        else
        {
            agent.speed = 0;
        }
    }

    void MoveState()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            currentState = EnemyAI.FSMStates.Attack;
        }
        else
        {
            FaceTarget(player.transform.position);
            agent.SetDestination(player.transform.position);   
        }
    }

    void AttackState()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            if (cooldown <= 0)
            {
                FireBullet();
                cooldown = cooldownDuration;
            }
            cooldown -= Time.deltaTime;    
        }
        else
        {
            currentState = EnemyAI.FSMStates.Chase;
        }
        
    }

    private void FireBullet()
    {
        Instantiate(bullet, gunTip.transform);
    }
    
    private void Initialize()
    {
        currentState = EnemyAI.FSMStates.Patrol;

        player = GameObject.FindGameObjectWithTag("Player");

        agent = GetComponent<NavMeshAgent>();

    }
    
    private void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp
            (transform.rotation, lookRotation, 2 * Time.deltaTime);
    }
    
}
