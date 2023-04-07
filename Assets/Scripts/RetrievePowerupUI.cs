using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetrievePowerupUI : MonoBehaviour
{
    [SerializeField] private Powerup.powerupType type;
    private GameObject[] powerups;

    void FixedUpdate()
    {
        powerups = GameObject.FindGameObjectsWithTag("Powerup");
        
        foreach (var powerup in powerups)
        {
            var powerupScript = powerup.GetComponent<Powerup>();
            if (powerupScript.GetSlot() == type)
            {
                var sprite = powerupScript.GetSprite();
                if (sprite != null)
                {
                    gameObject.GetComponent<Image>().sprite = sprite;
                }
                else
                {
                    // TODO what happens if there is no sprite?
                }
            }
        }
    }
}
