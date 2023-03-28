using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    public enum FSMStates 
    {
          Idle,
          Patrol,
          Chase,
          Attack,
          Back,
    }

    public FSMStates currentState;
    
    public float chaseDistance = 10;
    public float enemySpeed = 5;
    public GameObject player;

    private GameObject[] wanderPoints;
    private Vector3 nextDestination;
    private float distanceToPlayer;
    public int damageAmount = 20;

    private int currentDestinationIndex = 0;

    private NavMeshAgent agent;

    public Transform enemyEyes;
    public float fieldOfView = 150f;
    
    private float hitDelay = 0.5f;
    private float counter;
    private bool canHit = true;
    
    
    private float elapsedTime = 0.0f;
    
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance
            (transform.position, player.transform.position);

        switch (currentState)
        {
            case FSMStates.Patrol:
                UpdatePatrolState();
                break;
            case FSMStates.Chase:
                UpdateChaseState();
                break;
            case FSMStates.Attack:
                UpdateAttackState();
                break;
            case FSMStates.Back:
                UpdateBackState();
                break;
        }

        elapsedTime += Time.deltaTime;
        
        if (counter > hitDelay)
        {
            canHit = true;
        }

        counter += Time.deltaTime;
    }

    void Initialize()
    {
        currentState = FSMStates.Patrol;
        
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");

        player = GameObject.FindGameObjectWithTag("Player");

        agent = GetComponent<NavMeshAgent>();
        
        counter = hitDelay;

        FindNextPoint();
    }
    
    void UpdatePatrolState()
    {
        //print("Patrolling");
        
        //anim.SetInteger("animState", 1);

        agent.stoppingDistance = 0;
        agent.speed = 2.5f;

        if (Vector3.Distance(transform.position, nextDestination) < 1)
        {
            FindNextPoint();
        }
        else if (distanceToPlayer <= chaseDistance && IsPlayerInClearFOV())
        {
            currentState = FSMStates.Chase;
        }

        FaceTarget(nextDestination);

        agent.SetDestination(nextDestination);
    }
    
    void UpdateChaseState()
    {
        print("Chasing!");

        nextDestination = player.transform.position;
        
        //anim.SetInteger("animState", 2);
        
        agent.stoppingDistance = 0;
        agent.speed = 3;

        if (distanceToPlayer == 0)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            FindNextPoint();
            currentState = FSMStates.Patrol;
        }

        FaceTarget(nextDestination);

        agent.SetDestination(nextDestination);
    }
    
    void UpdateAttackState()
    {
        print("attack");

        nextDestination = player.transform.position;

        if (distanceToPlayer <= 0)
        {
            currentState = FSMStates.Back;
        }
        else if (distanceToPlayer > 0 && distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.Patrol;
        }
        
        FaceTarget(nextDestination);
        if (canHit)
        {
            canHit = false;
            counter = 0;
            var playerHealth = player.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageAmount);
        }
    }

    void UpdateBackState()
    {
        if (counter < hitDelay)
        {
            agent.speed = -3;
        }
        else
        {
            currentState = FSMStates.Patrol;
        }
    }

    void FindNextPoint()
    {
        nextDestination = wanderPoints[currentDestinationIndex].transform.position;

        currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;

        agent.SetDestination(nextDestination);
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp
            (transform.rotation, lookRotation, 2 * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        /*Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance); */

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);

        Vector3 frontRayPoint = enemyEyes.position + (enemyEyes.forward * chaseDistance);
        Vector3 leftRayPoint = Quaternion.Euler(0, fieldOfView * 0.5f, 0) * frontRayPoint;
        Vector3 rightRayPoint = Quaternion.Euler(0, -fieldOfView * 0.5f, 0) * frontRayPoint;
        
        Debug.DrawLine(enemyEyes.position, frontRayPoint, Color.cyan);
        Debug.DrawLine(enemyEyes.position, leftRayPoint, Color.yellow);
        Debug.DrawLine(enemyEyes.position, rightRayPoint, Color.yellow);
    }

    bool IsPlayerInClearFOV()
    {
        RaycastHit hit;
        
        Vector3 directionToPlayer = player.transform.position - enemyEyes.position;

        if (Vector3.Angle(directionToPlayer, enemyEyes.forward) <= fieldOfView)
        {
            if (Physics.Raycast(enemyEyes.position, directionToPlayer, out hit, chaseDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        return false;
    }
}
