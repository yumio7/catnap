using System.Collections;
using UnityEngine;

public class NewWeaponPowerup : MonoBehaviour, Powerup
{
    [SerializeField] private string myName;
    [SerializeField] private string description;
    [SerializeField] private int shopCost = 1;
    [SerializeField] private Sprite mySprite;
    [SerializeField] private GameObject newPawPrefab;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float firingDelay = 0.1f;

    private GameObject currentPaw;
    private GameObject projectileParent;
    private Transform playerCamera;
    private bool firing = false;

    private void Start()
    {
        currentPaw = GameObject.FindGameObjectWithTag("Paw");
        projectileParent = GameObject.FindGameObjectWithTag("ProjectileParent");
        playerCamera = Camera.main.transform;
        Destroy(currentPaw.gameObject);
        currentPaw = Instantiate(newPawPrefab, currentPaw.transform.position, currentPaw.transform.rotation, transform);
        currentPaw.transform.parent = playerCamera;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !firing)
        {
            firing = true;
            StartCoroutine(FireProjectiles());
        }
        else if (Input.GetKeyUp(KeyCode.F) && firing)
        {
            firing = false;
        }
    }

    private IEnumerator FireProjectiles()
    {
        while (firing)
        {
            GameObject projectile = Instantiate(projectilePrefab,
                playerCamera.position + playerCamera.forward, playerCamera.rotation);

            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            rb.AddForce(playerCamera.forward * projectileSpeed, ForceMode.VelocityChange);

            try
            {
                projectile.transform.SetParent(projectileParent.transform);
            }
            catch
            {
                projectileParent = GameObject.FindGameObjectWithTag("ProjectileParent");
                projectile.transform.SetParent(projectileParent.transform);
            }

            yield return new WaitForSeconds(firingDelay);
        }
    }

    public string GetName()
    {
        return myName;
    }

    public string GetDescription()
    {
        return description;
    }

    public Powerup.powerupType GetSlot()
    {
        return Powerup.powerupType.Paw;
    }

    public int GetCost()
    {
        return shopCost;
    }
    
    public Sprite GetSprite()
    {
        return mySprite;
    }
}