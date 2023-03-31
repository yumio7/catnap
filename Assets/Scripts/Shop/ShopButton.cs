using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    // reference to the button component
    private Button _button;

    // reference to the ShopBehavior script
    private ShopBehavior _shopBehavior;

    // index of this button
    public int buttonIndex;

    void Start()
    {
        // get a reference to the button component
        _button = GetComponent<Button>();

        // get a reference to the ShopBehavior script
        _shopBehavior = FindObjectOfType<ShopBehavior>();

        // add an event listener to the button
        _button.onClick.AddListener(OnButtonClicked);
    }

    void OnButtonClicked()
    {
        // call the OnButtonClicked method in the ShopBehavior script
        _shopBehavior.OnButtonClicked(buttonIndex);
    }
}
