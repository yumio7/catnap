using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public GameObject destroyedParticleEffect;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
          if (other.CompareTag("Projectile"))
        {
            DestroyEnemy();
        } 
         
       
    }

    void DestroyEnemy()
    {
        
         
        Instantiate(destroyedParticleEffect, transform.position, transform.rotation);
        gameObject.SetActive(false);
        Destroy(gameObject, 0.5f);
        
        
    }
}
