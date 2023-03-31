using System.Collections;
using System.Collections.Generic;
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
    
    // Start is called before the first frame update
    void Start()
    {
        currentState = FSMStates.Idle;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
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

    void UpdateIdleState()
    {
        // TODO implement wanderpoints/walking around/idle animation
        
        // spin in circles
        transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * 360);

        if (distanceToPlayer <= playerVisibleDistance)
        {
            currentState = FSMStates.Interact;
        }
    }

    void UpdateInteractState()
    {
        // rotate toward player
        var direction = (player.position - transform.position).normalized;
        direction.y = 0f; // set the y component of direction to 0 to keep the NPC flat

        transform.rotation = Quaternion.LookRotation(direction);

        if (distanceToPlayer > playerVisibleDistance)
        {
            currentState = FSMStates.Idle;
        }
    }

}
