using UnityEngine;

public class YarnGrenadePowerup : MonoBehaviour
{
    [SerializeField] private GameObject yarnGrenadePrefab;
    [SerializeField] private float yarnGrenadeProjectileSpeed;
    [SerializeField] private float yarnGrenadeCooldown;
    
    private float yarnGrenadeCooldownCounter;
    private Transform _camera;
    private Transform _projectileParent;
    
    private void Start()
    {
        yarnGrenadeCooldownCounter = yarnGrenadeCooldown;
        _camera = Camera.main.transform;
        _projectileParent = GameObject.FindGameObjectWithTag("ProjectileParent").transform;
    }
    
    private void Update()
    {
        // fire yarn grenade
        if (Input.GetKeyDown(KeyCode.Q) && yarnGrenadeCooldownCounter > yarnGrenadeCooldown)
        {
            FireYarnGrenade();
            yarnGrenadeCooldownCounter = 0;
        }
        
        yarnGrenadeCooldownCounter += Time.deltaTime;
    }
    
    public void FireYarnGrenade()
    {
        GameObject projectile = Instantiate(yarnGrenadePrefab,
            _camera.position + _camera.forward, _camera.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        rb.AddForce((_camera.forward + _camera.up) * yarnGrenadeProjectileSpeed, ForceMode.VelocityChange);

        projectile.transform.SetParent(
            _projectileParent.transform);
    }
}
