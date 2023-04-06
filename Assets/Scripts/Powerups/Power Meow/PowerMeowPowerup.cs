using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PowerMeowPowerup : MonoBehaviour
{
    [SerializeField] private int cooldown;
    [SerializeField, Tooltip("How low must the player get before power meow activates?")]
    private int activationThreshold;
    [SerializeField] private int damageDealt;
    [SerializeField, Tooltip("Explosion radius of the power meow")]
    private float explosionRadius;
    [SerializeField, Tooltip("How high does the power meow launch the player?")]
    private float launchHeight;
    [SerializeField] private AudioClip powerMeowSound;

    private GameObject _player;
    private PlayerHealth _playerHealth;
    private bool powerMeowOffCooldown;
    private CharacterController _characterController;
    private int prevHealth;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerHealth = _player.GetComponent<PlayerHealth>();
        _characterController = _player.GetComponent<CharacterController>();

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
            Invoke(nameof(PowerMeowAvailable), cooldown);
        }
        
        prevHealth = _playerHealth.GetHealth();
    }

    void FirePowerMeow()
    {
        var playerPos = _player.transform.position;
        
        // launch charCont into the air
        Collider[] hits = Physics.OverlapSphere(playerPos, explosionRadius);

        foreach (Collider hit in hits)
        {
            if (hit.gameObject.CompareTag("Enemy"))
            {
                hit.gameObject.GetComponent<EnemyHit>().EnemyHurt(damageDealt);
            }
        }
        
        _characterController.Move(new Vector3(0, launchHeight, 0));
        AudioSource.PlayClipAtPoint(powerMeowSound, playerPos);
    }

    void PowerMeowAvailable()
    {
        powerMeowOffCooldown = true;
    }
}