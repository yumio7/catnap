using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageBehavior : MonoBehaviour
{
    [SerializeField] private int _attackDamage;
    [SerializeField] private int _attackRate;

    private PlayerHealth _playerHealth;
    private float _timeSinceLastAttack = 0f;

    void Start()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerHealth.TakeDamage(_attackDamage);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _timeSinceLastAttack += Time.deltaTime;
            if (_timeSinceLastAttack >= _attackRate)
            {
                _timeSinceLastAttack = 0f;
                _playerHealth.TakeDamage(_attackDamage);
            }
        }
    }
}