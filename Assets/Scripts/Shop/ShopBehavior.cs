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

    // shopOpen so that the player can't press E again if shop is already open
    private bool shopOpen;

    void Start()
    {
        GenerateShopOptions();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void GenerateShopOptions()
    {
        shopOpen = true;
        
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
            itemCards[i].GetComponent<ItemCard>().SetTitleText(_shopOptions[i].ToString());
        }
    }

    public bool ShopIsOpen()
    {
        return shopOpen;
    }
}