using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShopBehavior : MonoBehaviour
{
    // this class generates a shop popup with 3 selections of powerup
    [SerializeField] private string[] _powerupsInput;

    // set of shop options to be displayed for players
    private List<string> _shopOptions;

    private GameObject player;
    private Camera mainCam;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCam = Camera.main;

        // generate shop
        GenerateShopOptions();

        // disable player movement and controls
        SetPlayerControls(false);
    }

    private void GenerateShopOptions()
    {
        // initialize lists
        _shopOptions = new List<string>();

        // shuffle the input array
        for (var i = 0; i < _powerupsInput.Length; i++)
        {
            var j = Random.Range(i, _powerupsInput.Length);
            
            // swap between j and i to shuffle
            (_powerupsInput[i], _powerupsInput[j]) = (_powerupsInput[j], _powerupsInput[i]);
        }

        // select the first three items
        for (var i = 0; i < 3; i++)
        {
            _shopOptions.Add(_powerupsInput[i]);
        }

        // update the item cards
        var itemCards = GameObject.FindGameObjectsWithTag("ShopItemCard");
        for (var i = 0; i < 3; i++)
        {
            itemCards[i].GetComponent<ItemCard>().SetTitleText(_shopOptions[i]);
        }
    }

    private void OnDestroy()
    {
        // re-enable player movement and controls
        SetPlayerControls(true);
    }


    public void OnButtonClicked(int buttonIndex)
    {
        Debug.Log("Button " + (buttonIndex + 1) + " clicked");
        // Do whatever you want to do when a button is clicked
        Debug.Log("Powerup Selected: " + _shopOptions[buttonIndex]);
    }

    private void SetPlayerControls(bool value)
    {
        if (value)
        {
            // giving player controls
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            // removing player controls
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        player.GetComponent<PlayerController>().enabled = value;
        mainCam.GetComponent<MouseLook>().enabled = value;
    }
}