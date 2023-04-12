using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderUpdate : MonoBehaviour
{
    public TextMeshProUGUI sliderValue;
    public Slider slider;
    void Start()
    {
        slider.value = PlayerPrefs.GetInt("MouseSenstivity", 50);
    }

    // Update is called once per frame
    void Update()
    {
        sliderValue.text = slider.value.ToString();
    }
    
    public void UpdatePref()
    {
        PlayerPrefs.SetInt("MouseSenstivity", (int) slider.value);
    }
}