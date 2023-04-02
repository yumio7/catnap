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
    public float chaseSpeed = 3f;
    public float patrolSpeed = 2.5f;
    public GameObject player;

    private GameObject[] wanderPoints;
    private Vector3 nextDestination;
    private float distanceToPlayer;
    public int damageAmount = 20;

    private int currentDestinationIndex = 0;

    private NavMeshAgent agent;

    public Transform enemyEyes;
    public float fieldOfView = 150f;
    
    private float hitDelay = 2f;
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
        
        if (counter > hitDelay)
        {
            canHit = true;
        }

        counter += Time.deltaTime;
        if (!LevelManager.isGameOver)
        {
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
        } 
        else 
        { 
            agent.speed = 0;
        }

        elapsedTime += Time.deltaTime;
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
        agent.stoppingDistance = 0;
        agent.speed = patrolSpeed;

        if (Vector3.Distance(transform.position, nextDestination) < 1)
        {
            FindNextPoint();
        }
        else if (IsPlayerInClearFOV())
        {
            currentState = FSMStates.Chase;
        }

        FaceTarget(nextDestination);

        agent.SetDestination(nextDestination);
    }
    
    void UpdateChaseState()
    {
        nextDestination = player.transform.position;

        agent.stoppingDistance = 0.5f;
        agent.speed = chaseSpeed;

        if (distanceToPlayer > chaseDistance)
        {
            FindNextPoint();
            currentState = FSMStates.Patrol;
        }

        FaceTarget(nextDestination);

        agent.SetDestination(nextDestination);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !LevelManager.isGameOver)
        {
            currentState = FSMStates.Attack;
        }
    }

    void UpdateAttackState()
    {
        nextDestination = player.transform.position;
        
        if (canHit)
        {
            print("Hit");
            counter = 0;
            canHit = false;
            var playerHealth = player.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageAmount);
            currentState = FSMStates.Back;
        }

        FaceTarget(nextDestination);
    }

    void UpdateBackState()
    {
        GetComponent<Rigidbody>().AddForce(-transform.forward * 500, ForceMode.Force);
        currentState = FSMStates.Chase;
    }

    void FindNextPoint()
    {
        nextDestination = wanderPoints[Random.Range(0, wanderPoints.Length)].transform.position;

       // currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;

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
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);

        Vector3 frontRayPoint = enemyEyes.position + (enemyEyes.forward * chaseDistance);
        Vector3 leftRayPoint = Quaternion.Euler(0, fieldOfView * 0.5f, 0) * frontRayPoint;
        Vector3 rightRayPoint = Quaternion.Euler(0, -fieldOfView * 0.5f, 0) * frontRayPoint;
        
        Debug.DrawLine(enemyEyes.position, frontRayPoint, Color.cyan);
        Debug.DrawLine(enemyEyes.position, leftRayPoint, Color.yellow);
        Debug.DrawLine(enemyEyes.position, rightRayPoint, Color.yellow); */
    }

    bool IsPlayerInClearFOV()
    {
        RaycastHit hit;
        
        Vector3 directionToPlayer = player.transform.position - enemyEyes.position;

        if (Vector3.Angle(directionToPlayer, enemyEyes.forward) <= fieldOfView)
        {
            if (Physics.Raycast(enemyEyes.position, directionToPlayer, out hit, chaseDistance))
            {
                if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("ClawZone"))
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
