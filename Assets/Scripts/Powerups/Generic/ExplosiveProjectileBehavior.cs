using UnityEngine;

public class ExplosiveProjectileBehavior : MonoBehaviour
{
    
    [SerializeField] private GameObject explosionVFXPrefab;
    [Tooltip("Explosion sound to be played")] [SerializeField]
    private AudioClip explosiveSFX;
    [Tooltip("Damage of the grenade")] [SerializeField]
    private int damageAmount = 1;
    [Tooltip("Should this projectile slow enemies it hits?")] [SerializeField]
    private bool slowingProjectile;
    [Tooltip("Slow duration of the explosive (if enabled)")] [SerializeField]
    private int slowDuration = 1;
    [Tooltip("Radius of the explosion")] [SerializeField]
    private int explosionRadius;

    private void OnCollisionEnter()
    {
        GrenadeExplode();
    }

    private void GrenadeExplode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        GameObject explosionSphere =
            Instantiate(explosionVFXPrefab, transform.position, Quaternion.identity);
        explosionSphere.transform.localScale = new Vector3(explosionRadius, explosionRadius, explosionRadius);

        AudioSource.PlayClipAtPoint(explosiveSFX, transform.position);

        foreach (Collider hit in hits)
        {
            if (hit.gameObject.CompareTag("Enemy"))
            {
                hit.gameObject.GetComponent<EnemyHit>().EnemyHurt(damageAmount);
                if (slowingProjectile)
                    hit.gameObject.GetComponent<EnemyHit>().Slow(slowDuration);
            }
        }

        Destroy(explosionSphere, 0.25f);
        Destroy(gameObject);
    }
}