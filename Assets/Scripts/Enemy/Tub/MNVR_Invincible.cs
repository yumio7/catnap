using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MNVR_Invincible : MonoBehaviour, Maneuver
{
    [SerializeField] private float invincibilityDuration;
    [SerializeField] private Color invincibilityColor;

    private Renderer[] renderersInEnemy;
    
    public void Activate()
    {
        renderersInEnemy = gameObject.GetComponentsInChildren<Renderer>();
        
        // disable EnemyHit component for the duration of the invincibility
        StartCoroutine(InvincibilityCoroutine());
        StartCoroutine(InvincibilityColor());
    }
    
    private IEnumerator InvincibilityCoroutine()
    {
        // disable EnemyHit component
        GetComponent<EnemyHit>().SetImmune(true);

        // wait for the specified duration
        yield return new WaitForSeconds(invincibilityDuration);

        // re-enable EnemyHit component
        GetComponent<EnemyHit>().SetImmune(false);

        // stop the coroutine
        StopCoroutine(InvincibilityCoroutine());
    }
    
    // make a visual indicator the enemy is invincible
    IEnumerator InvincibilityColor()
    {
        if (renderersInEnemy.Length <= 0)
        {
            renderersInEnemy = gameObject.GetComponentsInChildren<Renderer>();
        }

        var defaultColors = new Color[renderersInEnemy.Length];

        for (int i = 0 ; i < renderersInEnemy.Length; i++)
        {
            defaultColors[i] = renderersInEnemy[i].material.color;
            renderersInEnemy[i].material.color = invincibilityColor;
        }

        yield return new WaitForSeconds(invincibilityDuration);
        
        for (int i = 0 ; i < renderersInEnemy.Length ; i++)
        {
            renderersInEnemy[i].material.color = defaultColors[i]; // reset the color
        }

        StopCoroutine(InvincibilityColor()); // stop the coroutine
    }

}
