using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnpointBehavior : MonoBehaviour
{
    private bool _teleported;
    private Vector3 _spawnPos;
    
    private void Awake()
    {
        _spawnPos = GameObject.FindGameObjectWithTag("Spawnpoint").transform.position;
        SceneManager.sceneLoaded += MoveToSpawn;
        _teleported = false;
    }

    private void FixedUpdate()
    {
        if (_teleported) return;
        gameObject.transform.position = _spawnPos;
        _teleported = true;
    }

    private void MoveToSpawn(Scene scene, LoadSceneMode mode)
    {
        _spawnPos = GameObject.FindGameObjectWithTag("Spawnpoint").transform.position;
        _teleported = false;
    }
}