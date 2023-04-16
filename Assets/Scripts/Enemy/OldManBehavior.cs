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
    [SerializeField] private int shootForce = 10;
    
    private float cooldown;
    private EnemyAI.FSMStates currentState;
    private GameObject player;
    private NavMeshAgent agent;
    private GameObject projectileParent;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        cooldown = cooldownDuration;
        agent.speed = moveSpeed;
        currentState = EnemyAI.FSMStates.Chase;
        projectileParent = GameObject.FindGameObjectWithTag("ProjectileParent");
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.isGameOver)
        {
            switch (currentState)
            {
                case EnemyAI.FSMStates.Chase:
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
        Debug.Log("Move State");
        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            currentState = EnemyAI.FSMStates.Attack;
        }
        else
        {
            FaceTarget(player.transform.position);
            agent.SetDestination(player.transform.position);
            Debug.Log(agent.destination);
        }
    }

    void AttackState()
    {
        Debug.Log("Attack State");
        if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            Debug.Log("In Range");
            agent.speed = 0;
            FaceTarget(player.transform.position);
            if (cooldown <= 0)
            {
                Debug.Log("Cooldown is less than zero!");
                FireBullet();
                cooldown = cooldownDuration;
            }
            cooldown -= Time.deltaTime;    
        }
        else
        {
            agent.speed = moveSpeed;
            currentState = EnemyAI.FSMStates.Chase;
        }
        
    }

    private void FireBullet()
    {
        Debug.Log("Fired Bullet");
        var b =Instantiate(bullet, gunTip.transform.position, Quaternion.identity);
        b.transform.parent = projectileParent.transform;
        var rb = b.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * shootForce);

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
