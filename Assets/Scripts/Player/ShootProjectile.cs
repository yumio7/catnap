using UnityEngine;
using UnityEngine.UI;

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;

    public float projectileSpeed = 20;

    public AudioClip hairballSFX;

    public float shootRate = 2.0f;

    private GameObject _projectileParent;

    private float elapsedTime;

    private void Start()
    {
        elapsedTime = shootRate;
        _projectileParent = GameObject.FindGameObjectWithTag("ProjectileParent");
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && elapsedTime > shootRate && !LevelManager.isGameOver)
        {
            GameObject projectile = Instantiate(projectilePrefab,
                transform.position + transform.forward, transform.rotation);
            projectile.transform.SetParent(
                _projectileParent.transform);
            
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);

            AudioSource.PlayClipAtPoint(hairballSFX, transform.position);

            elapsedTime = 0.0f;
        }
        
        elapsedTime += Time.deltaTime;
    }
}