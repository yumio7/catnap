using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetrievePowerupUI : MonoBehaviour
{
    [SerializeField] private Powerup.powerupType type;
    [SerializeField] private Sprite noPowerupSprite;
    private GameObject[] powerups;

    void FixedUpdate()
    {
        powerups = GameObject.FindGameObjectsWithTag("Powerup");
        bool powerupFound = false;
        
        foreach (var powerup in powerups)
        {
            var powerupScript = powerup.GetComponent<Powerup>();
            if (powerupScript.GetSlot() == type)
            {
                powerupFound = true;
                var sprite = powerupScript.GetSprite();
                if (sprite != null)
                {
                    gameObject.GetComponent<Image>().sprite = sprite;
                }
            }
        }

        if (!powerupFound)
        {
            gameObject.GetComponent<Image>().sprite = noPowerupSprite;
        }
    }
}
