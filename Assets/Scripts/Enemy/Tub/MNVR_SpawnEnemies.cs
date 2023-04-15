using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MNVR_SpawnEnemies : MonoBehaviour, Maneuver
{
    [SerializeField] private int numOfEnemiesToSpawn;
    [SerializeField] private GameObject enemySpawnPrefab;
    [SerializeField] private float xSpawnRadius;
    [SerializeField] private float zSpawnRadius;
    [SerializeField] private float ySpawnOffset;
    
    public void Activate()
    {
        for (int i = 0; i < numOfEnemiesToSpawn; i++)
        {
            var xSpawnPos = Random.Range(-xSpawnRadius, xSpawnRadius);
            if (xSpawnPos < 3)
            {
                xSpawnPos = 3;
            } else if (xSpawnPos > -3)
            {
                xSpawnPos = -3;
            }
            
            var zSpawnPos = Random.Range(-zSpawnRadius, zSpawnRadius);
            if (zSpawnPos < 6)
            {
                zSpawnPos = 6;
            } else if (zSpawnPos > -6)
            {
                zSpawnPos = -6;
            }

            var ySpawnPos = transform.position.y + ySpawnOffset;
            
            var spawnPosition = 
                new Vector3(xSpawnPos, ySpawnPos, zSpawnPos);
            
            Instantiate(enemySpawnPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
