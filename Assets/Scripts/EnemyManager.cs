using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int enemyCap = 10;

    public int enemyCount = 0;

    public int enemiesKilled = 0;

    public int killsForBossSpawn = 2;

    [SerializeField] private GameObject boss;

    public static EnemyManager Instance;
    
    private bool _spawned = false;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesKilled >= killsForBossSpawn && !_spawned)
        {
            Instantiate(boss);
            _spawned = true;
        }
    }
}
