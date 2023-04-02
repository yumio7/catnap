using UnityEngine;

public class MilkPickupBehavior : MonoBehaviour
{
    public int healthGainBack = 10;
    
    public AudioClip drinkingSFX;

    private int touched = 0;

    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && touched == 0)
        {
            touched++;
            var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            playerHealth.AddHealth(healthGainBack);
            
            AudioSource.PlayClipAtPoint(drinkingSFX, transform.position);
            
            Destroy(gameObject);
        }
        else
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), other);
        }
    }
}
