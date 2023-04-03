using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    public float speed = 5f;
    public float jumpHeight = 3;
    public float gravity = 9.81f;
    public float airControl = 10;
    public float swipeRate = 0.5f;
    public AudioClip swipeSFX;

    private GameObject _clawZone;
    private float elapsedTime = 0.0f;
    private GameObject paw;
    private bool paw_attack = false;
    private Vector3 paw_pos;

    private Vector3 input, moveDirection;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        _clawZone = GameObject.FindGameObjectWithTag("ClawZone");
        paw = GameObject.FindGameObjectWithTag("Paw");
        paw_pos = paw.transform.localPosition;
        paw_attack = false;
    }

    // Update is called once per frame
    private void Update()
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
            //Debug.Log(moveDirection);

        if (!LevelManager.isGameOver)
        {
            controller.Move(moveDirection * Time.deltaTime);
        }


        // SWIPE ATTACK
        if (Input.GetKeyDown(KeyCode.Mouse1) && elapsedTime > swipeRate && !LevelManager.isGameOver)
        {
            _swipeAttack();

            paw_attack = true;
            
            elapsedTime = 0.0f;
        }
        
        elapsedTime += Time.deltaTime;
        
        Vector3 xmove = new Vector3(-0.5f, paw.transform.localPosition.y, paw.transform.localPosition.z);
        if (paw_attack && elapsedTime < (swipeRate / 2))
        {
            paw.transform.localPosition = Vector3.Lerp(paw.transform.localPosition, xmove, 2 * Time.deltaTime);
        } 
        else if (paw_attack && elapsedTime < swipeRate)
        {
            paw.transform.localPosition = Vector3.Lerp(paw.transform.localPosition, paw_pos, 6 * Time.deltaTime);
        }
        else
        {
            paw_attack = false;
        }
    }


    private void _swipeAttack()
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

                if (eh == null)
                {
                    obj.GetComponent<BossHit>().EnemyHurt(1);
                }
                else
                {
                    eh.EnemyHurt(1); 
                }
            }
        }
    }
}