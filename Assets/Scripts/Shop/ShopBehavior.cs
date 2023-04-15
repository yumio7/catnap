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
        var inputArrayList = new List<GameObject>();
        
        // copy inputArray to inputArrayList
        foreach (var pp in _powerupsInput)
        {
            inputArrayList.Add(pp);
        }
        
        // remove any powerups we already have from input array
        var playerPowerups = GameObject.FindGameObjectsWithTag("Powerup");
        foreach(GameObject powerup in _powerupsInput)
        {
            var thisPowerupComponent = powerup.GetComponent<Powerup>();
            foreach (GameObject playerPowerup in playerPowerups)
            {
                var playerPowerupComponent = playerPowerup.GetComponent<Powerup>();
                if (thisPowerupComponent.GetName() == playerPowerupComponent.GetName())
                {
                    // same powerup, need to remove
                    print("removed powerup " + thisPowerupComponent.GetName());
                    inputArrayList.Remove(powerup);
                } 
            }
        }
        
        // shuffle the input array
        for (var i = 0; i < inputArrayList.Count; i++)
        {
            var j = Random.Range(i, inputArrayList.Count);

            // swap between j and i to shuffle
            (inputArrayList[i], inputArrayList[j]) = (inputArrayList[j], inputArrayList[i]);
        }

        // select the first three items
        for (var i = 0; i < 3; i++)
        {
            _shopOptions.Add(inputArrayList[i]);
        }

        // update the item cards
        var itemCards = GameObject.FindGameObjectsWithTag("ShopItemCard");
        for (var i = 0; i < 3; i++)
        {
            var itemCard = itemCards[i].GetComponent<ItemCard>();
            var curPowerup = _shopOptions[i].GetComponent<Powerup>();
            itemCard.SetTitleText(curPowerup.GetName());
            itemCard.SetDescriptionText(curPowerup.GetDescription());
            itemCard.SetCostText("Cost: " + curPowerup.GetCost());
            itemCard.SetImage(curPowerup.GetSprite());
            itemCard.SetOverwriteWarningTextActive(false);

            // player can get infinite augments, but we need to check for conflict on paw or grenade
            var conflictPowerup = FindPowerupsConflictingWith(curPowerup);
            if (conflictPowerup != null)
            {
                // conflict found
                var powerupComponent = conflictPowerup.GetComponent<Powerup>();
                itemCard.SetOverwriteWarningText("Warning! Will overwrite your " + powerupComponent.GetName());
                itemCard.SetOverwriteWarningTextActive(true);
            }
        }
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
            // player can get infinite augments, but we need to check for conflict on paw or grenade
            var curPowerup = _shopOptions[buttonIndex].GetComponent<Powerup>();
            var conflictPowerup = FindPowerupsConflictingWith(curPowerup);
            if (conflictPowerup != null)
            {
                // conflict found, destroy the existing one
                Destroy(conflictPowerup);
            }
            
            // buy the new powerup, add that component to the player
            Instantiate(_shopOptions[buttonIndex], player.transform, true);
            // and remove that currency from the player
            playerCurrency.RemoveMoney(costOfItem);
        }
        else
        {
            print("Can't afford!!");
        }
    }

    public void SetShopOpened(bool open)
    {
        if (open)
        {
            // if we are opening the shop, disable player controls
            SetPlayerControls(false);
            // and open the shop window
            gameObject.SetActive(true);
        }
        else
        {
            // if we are closing the shop, enable player controls
            SetPlayerControls(true);
            // and close the shop window
            gameObject.SetActive(false);
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
        
        Camera.main.GetComponent<ShootProjectile>().enabled = value;
        player.GetComponent<PlayerController>().enabled = value;
        mainCam.GetComponent<MouseLook>().enabled = value;
    }

    private GameObject FindPowerupsConflictingWith(Powerup curPowerup)
    {
        // find conflicting powerups
        if (curPowerup.GetSlot() != Powerup.powerupType.Augment)
        {
            var powerupConflict = FindPowerupsConflictingWithHelper(curPowerup);
            if (powerupConflict != null)
            {
                // var powerupComponent = powerupConflict.GetComponent<Powerup>();
                // conflict found
                return powerupConflict;
            }
        }
        
        // no conflict
        return null;
    }

    // check if the player already has a powerup of this type, return null if none found
    GameObject FindPowerupsConflictingWithHelper(Powerup inPowerup)
    {
        var type = inPowerup.GetSlot();

        var listOfPowerups = GameObject.FindGameObjectsWithTag("Powerup");

        foreach (var p in listOfPowerups)
        {
            var powerupScript = p.GetComponent<Powerup>();
            if (powerupScript.GetSlot() == type)
            {
                // conflict found, return conflicting powerup
                return p;
            }
        }

        // no conflict found
        return null;
    }
}