using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerups : MonoBehaviour
{

    [SerializeField] private GameObject yarnGrenadePrefab;
    [SerializeField] private float yarnProjectileSpeed;
    [SerializeField] private GameObject fireballPrefab;

    private int yarnGrenadeLevel = 1;
    private int fireballLevel = 0;
    private int powerMeowLevel = 0;
    private GameObject _projectileParent;
    private Transform _camera;
    
    // Start is called before the first frame update
    void Start()
    {
        _projectileParent = GameObject.FindGameObjectWithTag("ProjectileParent");
        _camera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void YarnGrenadeUpgrade()
    {
        yarnGrenadeLevel++;
    }

    public int GetYarnGrenadeLevel()
    {
        return yarnGrenadeLevel;
    }

    public void FireYarnGrenade()
    {
        GameObject projectile = Instantiate(yarnGrenadePrefab,
            _camera.position + _camera.forward, _camera.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        rb.AddForce((_camera.forward + _camera.up) * yarnProjectileSpeed, ForceMode.VelocityChange);

        projectile.transform.SetParent(
            _projectileParent.transform);
    }

    public void FireballUpgrade()
    {
        fireballLevel++;
    }

    public void PowerMeowUpgrade()
    {
        powerMeowLevel++;
    }
}
