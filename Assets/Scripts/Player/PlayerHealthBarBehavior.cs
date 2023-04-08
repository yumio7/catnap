using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarBehavior : MonoBehaviour
{

    private PlayerHealth _playerHealth;
    private Slider _healthSlider;
    
    void Start()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        _healthSlider = gameObject.GetComponent<Slider>();
    }
    
    void Update()
    {
        _healthSlider.value = _playerHealth.GetHealth();
    }
}
