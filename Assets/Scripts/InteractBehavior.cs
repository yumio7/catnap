using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBehavior : MonoBehaviour
{
    [SerializeField] private Canvas shopWindow;
    [SerializeField] private int interactDistance;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward * 10, Color.red);
        // INTERACT
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out var hit, interactDistance))
            {
                if (hit.transform.CompareTag("ShopkeepNPC"))
                {
                    Instantiate(shopWindow);
                }
            }
        }
    }
}