using UnityEngine;

public class YarnGrenadePowerup : MonoBehaviour, Powerup
{
    [SerializeField] private string myName;
    [SerializeField] private string description;
    [SerializeField] private int shopCost = 1;
    [SerializeField] private Sprite mySprite;
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

        try
        {
            projectile.transform.SetParent(
                _projectileParent.transform);
        }
        catch
        {
            _projectileParent = GameObject.FindGameObjectWithTag("ProjectileParent").transform;
            projectile.transform.SetParent(
                _projectileParent.transform);
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
        return Powerup.powerupType.Q;
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
