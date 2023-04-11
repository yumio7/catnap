using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private enum FSMStates
    {
        Idle,
        Interact
    }

    [SerializeField] private int playerVisibleDistance = 5;
    
    public GameObject dialoguebox;
    
    private int talkCounter;
    public TextMeshProUGUI dialogueText;
    
    private FSMStates currentState;
    private Transform player;
    private float distanceToPlayer;
    private GameObject[] wanderPoints;
    private Vector3 nextDestination;
    private int currentDestinationIndex = 0;
    private Animator anim;
    private string[] dialogue;
    private bool canTalk;
    public float coolDown = 2f;
    private float timer = 0f;
    
    

    // Start is called before the first frame update
    private void Start()
    {
        currentState = FSMStates.Idle;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        anim = this.GetComponent<Animator>();
        FindNextPoint();
        canTalk = true;

        talkCounter = 0;
        string[] d =
        {
            "Oh hey little cat! You look so tired! Having trouble moving, " +
            "try pressing the arrow keys or WASD.",
            "Wow! You did such a great job! Now try swipping, by using right click!",
            "Who's a good little cat! OH NO! There's a rat. Quick fire a hairball by left clicking or" +
            "swipe at them.",
            "I think you need a nap! Word of advice though, remember drinking milk can only help!"
        };
        dialogue = d;
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

        timer += Time.deltaTime;

        if (timer > coolDown)
        {
            canTalk = true;
        }
    }

    private void UpdateIdleState()
    {
        anim.SetInteger("animState", 1);

        if (Vector3.Distance(transform.position, nextDestination) <= 0.5f)
        {
            FindNextPoint();
        }

        FaceTarget(nextDestination);
        
        if (distanceToPlayer <= playerVisibleDistance && canTalk)
        {
            currentState = FSMStates.Interact; 
            dialoguebox.SetActive(true);
            dialogueText.text = dialogue[talkCounter];
            talkCounter++;
        }
    }

    private void UpdateInteractState()
    {
        player.GetComponent<PlayerController>().enabled = false;
        anim.SetInteger("animState", 2);
        var direction = (player.position - transform.position).normalized;
        direction.y = 0f;

        transform.rotation = Quaternion.LookRotation(direction);

        if (distanceToPlayer > playerVisibleDistance || Input.GetKey(KeyCode.E))
        {
            dialoguebox.SetActive(false);
            canTalk = false;
            timer = 0;
            player.GetComponent<PlayerController>().enabled = true;
            currentState = FSMStates.Idle;
        }
    }

    private void FindNextPoint()
    {
        nextDestination = wanderPoints[currentDestinationIndex].transform.position;

        currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;
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
