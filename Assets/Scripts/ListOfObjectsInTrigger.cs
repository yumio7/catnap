using System.Collections.Generic;
using UnityEngine;

public class ListOfObjectsInTrigger : MonoBehaviour
{
    public List<GameObject> enemies;

    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
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
