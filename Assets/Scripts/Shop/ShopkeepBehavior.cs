using UnityEngine;
using TMPro;

public class ShopkeepBehavior : MonoBehaviour
{
    private enum FSMStates
    {
        Idle,
        Interact
    }

    [SerializeField] private int playerVisibleDistance = 5;
    
    [SerializeField] private string[] dialogue;

    public static bool hasTalked;
    public GameObject dialoguebox;
    
    public TextMeshProUGUI dialogueText;

    private string message;
    private int counter;
    private FSMStates currentState;
    private Transform player;
    private float distanceToPlayer;
    private GameObject[] wanderPoints;
    private Vector3 nextDestination;
    private int currentDestinationIndex = 0;
    private Animator anim;
    public Transform enemyEyes;
    public float fieldOfView = 150f;

    // Start is called before the first frame update
    private void Start()
    {
        currentState = FSMStates.Idle;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        anim = this.GetComponent<Animator>();
        counter = 0;
        hasTalked = false;
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

        if (Vector3.Distance(transform.position, nextDestination) <= 0.5f)
        {
            FindNextPoint();
        }

        FaceTarget(nextDestination);
        
        if (IsPlayerInClearFOV() && counter < dialogue.Length)
        {
            dialoguebox.SetActive(true);
            currentState = FSMStates.Interact;
            player.GetComponent<PlayerController>().enabled = false;
            Camera.main.GetComponent<MouseLook>().enabled = false;
        }
        else if (IsPlayerInClearFOV())
        {
            anim.SetInteger("animState", 2);
        }
        
    }

    private void UpdateInteractState()
    {
        anim.SetInteger("animState", 2);
        // rotate toward player
        var direction = (player.position - transform.position).normalized;
        direction.y = 0f; // set the y component of direction to 0 to keep the NPC flat
        
        dialogueText.text = dialogue[counter];

        transform.rotation = Quaternion.LookRotation(direction);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (counter == dialogue.Length - 1)
            {
                counter++;
                player.GetComponent<PlayerController>().enabled = true;
                Camera.main.GetComponent<MouseLook>().enabled = true;
                dialoguebox.SetActive(false);
                hasTalked = true;
                currentState = FSMStates.Idle;
            }
            else
            {
                counter++;
            }
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
    
    private bool IsPlayerInClearFOV()
    {
        RaycastHit hit;

        Vector3 directionToPlayer = player.transform.position - enemyEyes.position;

        if (Vector3.Angle(directionToPlayer, enemyEyes.forward) <= fieldOfView)
        {
            if (Physics.Raycast(enemyEyes.position, directionToPlayer, out hit, playerVisibleDistance))
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
