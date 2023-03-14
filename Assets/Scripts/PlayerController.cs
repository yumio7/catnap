using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;

    public float speed = 5f;
    public float jumpHeight = 3;
    public float gravity = 9.81f;
    public float airControl = 10;
    public float swipeRate = 0.5f;
    public AudioClip swipeSFX;
    private GameObject _clawZone;
    private PlayerPowerups _powerups;
    private float elapsedTime = 0.0f;

    Vector3 input, moveDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        _clawZone = GameObject.FindGameObjectWithTag("ClawZone");
        _powerups = gameObject.GetComponent<PlayerPowerups>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        input = (transform.right * moveHorizontal + transform.up * moveVertical).normalized;

        input *= speed;

        if (controller.isGrounded)
        {
            moveDirection = input;

            //we can jump
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);
            }
            else
            {
                moveDirection.y = 0.0f;
            }
        }
        else
        {
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
        }

        moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);


        // SWIPE ATTACK
        if (Input.GetKeyDown(KeyCode.Mouse1) && elapsedTime > swipeRate)
        {
            _swipeAttack();
            
            elapsedTime = 0.0f;
        }
        
        if (Input.GetKeyDown(KeyCode.Q) && _powerups.GetYarnGrenadeLevel() > 0)
        {
            _yarnGrenade();
        }
        
        elapsedTime += Time.deltaTime;
    }


    void _swipeAttack()
    {
        // Access list of objects in zone
        List<GameObject> objList = _clawZone.GetComponent<ListOfObjectsInTrigger>().enemies;
        Vector3 pos = this.transform.position;
        
        AudioSource.PlayClipAtPoint(swipeSFX, transform.position);

        foreach (GameObject obj in objList)
        {
            if (obj != null)
            {
                Vector3 nmePos = obj.GetComponent<Transform>().position;
                Vector3 forceVector = new Vector3(nmePos.x - pos.x, nmePos.y - pos.y, nmePos.z - pos.z);
                obj.GetComponent<Rigidbody>().AddForce(forceVector * 500, ForceMode.Force);
                EnemyHit eh = obj.GetComponent<EnemyHit>();

                eh.EnemyHurt(1);
            }
        }
    }

    void _yarnGrenade()
    {
        _powerups.FireYarnGrenade();
    }
}