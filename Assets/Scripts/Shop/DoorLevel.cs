using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorLevel : MonoBehaviour
{
    [SerializeField] private string _nextLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && ShopkeepBehavior.hasTalked)
        {
            SceneManager.LoadScene(_nextLevel);
        }
    }
}
