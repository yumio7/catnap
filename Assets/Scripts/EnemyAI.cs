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
    }

    public FSMStates currentState;
    
    public float attackDistance = 5;
    public float chaseDistance = 10;
    public float enemySpeed = 5;
    public GameObject player;
    //public GameObject[] spellProjectiles;
    //public GameObject wandTip;
    //public float shootRate = 2.0f;
    //public GameObject deadVFX;

    private GameObject[] wanderPoints;
    private Vector3 nextDestination;
    //private Animator anim;
    private float distanceToPlayer;

    private int currentDestinationIndex = 0;
    
    //private EnemyHealth enemyHealth;
    //private int health;
    //private Transform deadTransform;
    //private bool isDead;

    private NavMeshAgent agent;

    public Transform enemyEyes;
    public float fieldOfView = 150f;
    
    
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
        
        //health = enemyHealth.currentHealth;
        
        switch (currentState)
        {
            case FSMStates.Patrol:
                UpdatePatrolState();
                break;
            case FSMStates.Chase:
                UpdateChaseState();
                break;
            /*case FSMStates.Attack:
                UpdateAttackState();
                break;
            case FSMStates.Dead:
                UpdateDeadState();
                break; */
        }

        elapsedTime += Time.deltaTime;

        /*if (health <= 0)
        {
            currentState = FSMStates.Dead;
        }*/
    }

    void Initialize()
    {
        currentState = FSMStates.Patrol;
        
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");

        //anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");

        //wandTip = GameObject.FindGameObjectWithTag("WandTip");

        agent = GetComponent<NavMeshAgent>();

        //enemyHealth = GetComponent<EnemyHealth>();

        //health = enemyHealth.currentHealth;

        //isDead = false;
        
        FindNextPoint();
    }
    
    void UpdatePatrolState()
    {
        //print("Patrolling");
        
        //anim.SetInteger("animState", 1);

        agent.stoppingDistance = 0;
        agent.speed = 3.5f;

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
        agent.speed = 5;

        if (distanceToPlayer <= attackDistance)
        {
            //currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            FindNextPoint();
            currentState = FSMStates.Patrol;
        }

        FaceTarget(nextDestination);

        agent.SetDestination(nextDestination);
    }
    
    /*void UpdateAttackState()
    {
        print("attack");

        nextDestination = player.transform.position;
        
        //agent.stoppingDistance = attackDistance;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > attackDistance && distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.Patrol;
        }
        
        FaceTarget(nextDestination);
        
        anim.SetInteger("animState", 3);
        
        EnemySpellCast();
    } */
    
    /*void UpdateDeadState()
    {
        anim.SetInteger("animState", 4);
        deadTransform = gameObject.transform;
        isDead = true;
        
        Destroy(gameObject, 3);
    } */

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
            (transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    /*void EnemySpellCast()
    {
        if (!isDead)
        {
            if (elapsedTime >= shootRate)
            {
                var animDuration = anim.GetCurrentAnimatorStateInfo(0).length;
                Invoke("SpellCasting", animDuration);
                elapsedTime = 0.0f;
            } 
        }
    } */

    /*void SpellCasting()
    {
        int randProjectileIndex = Random.Range(0, spellProjectiles.Length);
        
        GameObject spellProjectile = spellProjectiles[randProjectileIndex];

        Instantiate(spellProjectile, wandTip.transform.position, wandTip.transform.rotation);
    } */

    /*private void OnDestroy()
    {
        Instantiate(deadVFX, deadTransform.position, deadTransform.rotation);
    } */

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

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
