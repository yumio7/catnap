using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShopBehavior : MonoBehaviour
{
    // this class generates a shop popup with 3 selections of powerup
    [SerializeField] private String[] _powerupsInput;

    // list of powerupsInput in ArrayList form
    private List<String> _powerupsInputArrayList;

    // set of shop options to be displayed for players
    private List<String> _shopOptions;
    
    void Start()
    {
        
        GenerateShopOptions();

        // free the cursor and make it visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // disable player movement and controls
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<PlayerHealth>().enabled = false;
        player.GetComponent<PlayerPowerups>().enabled = false;
        
        // disable camera movement
        Camera camera = Camera.main;
        camera.GetComponent<MouseLook>().enabled = false;
        camera.GetComponent<ShootProjectile>().enabled = false;
    }

    void GenerateShopOptions()
    {
        // initialize arraylists
        _powerupsInputArrayList = new List<String>();
        _shopOptions = new List<String>();

        // copy the array over, we need an arraylist for the removeat method
        foreach (var t in _powerupsInput)
        {
            _powerupsInputArrayList.Add(t);
        }

        // select powerups
        for (int i = 0; i < 3; i++)
        {
            // select this powerup
            int powerupSelectionIdx = Random.Range(0, _powerupsInputArrayList.Count - i);
            _shopOptions.Add(_powerupsInputArrayList[powerupSelectionIdx]);

            // remove it from the list of powerups
            _powerupsInputArrayList.RemoveAt(powerupSelectionIdx);
        }

        // grab the three text areas in the canvas I am attached to
        GameObject[] itemCards = GameObject.FindGameObjectsWithTag("ShopItemCard");

        for (int i = 0; i < 3; i++)
        {
            itemCards[i].GetComponent<ItemCard>().SetTitleText(_shopOptions[i]);
        }
    }

    void OnDestroy()
    {
        // lock the cursor and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // enable player movement and controls
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<PlayerHealth>().enabled = true;
        player.GetComponent<PlayerPowerups>().enabled = true;

        // enable camera movement
        Camera camera = Camera.main;
        camera.GetComponent<MouseLook>().enabled = true;
        camera.GetComponent<ShootProjectile>().enabled = true;
    }

    
    public void OnButtonClicked(int buttonIndex)
    {
        Debug.Log("Button " + (buttonIndex + 1) + " clicked");
        // Do whatever you want to do when a button is clicked
        Debug.Log("Powerup Selected: " + _shopOptions[buttonIndex]);
    }
}