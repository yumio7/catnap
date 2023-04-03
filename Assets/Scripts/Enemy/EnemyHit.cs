using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHit : MonoBehaviour
{
    [SerializeField, Tooltip("Should the player win the game when this enemy is beat?")]
    private bool isBoss;
    [SerializeField] private GameObject destroyedParticleEffect;
    [SerializeField] private int enemyHealth = 2;
    [SerializeField] private GameObject milkPrefab;
    
    private GameObject _powerupParent;
    private EnemyNav _enemyNav;
    private LevelManager _levelMan;

    private void Start()
    {
        _powerupParent = GameObject.FindGameObjectWithTag("PowerupParent");
        _enemyNav = gameObject.GetComponent<EnemyNav>();
        _levelMan = FindObjectOfType<LevelManager>();
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

    public void Slow(int duration)
    {
        SlowMoveSpeed();
        Invoke(nameof(RegularMoveSpeed), duration);
    }
    
    private void SlowMoveSpeed()
    {
        _enemyNav.SlowMovespeed();
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
