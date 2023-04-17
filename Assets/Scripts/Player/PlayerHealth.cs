using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;

    public AudioClip hurtSFX;

    private int currentHealth;

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        AudioSource.PlayClipAtPoint(hurtSFX, transform.position);
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
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
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    private void PlayerDies()
    {
        if (!LevelManager.isGameOver)
        {
            FindObjectOfType<LevelManager>().LevelLost();
        }
    }
}