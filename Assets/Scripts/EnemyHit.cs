using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public GameObject destroyedParticleEffect;

    public int enemyHealth = 2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
            EnemyHurt(1);
        } 
    }

    public void EnemyHurt(int damage)
    {
        enemyHealth -= damage;
            
        if (enemyHealth <= 0)
        {
            DestroyEnemy();
        }
    }

    void DestroyEnemy()
    {
        Instantiate(destroyedParticleEffect, transform.position, transform.rotation);
        EnemyManager.Instance.enemyCount -= 1;
        EnemyManager.Instance.enemiesKilled += 1;
        gameObject.SetActive(false);
        FindObjectOfType<LevelManager>().SetScoreText();
        Destroy(gameObject, 0.5f);
        
        
    }
}
