using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    //public AudioClip deadSFX;
    public Slider healthSlider;

    private int currentHealth;

    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
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
            print(currentHealth);
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            PlayerDies();
        }
    }

    public void AddHealth(int healthAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth += healthAmount;
        }

        if (currentHealth >= startingHealth)
        {
            currentHealth = startingHealth;
        }
        healthSlider.value = currentHealth;
    }
    
    public int GetHealth() { return currentHealth; }

    void PlayerDies()
    {
        if (!LevelManager.isGameOver)
        {
            FindObjectOfType<LevelManager>().LevelLost();
        }
        // AudioSource.PlayClipAtPoint(deadSFX, transform.position);
    }
}