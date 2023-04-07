using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractBehavior : MonoBehaviour
{
    [SerializeField] private Canvas shopWindowPrefab;
    [SerializeField] private int interactDistance;
    // this string represents the name of the next level
    [SerializeField] private string _nextLevel;

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
                Invoke(nameof(LoadLevel), 3f);
            }
        }
    }

    private void LoadLevel()
    {
        var levelMan = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        levelMan.LoadLevel();
    }
}