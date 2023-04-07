using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnpointBehavior : MonoBehaviour
{
    private GameObject player;
    private bool teleportedAlready;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        teleportedAlready = false;
    }

    void LateUpdate()
    {
        if (!teleportedAlready)
        {
            Invoke(nameof(Teleport), 0.5f);
            teleportedAlready = true;
        }
    }

    void Teleport()
    {
        player.transform.position = gameObject.transform.position;
    }
}