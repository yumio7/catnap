using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCard : MonoBehaviour
{

    private TextMeshProUGUI[] texts;
    private TextMeshProUGUI title;
    private TextMeshProUGUI description;
    private TextMeshProUGUI overwriteWarning;
    private TextMeshProUGUI cost;

    private Image[] imagePanels;
    private Image image;
    
    // Start is called before the first frame update
    private void Start()
    {
        RetrieveTexts();
        RetrieveImagePanel();
    }

    void RetrieveImagePanel()
    {
        imagePanels = GetComponentsInChildren<Image>();
        foreach (Image i in imagePanels)
        {
            if (i.CompareTag("ShopItemImage"))
            {
                image = i;
            }
        }
    }

    void RetrieveTexts()
    {
        texts = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI t in texts)
        {
            if (t.CompareTag("ShopEntryTitle"))
            {
                title = t;
            }
            else if (t.CompareTag("ShopEntryDescription"))
            {
                description = t;
            }
            else if (t.CompareTag("ShopEntryOverwriteWarning"))
            {
                overwriteWarning = t;
            } else if (t.CompareTag("ShopEntryCost"))
            {
                cost = t;
            }
        }
    }

    public void SetTitleText(String text)
    {
        if (title == null)
        {
            RetrieveTexts();
        }
        
        title.text = text;
    }

    public void SetDescriptionText(String text)
    {
        if (description == null)
        {
            RetrieveTexts();
        }

        description.text = text;
    }

    public void SetOverwriteWarningText(String text)
    {
        if (overwriteWarning == null)
        {
            RetrieveTexts();
        }

        overwriteWarning.text = text;
    }

    public void SetOverwriteWarningTextActive(bool active)
    {
        if (overwriteWarning == null)
        {
            RetrieveTexts();
        }

        overwriteWarning.text = active ? overwriteWarning.text : ""; 
    }

    public void SetCostText(String text)
    {
        if (cost == null)
        {
            RetrieveTexts();
        }
        
        cost.text = text;
    }

    public void SetImage(Sprite inputSprite)
    {
        if (image == null)
        {
            RetrieveImagePanel();
        }
        image.sprite = inputSprite;
    }
}
