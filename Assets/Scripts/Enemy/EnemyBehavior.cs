using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 10;
    public float minDistance = 2;
    public int damageAmount = 20;

    private float hitDelay = 0.5f;
    private float counter;
    // private bool canHit = true;
    private void Start()
    {
        counter = hitDelay;
    }

    // Update is called once per frame
    private void Update()
    {
        if (counter > hitDelay)
        {
            // variable not used right now
           // canHit = true;
        }

        counter += Time.deltaTime;
    }

    /*void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && canHit)
        {
            canHit = false;
            counter = 0;
            var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            other.gameObject.GetComponent<CharacterController>()
                .Move((transform.position - other.contacts[0].point).normalized * 2 * Time.deltaTime);
            playerHealth.TakeDamage(damageAmount);
            
            Vector3 nmePos = other.gameObject.GetComponent<Transform>().position;
            Vector3 forceVector = new Vector3(transform.position.x - nmePos.x, 
                transform.position.y - nmePos.y, transform.position.z - nmePos.z);
            GetComponent<Rigidbody>().AddForce(forceVector * 500, ForceMode.Force);
        }
    } */
}
