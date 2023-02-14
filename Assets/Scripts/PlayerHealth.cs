using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    //public AudioClip deadSFX;
    //public Slider healthSlider;
    
    private int currentHealth;
    
    void Start()
    {
        currentHealth = startingHealth;
        //healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
            //healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            PlayerDies();
        }
        
        Debug.Log(currentHealth);
    }

    void PlayerDies()
    {
        // AudioSource.PlayClipAtPoint(deadSFX, transform.position);
    }
}
