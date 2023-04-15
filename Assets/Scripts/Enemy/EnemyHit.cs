using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHit : MonoBehaviour
{
    [SerializeField, Tooltip("Should the player win the game when this enemy is beat?")]
    private bool isBoss;

    [SerializeField] private GameObject destroyedParticleEffect;
    [SerializeField] private int enemyHealth = 2;
    [SerializeField] private GameObject milkPrefab;
    [SerializeField] private GameObject slowIndicator;
    [SerializeField, Tooltip("What sound does this enemy make when it's hit?")]
    private AudioClip hitSFX;
    [SerializeField, Tooltip("When enemy is hit, how long do they flash red?")] 
    private float hitIndicatorDuration = 0.1f;

    private GameObject _powerupParent;
    private EnemyNav _enemyNav;
    private LevelManager _levelMan;
    private int maxHealth;
    private Renderer[] renderersInEnemy;
    private bool immune;

    private void Start()
    {
        maxHealth = enemyHealth;
        _powerupParent = GameObject.FindGameObjectWithTag("PowerupParent");
        _enemyNav = gameObject.GetComponent<EnemyNav>();
        _levelMan = FindObjectOfType<LevelManager>();
        renderersInEnemy = gameObject.GetComponentsInChildren<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var coll = collision.gameObject;

        if (coll.CompareTag("Projectile"))
        {
            EnemyHurt(coll.GetComponent<ProjectileStats>().GetDamageDealt());
            Destroy(coll);
        }
    }

    public void SetImmune(bool immunity)
    {
        immune = immunity;
    }

    public void EnemyHurt(int damage)
    {
        if (!immune)
        {
            StartCoroutine(HitRegister());
            enemyHealth -= damage;

            if (enemyHealth <= 0)
            {
                DestroyEnemy();
            }
        }
    }

    void OnDisable()
    {
        StopCoroutine(HitRegister());
    }
    
    // make a visual indicator the enemy got hit with a red effect and play SFX
    IEnumerator HitRegister()
    {
        if (renderersInEnemy.Length <= 0)
        {
            renderersInEnemy = gameObject.GetComponentsInChildren<Renderer>();
        }

        var defaultColors = new Color[renderersInEnemy.Length];

        for (int i = 0 ; i < renderersInEnemy.Length; i++)
        {
            defaultColors[i] = renderersInEnemy[i].material.color;
            renderersInEnemy[i].material.color = Color.red;
        }

        AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position, .2f); 

        yield return new WaitForSeconds(hitIndicatorDuration); // wait for duration of hit indicator
        
        for (int i = 0 ; i < renderersInEnemy.Length ; i++)
        {
            renderersInEnemy[i].material.color = defaultColors[i]; // reset the color
        }

        StopCoroutine(HitRegister()); // stop the coroutine
    }


    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentHealth()
    {
        return enemyHealth;
    }

    public void Slow(int duration)
    {
        SlowMoveSpeed(duration);
        Invoke(nameof(RegularMoveSpeed), duration);
    }

    private void SlowMoveSpeed(int duration)
    {
        var slowIndctr = Instantiate(slowIndicator,
            transform.position,
            Quaternion.identity);
        slowIndctr.transform.parent = gameObject.transform;
        _enemyNav.SlowMovespeed();
        Destroy(slowIndctr, duration);
    }

    private void RegularMoveSpeed()
    {
        _enemyNav.RegularMovespeed();
    }

    private void DestroyEnemy()
    {
        Instantiate(destroyedParticleEffect, transform.position, transform.rotation);
        EnemyManager.Instance.enemyCount -= 1;
        EnemyManager.Instance.enemiesKilled += 1;
        gameObject.SetActive(false);

        if (Random.Range(0f, 10f) <= 5f)
        {
            GameObject projectile = Instantiate(milkPrefab,
                transform.position + transform.forward, transform.rotation);

            projectile.transform.SetParent(
                _powerupParent.transform);
        }

        _levelMan.SetScoreText();
        if (isBoss)
        {
            _levelMan.LevelBeat();
        }

        Destroy(gameObject, 0.5f);
    }
}