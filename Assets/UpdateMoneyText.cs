using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateMoneyText : MonoBehaviour
{

    private Text moneyText;
    private PlayerCurrency currency;
    
    void Start()
    {
        moneyText = gameObject.GetComponent<Text>();
        currency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();
    }
    
    void FixedUpdate()
    {
        moneyText.text = "Cash: " + currency.GetMoneyAmount();
    }
}
