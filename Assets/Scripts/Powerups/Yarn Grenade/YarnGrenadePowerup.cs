using UnityEngine;

public class YarnGrenadePowerup : MonoBehaviour
{
    [SerializeField] private GameObject yarnGrenadePrefab;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float cooldown;
    
    private float yarnGrenadeCooldownCounter;
    private Transform _camera;
    private Transform _projectileParent;
    
    private void Start()
    {
        yarnGrenadeCooldownCounter = cooldown;
        _camera = Camera.main.transform;
        _projectileParent = GameObject.FindGameObjectWithTag("ProjectileParent").transform;
    }
    
    private void Update()
    {
        // fire yarn grenade
        if (Input.GetKeyDown(KeyCode.Q) && yarnGrenadeCooldownCounter > cooldown)
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

        rb.AddForce((_camera.forward + _camera.up) * projectileSpeed, ForceMode.VelocityChange);

        projectile.transform.SetParent(
            _projectileParent.transform);
    }
}
