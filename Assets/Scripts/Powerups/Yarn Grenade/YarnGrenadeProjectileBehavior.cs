using UnityEngine;

public class YarnGrenadeProjectileBehavior : MonoBehaviour
{
    [SerializeField] private GameObject yarnExplosionEffectPrefab;

    [Tooltip("Yarn bomb explosion")] [SerializeField]
    private AudioClip yarnSFX;

    [Tooltip("Damage of the grenade")] [SerializeField]
    private int damageAmount = 1;

    [Tooltip("Slow duration of the grenade")] [SerializeField]
    private int slowDuration = 1;

    [Tooltip("Radius of the explosion")] [SerializeField]
    private int yarnGrenadeRadius;

    private int currentLevel;

    private void OnCollisionEnter()
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
                hit.gameObject.GetComponent<EnemyHit>().EnemyHurt(damageAmount);
                hit.gameObject.GetComponent<EnemyHit>().Slow(slowDuration);
            }
        }

        Destroy(explosionSphere, 0.25f);
        Destroy(gameObject);
    }
}