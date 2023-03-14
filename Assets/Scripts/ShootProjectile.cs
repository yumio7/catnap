using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;

    public float projectileSpeed = 20;

    public AudioClip hairballSFX;

    public Image reticleImage;

    public Color reticleDementorColor;
    
    public float shootRate = 2.0f;

    private GameObject _projectileParent;

    private Color originalReticleColor;

    private float elapsedTime;

    void Start()
    {
        elapsedTime = shootRate;
        originalReticleColor = reticleImage.color;
        _projectileParent = GameObject.FindGameObjectWithTag("ProjectileParent");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && elapsedTime > shootRate)
        {
            GameObject projectile = Instantiate(projectilePrefab,
                transform.position + transform.forward, transform.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);

            projectile.transform.SetParent(
                _projectileParent.transform);

            AudioSource.PlayClipAtPoint(hairballSFX, transform.position);

            elapsedTime = 0.0f;
        }
        
        elapsedTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        ReticleEffect();
    }

    void ReticleEffect()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                reticleImage.color = Color.Lerp(
                    reticleImage.color, reticleDementorColor, Time.deltaTime * 2);
                reticleImage.transform.localScale = Vector3.Lerp(
                    reticleImage.transform.localScale, new Vector3(0.7f, 0.7f, 0.7f),
                    Time.deltaTime * 2);
            }
            else
            {
                reticleImage.color = Color.Lerp(
                    reticleImage.color, originalReticleColor, Time.deltaTime * 2);
                reticleImage.transform.localScale = Vector3.Lerp(
                    reticleImage.transform.localScale, Vector3.one,
                    Time.deltaTime * 2);
            }
        }
    } 
}