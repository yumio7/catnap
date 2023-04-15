using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && ShopkeepBehavior.hasTalked)
        {
            FindObjectOfType<LevelManager>().LevelBeat();;
        }
    }
}
