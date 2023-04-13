using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUIBehavior : MonoBehaviour
{
    private TextMeshProUGUI myText;
    void Start()
    {
        myText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetText(string inText)
    {
        myText.text = inText;
    }

    public void SetTextColor(Color inColor)
    {
        myText.color = inColor;
    }
}
