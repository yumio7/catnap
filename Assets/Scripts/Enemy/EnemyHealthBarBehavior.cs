using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarBehavior : MonoBehaviour
{
    
    private EnemyHit _health;
    private Slider _healthSlider;

    void Start()
    {
        _health = GetComponentInParent<EnemyHit>();
        _healthSlider = GetComponentInChildren<Slider>();
        _healthSlider.maxValue = _health.GetMaxHealth();
    }

    void Update()
    {
        if (_healthSlider.maxValue == 0)
        {
            _healthSlider.maxValue = _health.GetMaxHealth();
        }

        _healthSlider.value = _health.GetCurrentHealth();
    }
}
