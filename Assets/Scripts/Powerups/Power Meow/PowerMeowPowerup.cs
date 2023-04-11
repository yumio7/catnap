using System.Collections;
using UnityEngine;

public class PowerMeowPowerup : MonoBehaviour, Powerup
{
    [SerializeField] private string myName;
    [SerializeField] private string description;
    [SerializeField] private int shopCost = 1;
    [SerializeField] private Sprite mySprite;
    [SerializeField] private int cooldown = 30;
    [SerializeField, Tooltip("How low must the player get before power meow activates?")]
    private int activationThreshold;
    [SerializeField] private int damageDealt;
    [SerializeField, Tooltip("Explosion radius of the power meow")]
    private float explosionRadius;
    [SerializeField, Tooltip("How long the player is invulnerable after power meow")]
    private float invulnerabilityDuration = 5f;
    [SerializeField] private AudioClip powerMeowSound;

    private GameObject _player;
    private PlayerHealth _playerHealth;
    private bool powerMeowOffCooldown;
    private int prevHealth;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerHealth = _player.GetComponent<PlayerHealth>();

        powerMeowOffCooldown = true;
        prevHealth = _playerHealth.GetHealth();
    }

    void Update()
    {
        if (powerMeowOffCooldown && 
            prevHealth != _playerHealth.GetHealth() &&
            _playerHealth.GetHealth() <= activationThreshold)
        {
            FirePowerMeow();
            powerMeowOffCooldown = false;
            StartCoroutine(PowerMeowCooldown());
        }
    
        prevHealth = _playerHealth.GetHealth();
    }

    void FirePowerMeow()
    {
        var playerPos = _player.transform.position;
    
        Collider[] hits = Physics.OverlapSphere(playerPos, explosionRadius);

        foreach (Collider hit in hits)
        {
            if (hit.gameObject.CompareTag("Enemy"))
            {
                hit.gameObject.GetComponent<EnemyHit>().EnemyHurt(damageDealt);
            }
        }
    
        AudioSource.PlayClipAtPoint(powerMeowSound, playerPos);
    
        // disable all player colliders
        foreach (Collider collider in _player.GetComponentsInChildren<Collider>())
        {
            collider.enabled = false;
        }
    
        // wait power meow invulnerability duration
        StartCoroutine(DisablePlayerColliders(invulnerabilityDuration));
    }

    private IEnumerator DisablePlayerColliders(float duration)
    {
        yield return new WaitForSeconds(duration);
    
        // re-enable colliders
        foreach (Collider collider in _player.GetComponentsInChildren<Collider>())
        {
            collider.enabled = true;
        }
    }

    private IEnumerator PowerMeowCooldown()
    {
        yield return new WaitForSeconds(cooldown);
    
        powerMeowOffCooldown = true;
    }
    
    public string GetName()
    {
        return myName;
    }

    public string GetDescription()
    {
        return description;
    }

    public Powerup.powerupType GetSlot()
    {
        return Powerup.powerupType.Augment;
    }

    public int GetCost()
    {
        return shopCost;
    }

    public Sprite GetSprite()
    {
        return mySprite;
    }
}