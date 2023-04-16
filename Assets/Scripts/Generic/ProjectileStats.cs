using UnityEngine;

public class ProjectileStats : MonoBehaviour
{

    [SerializeField] private int damageDealt;
    [SerializeField] private bool destroyOnCollide;

    public int GetDamageDealt()
    {
        return damageDealt;
    }

    void OnCollisionEnter(Collision other)
    {
        if (destroyOnCollide && !other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
