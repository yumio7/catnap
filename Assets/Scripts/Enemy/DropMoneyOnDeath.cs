using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMoneyOnDeath : MonoBehaviour
{
    [SerializeField] private int minMoneyValue;
    [SerializeField] private int maxMoneyValue;
    

    void OnDestroy()
    {
        var currency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();
        
        currency.AddMoney(Random.Range(minMoneyValue, maxMoneyValue));
    }
}
