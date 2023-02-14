using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class SpawnerBehavior : MonoBehaviour
{
    // Object to be spawned by spawner
    [SerializeField] private GameObject spawnedObject;

    // Total number of objects to be spawned over the
    // lifetime of this spawner
    [SerializeField] private int totalInstances;

    // Maximum delay between spawns
    [SerializeField] private float spawnDelay;
    
    // X distance away from spawner that object can spawn
    [SerializeField] private float xRange;
    
    // Z distance away from spawner that object can spawn
    [SerializeField] private float zRange;

    // The number of objects that this spawner has spawned
    private int _instanceCounter;

    // The last time that this spawner has spawned an object
    private float _lastTime;

    // Clamped between .5 and 1, affects the spawn rate
    // based on how many enemies are in the scene relative to the cap
    private float _spawnRateModifier;

    private EnemyManager _enemyManager;
    
    
    void Start()
    {
        // Set the instance counter to zero and initialize the time
        _instanceCounter = 0;
        _lastTime = Time.time;
        _enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // We calculate the spawn rate modifier and 
        // possible spawn enemies
        _calculateSpawnRateModifier();
        _spawnerLogic();
    }
    
    // Spawns an instance of the specified object
    // if we have any spawns remaining
    private void _spawnInstance()
    {
        if (_instanceCounter < totalInstances)
        {
            Instantiate(spawnedObject, _vectorInRange(), Quaternion.identity);
            _instanceCounter += 1;
            _enemyManager.enemyCount += 1;
        }
    }

    // Spawns enemies at a certain rate based on the current amount of enemies
    // in the scene and the base spawn rate
    private void _spawnerLogic()
    {
        float currentTime = Time.time;
        // Spawns enemies on the given delay modified by the spawnrate modifier
        if (_inRange(currentTime % (spawnDelay * _spawnRateModifier), -.01f, .01f) 
            && ((currentTime - _lastTime) > (spawnDelay / 2))
            && _enemyManager.enemyCount < _enemyManager.enemyCap)
        {
            _spawnInstance();
            _lastTime = currentTime;
        }
    }

    // Calculates the spawn rate modifier based on how full the scene is of enemies
    private void _calculateSpawnRateModifier()
    {
        float count = _enemyManager.enemyCount;

        if (count == 0)
        {
            count = 1;
        }

        _spawnRateModifier = Mathf.Clamp(count / (float)_enemyManager.enemyCap, .5f, 1f);
        
    }

    // Returns true if a val is within range, false otherwies
    private bool _inRange(float val, float low, float high)
    {
        return (val >= low && val <= high);
    }

    // Returns a vector within the x range and z range of the spawner
    private Vector3 _vectorInRange()
    {
        Vector3 pos = this.transform.position;
        Vector3 ret = new Vector3();

        ret.x = Random.Range(pos.x - xRange, pos.x + xRange);
        ret.z = Random.Range(pos.z - zRange, pos.z + zRange);
        ret.y = pos.y;

        return ret;
    }
    
}
