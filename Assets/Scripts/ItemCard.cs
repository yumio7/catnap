using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCard : MonoBehaviour
{

    private Text _titleText;
    
    // Start is called before the first frame update
    void Start()
    {
        _titleText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTitleText(String text)
    {
        _titleText.text = text;
    }
}
