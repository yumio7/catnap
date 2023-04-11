using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShopBehavior : MonoBehaviour
{
    // this class generates a shop popup with 3 selections of powerup
    [SerializeField] private GameObject[] _powerupsInput;

    // set of shop options to be displayed for players
    private List<GameObject> _shopOptions;

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
        _shopOptions = new List<GameObject>();

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
            var itemCard = itemCards[i].GetComponent<ItemCard>();
            var curPowerup = _shopOptions[i].GetComponent<Powerup>();
            itemCard.SetTitleText(curPowerup.GetName());
            itemCard.SetDescriptionText(curPowerup.GetDescription());
            itemCard.SetImage(curPowerup.GetSprite());
            // TODO if player has this powerup type already (Q or F), add overwrite warning
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
        
        // check if player can afford it
        var playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();
        var costOfItem = _shopOptions[buttonIndex].GetComponent<Powerup>().GetCost();

        if (playerCurrency.CanBuy(costOfItem))
        {
            // add that component to the player
            var powerup = Instantiate(_shopOptions[buttonIndex]);
            powerup.transform.parent = player.transform;
            // and remove that currency from the player
            playerCurrency.RemoveMoney(costOfItem);
        }
        else
        {
            print("Can't afford!!");
        }
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