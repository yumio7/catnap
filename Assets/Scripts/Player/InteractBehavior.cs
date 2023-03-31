using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractBehavior : MonoBehaviour
{
    [SerializeField] private Canvas shopWindowPrefab;
    [SerializeField] private int interactDistance;

    private Canvas shopWindowInstance;
    private bool shopOpen;

    private void Start()
    {
        // shop not open when we start a level
        shopOpen = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            // if player is looking at shopkeep and in range...
            if (!shopOpen && 
                Physics.Raycast(transform.position, transform.forward, out hit, interactDistance))
            {
                if (hit.transform.CompareTag("ShopkeepNPC"))
                {
                    // open the shop
                    shopWindowInstance = Instantiate(shopWindowPrefab);
                    shopOpen = true;
                }
            }
            // if shop is open, we should close
            else if (shopOpen)
            {
                // close the shop
                Destroy(shopWindowInstance.gameObject);
                shopOpen = false;
            }
        }
    }
}