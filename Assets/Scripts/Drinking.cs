using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drinking : MonoBehaviour
{
    public int healthGainBack = 10;

    private int touched = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && touched == 0)
        {
            touched++;
            var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            playerHealth.AddHealth(healthGainBack);
            
            Destroy(gameObject);
        }
    }
}
