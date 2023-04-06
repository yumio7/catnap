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
    
    private GameObject _powerupParent;
    private EnemyNav _enemyNav;
    private LevelManager _levelMan;
    private int maxHealth;

    private void Start()
    {
        maxHealth = enemyHealth;
        _powerupParent = GameObject.FindGameObjectWithTag("PowerupParent");
        _enemyNav = gameObject.GetComponent<EnemyNav>();
        _levelMan = FindObjectOfType<LevelManager>();
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

    public void EnemyHurt(int damage)
    {
        enemyHealth -= damage;
            
        if (enemyHealth <= 0)
        {
            DestroyEnemy();
        }
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
