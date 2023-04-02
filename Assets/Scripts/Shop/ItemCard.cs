using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemCard : MonoBehaviour
{

    private Text _titleText;
    
    // Start is called before the first frame update
    private void Start()
    {
        _titleText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void SetTitleText(String text)
    {
        if (_titleText != null)
        {
            _titleText.text = text;
        }
        else
        {
            _titleText = GetComponentInChildren<Text>();
            _titleText.text = text;
        }

    }
}
