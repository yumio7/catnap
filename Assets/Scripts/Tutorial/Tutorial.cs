using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    private enum FSMStates
    {
        Idle,
        Interact
    }

    [SerializeField] private int playerVisibleDistance = 5;

    public GameObject dialoguebox;
    public GameObject rat;

    private int talkCounter;
    public TextMeshProUGUI dialogueText;

    public Transform enemyEyes;
    public float fieldOfView = 150f;

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
    private NavMeshAgent navMeshAgent;

    private TutorialUIBehavior tutorialUIComponent;
    private bool readyToAdvance;


    // Start is called before the first frame update
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentState = FSMStates.Idle;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        anim = this.GetComponent<Animator>();
        FindNextPoint();
        canTalk = true;

        talkCounter = 0;
        string[] d =
        {
            "Oh hey little cat! You look so tired! Having trouble moving? " +
            "Try using WASD.",
            "You can also jump with space bar.",
            "Wow! You did such a great job! Now try swiping, by using right click!",
            "Now look around. You have a health bar, a score counter and a coin counter. If you" +
            " press ESC, the game will pause.",
            "Who's a good little cat! OH NO! There's a rat. Quick fire a hairball by left clicking or" +
            " swipe at them.",
            "I think you need a nap! Word of advice though, remember drinking milk can only help!"
        };
        dialogue = d;

        tutorialUIComponent = GameObject.FindGameObjectWithTag("TutorialUI").GetComponent<TutorialUIBehavior>();
        readyToAdvance = true;
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

        switch (talkCounter)
        {
            case 0:
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) ||
                    Input.GetKeyDown(KeyCode.D))
                    Success();
                break;
            case 2:
                if(Input.GetKeyDown(KeyCode.Space))
                    Success();
                break;
            case 3:
                if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2"))
                    Success();
                break;
            case 4:
                if (Input.GetKeyDown(KeyCode.Escape))
                    Success();
                break;
            case 5:
                if (LevelManager.totalKills == 1)
                    Success();
                break;
            case 6:
                tutorialUIComponent.SetTextColor(Color.green);
                break;
        }

        timer += Time.deltaTime;

        if (timer > coolDown)
        {
            canTalk = true;
        }
    }

    private void Success()
    {
        tutorialUIComponent.SetTextColor(Color.green);
        readyToAdvance = true;
    }

    private void UpdateIdleState()
    {
        navMeshAgent.speed = 0.5f;

        anim.SetInteger("animState", 1);

        if (Vector3.Distance(transform.position, nextDestination) <= 2f)
        {
            FindNextPoint();
        }

        FaceTarget(nextDestination);

        if (IsPlayerInClearFOV() && canTalk && readyToAdvance)
        {
            switch (talkCounter)
            {
                case 0:
                    AdvanceTalkingState("Move with WASD");
                    break;
                case 1:
                    AdvanceTalkingState("Jump with Space");
                    break;
                case 2:
                    AdvanceTalkingState("Swipe with right click or shoot a hairball with left click");
                    break;
                case 3:
                    AdvanceTalkingState("Press ESC to pause");
                    break;
                case 4:
                    AdvanceTalkingState("Defeat the enemy rat");
                    break;
                case 5:
                    AdvanceTalkingState("Great job!");
                    break;
            }
        }
    }

    private void AdvanceTalkingState(string inStr)
    {
        navMeshAgent.speed = 0;
        currentState = FSMStates.Interact;
        dialoguebox.SetActive(true);
        dialogueText.text = dialogue[talkCounter];
        talkCounter++;
        tutorialUIComponent.SetTextColor(Color.red);
        tutorialUIComponent.SetText(inStr);
        readyToAdvance = false;
    }

    private void UpdateInteractState()
    {
        navMeshAgent.speed = 0;
        player.GetComponent<PlayerController>().enabled = false;
        anim.SetInteger("animState", 2);
        var direction = (player.position - transform.position).normalized;
        direction.y = 0f;

        transform.rotation = Quaternion.LookRotation(direction);

        if (Input.GetKey(KeyCode.E))
        {
            if (talkCounter == 5)
            {
                Instantiate(rat, new Vector3(0, 210, 1), player.rotation);
            }

            if (talkCounter == 6)
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                foreach (var p in players)
                {
                    Destroy(p);
                }

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

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

        navMeshAgent.SetDestination(nextDestination);
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