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

    // Cap of all enemies in scene
    public static int EnemyCap = 5;

    // Counter of all enemies alive in scene
    // MAY WANT TO MOVE THIS AND ENEMY CAP TO LEVEL MANAGER
    // OR AN "ENEMY MANAGER" SCRIPT
    public static int EnemiesAlive = 0;

    // The number of objects that this spawner has spawned
    private int _instanceCounter;

    // The last time that this spawner has spawned an object
    private float _lastTime;

    // Clamped between .5 and 1, affects the spawn rate
    // based on how many enemies are in the scene relative to the cap
    private float _spawnRateModifier;
    
    void Start()
    {
        // Set the instance counter to zero and initialize the time
        _instanceCounter = 0;
        _lastTime = Time.time;
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
            Instantiate(spawnedObject, transform.position, Quaternion.identity);
            _instanceCounter += 1;
            EnemiesAlive += 1;
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
            && EnemiesAlive < EnemyCap)
        {
            _spawnInstance();
            _lastTime = currentTime;
        }
    }

    // Calculates the spawn rate modifier based on how full the scene is of enemies
    private void _calculateSpawnRateModifier()
    {
        float count = EnemiesAlive;

        if (count == 0)
        {
            count = 1;
        }

        _spawnRateModifier = Mathf.Clamp(count / (float)EnemyCap, .5f, 1f);
        
    }

    // Returns true if a val is within range, false otherwies
    private bool _inRange(float val, float low, float high)
    {
        return (val >= low && val <= high);
    }
    
}
