using UnityEngine;
using UnityEngine.AI;

public class SneakersPowerup : MonoBehaviour, Powerup
{
    [SerializeField] private string myName;
    [SerializeField] private string description;
    [SerializeField] private int shopCost = 1;
    [SerializeField] private Sprite mySprite;

    private GameObject player;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().speed *= 1.5f;
    }

    public string GetName()
    {
        return myName;
    }

    public string GetDescription()
    {
        return description;
    }

    public Powerup.powerupType GetSlot()
    {
        return Powerup.powerupType.Augment;
    }

    public int GetCost()
    {
        return shopCost;
    }
    
    public Sprite GetSprite()
    {
        return mySprite;
    }
}
