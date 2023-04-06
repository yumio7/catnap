using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class NewWeaponPowerup : MonoBehaviour
{
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
        Destroy(currentPaw.gameObject);
        currentPaw = Instantiate(newPawPrefab, currentPaw.transform.position, currentPaw.transform.rotation, transform);
        projectileParent = GameObject.FindGameObjectWithTag("ProjectileParent");
        playerCamera = Camera.main.transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            firing = true;
            StartCoroutine(FireProjectiles());
        }
        else if (Input.GetKeyUp(KeyCode.F))
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

            projectile.transform.SetParent(projectileParent.transform);

            yield return new WaitForSeconds(firingDelay);
        }
    }
}