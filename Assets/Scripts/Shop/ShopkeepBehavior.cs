using UnityEngine;

public class ShopkeepBehavior : MonoBehaviour
{
    private enum FSMStates
    {
        Idle,
        Interact
    }

    [SerializeField] private int playerVisibleDistance = 5;
    
    private FSMStates currentState;
    private Transform player;
    private float distanceToPlayer;
    private GameObject[] wanderPoints;
    private Vector3 nextDestination;
    private int currentDestinationIndex = 0;
    private Animator anim;

    // Start is called before the first frame update
    private void Start()
    {
        currentState = FSMStates.Idle;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        anim = this.GetComponent<Animator>();
        FindNextPoint();
    }

    // Update is called once per frame
    private void Update()
    {

        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        switch (currentState)
        {
            case FSMStates.Idle:
                UpdateIdleState();
                break;
            case FSMStates.Interact:
                UpdateInteractState();
                break;
        }
    }

    private void UpdateIdleState()
    {
        // TODO implement wanderpoints/walking around/idle animation

        anim.SetInteger("animState", 1);
        
        //Debug.Log(nextDestination);

        if (Vector3.Distance(transform.position, nextDestination) <= 0.5f)
        {
            FindNextPoint();
        }

        //transform.position = Vector3.MoveTowards(transform.position, nextDestination, Time.deltaTime);
        
        FaceTarget(nextDestination);
        
        if (distanceToPlayer <= playerVisibleDistance)
        {
            currentState = FSMStates.Interact;
        }

        // spin in circles
        /*transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * 360);

        if (distanceToPlayer <= playerVisibleDistance)
        {
            currentState = FSMStates.Interact;
        } */
    }

    private void UpdateInteractState()
    {
        anim.SetInteger("animState", 2);
        // rotate toward player
        var direction = (player.position - transform.position).normalized;
        direction.y = 0f; // set the y component of direction to 0 to keep the NPC flat

        transform.rotation = Quaternion.LookRotation(direction);

        if (distanceToPlayer > playerVisibleDistance)
        {
            currentState = FSMStates.Idle;
        }
    }

    private void FindNextPoint()
    {
        nextDestination = wanderPoints[currentDestinationIndex].transform.position;

        currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;

        //agent.SetDestination(nextDestination);
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
