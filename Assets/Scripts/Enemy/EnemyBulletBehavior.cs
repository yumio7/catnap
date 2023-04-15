using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBulletBehavior : MonoBehaviour
{

    private PlayerHealth player;
    [SerializeField] private int damage;
    [SerializeField] private int speed;
    private Vector3 point;
    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        player = p.GetComponent<PlayerHealth>();
        point = p.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3.MoveTowards(this.transform.position, point, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.TakeDamage(damage);
        }
    }
}
