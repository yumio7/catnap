using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractBehavior : MonoBehaviour
{
    [SerializeField] private GameObject shopWindowPrefab;
    [SerializeField] private int interactDistance;

    private GameObject shopWindowInstance;
    private bool shopOpen;
    private ShopBehavior shopPopupBehavior;

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
                Physics.Raycast(transform.position, transform.forward, out hit, interactDistance) &&
                ShopkeepBehavior.hasTalked)
            {
                if (hit.transform.CompareTag("ShopkeepNPC"))
                {
                    // open the shop
                    if (shopWindowInstance == null)
                    {
                        shopWindowInstance = Instantiate(shopWindowPrefab);
                        shopPopupBehavior = shopWindowInstance.GetComponent<ShopBehavior>();
                    }
                    else
                    {
                        shopWindowInstance.GetComponent<ShopBehavior>().SetShopOpened(true);
                        shopPopupBehavior.SetShopOpened(true);
                    }

                    shopOpen = true; 
                }
            }
            // if shop is open, we should close
            else if (shopOpen)
            {
                // close the shop
                shopPopupBehavior.SetShopOpened(false);
                shopOpen = false;
            }
        }
    }
}