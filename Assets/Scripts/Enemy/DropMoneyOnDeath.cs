using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMoneyOnDeath : MonoBehaviour
{
    [SerializeField, Tooltip("Is this enemy a boss enemy? Allows money collection after round ends")] 
    private bool isBoss;
    [SerializeField] private int minMoneyValue;
    [SerializeField] private int maxMoneyValue;
    

    void OnDestroy()
    {
        if (!LevelManager.isGameOver || isBoss)
        {
            var currency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();

            currency.AddMoney(Random.Range(minMoneyValue, maxMoneyValue));
        }
    }
}
