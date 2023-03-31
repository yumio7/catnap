using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YarnGrenadeProjectileBehavior : MonoBehaviour
{
    [SerializeField] private GameObject yarnExplosionEffectPrefab;
    
    [Tooltip("Yarn bomb explosion")] [SerializeField]
    private AudioClip yarnSFX;

    [Tooltip("Base damage of the grenade")] [SerializeField]
    private int baseDamage = 1;

    [Tooltip("Base slow duration of the grenade")] [SerializeField]
    private int baseDuration = 1;

    [Tooltip("Radius of the explosion")] [SerializeField]
    private int yarnGrenadeRadius;

    [Tooltip("Damage multiplied by how much per level")] [SerializeField]
    private int perLevelDamageModifier = 2;

    [Tooltip("Slow duration multiplied by how much per level")] [SerializeField]
    private int perLevelSlowModifier = 2;

    private int currentLevel;
    private int damageAmount;
    private int slowAmount;
    private PlayerPowerups _powerups;

    // Start is called before the first frame update
    void Start()
    {
        _powerups = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPowerups>();
        currentLevel = _powerups.GetYarnGrenadeLevel();
        damageAmount = baseDamage * (perLevelDamageModifier * currentLevel);
        slowAmount = baseDuration * (perLevelSlowModifier * currentLevel);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        GrenadeExplode();
    }

    private void GrenadeExplode()
    {

        Collider[] hits = Physics.OverlapSphere(transform.position, yarnGrenadeRadius);
        GameObject explosionSphere = Instantiate(yarnExplosionEffectPrefab, transform.position, Quaternion.identity);
        explosionSphere.transform.localScale = new Vector3(yarnGrenadeRadius, yarnGrenadeRadius, yarnGrenadeRadius);

        AudioSource.PlayClipAtPoint(yarnSFX, transform.position);
        
        foreach (Collider hit in hits)
        {
            if (hit.gameObject.CompareTag("Enemy"))
            {
                if (hit.gameObject.GetComponent<EnemyHit>() == null)
                {
                    hit.gameObject.GetComponent<BossHit>().EnemyHurt(damageAmount);
                    hit.gameObject.GetComponent<BossHit>().Slow(slowAmount);
                }
                else
                {
                    hit.gameObject.GetComponent<EnemyHit>().EnemyHurt(damageAmount);
                    hit.gameObject.GetComponent<EnemyHit>().Slow(slowAmount);
                }
            }
        }

        Destroy(explosionSphere, 0.25f);
        Destroy(gameObject);
    }
}