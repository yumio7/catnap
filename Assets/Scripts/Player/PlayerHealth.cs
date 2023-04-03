using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    //public AudioClip deadSFX;
    public Slider healthSlider;

    private int currentHealth;

    private void Start()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            if (currentHealth > 25 && currentHealth - damageAmount <= 25)
            {
                PlayerPowerups.jump = true;
            }
            currentHealth -= damageAmount;
            print(currentHealth);
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            healthSlider.value = currentHealth;
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

    private void PlayerDies()
    {
        if (!LevelManager.isGameOver)
        {
            FindObjectOfType<LevelManager>().LevelLost();
        }
        // AudioSource.PlayClipAtPoint(deadSFX, transform.position);
    }
}