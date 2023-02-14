using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfObjectsInTrigger : MonoBehaviour
{
    public List<GameObject> enemies;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag("Enemy"))
        {
            enemies.Add(obj);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag("Enemy"))
        {
            enemies.Remove(obj);
        }
    }
}
