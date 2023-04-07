using UnityEngine;

public class ProjectileStats : MonoBehaviour
{

    [SerializeField] private int damageDealt;

    public int GetDamageDealt()
    {
        return damageDealt;
    }
}
