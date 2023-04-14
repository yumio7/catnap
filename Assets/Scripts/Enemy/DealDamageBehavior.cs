using UnityEngine;

public class DealDamageBehavior : MonoBehaviour
{
    [SerializeField] private int _attackDamage;
    [SerializeField] private int _attackRate;

    private PlayerHealth _playerHealth;
    private float _timeSinceLastAttack = 0f;

    private void Start()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        _timeSinceLastAttack = _attackRate;
    }

    private void Update()
    {
        _timeSinceLastAttack += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && _timeSinceLastAttack >= _attackRate)
        {
            _timeSinceLastAttack = 0f;
            _playerHealth.TakeDamage(_attackDamage);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_timeSinceLastAttack >= _attackRate)
            {
                _timeSinceLastAttack = 0f;
                _playerHealth.TakeDamage(_attackDamage);
            }
        }
    }
}