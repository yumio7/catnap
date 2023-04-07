using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrency : MonoBehaviour
{
    public int money = 0;
    
    void Start()
    {
        
    }

    public bool CanBuy(int cost)
    {
        return money >= cost;
    }

    public int GetMoneyAmount()
    {
        return money;
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public void RemoveMoney(int amount)
    {
        money -= amount;
    }
}
