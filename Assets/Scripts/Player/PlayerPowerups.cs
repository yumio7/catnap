using UnityEngine;

public class PlayerPowerups : MonoBehaviour
{

    [SerializeField] private GameObject yarnGrenadePrefab;
    [SerializeField] private float yarnProjectileSpeed;
    [SerializeField] private float yarnGrenadeCooldown;
    [SerializeField] private float powerMeowCooldown;

    private int yarnGrenadeLevel = 1;
    private int powerMeowLevel = 0;
    private GameObject _projectileParent;
    private GameObject _player;
    private Transform _camera;
    private PlayerHealth _playerHealth;

    private float yarnGrenadeCooldownCounter;
    private float powerMeowCooldownCounter;

    // Start is called before the first frame update
    private void Start()
    {
        _projectileParent = GameObject.FindGameObjectWithTag("ProjectileParent");
        _player = GameObject.FindGameObjectWithTag("Player");
        _camera = Camera.main.transform;
        _playerHealth = gameObject.GetComponent<PlayerHealth>();
        
        yarnGrenadeCooldownCounter = yarnGrenadeCooldown;
        powerMeowCooldownCounter = powerMeowCooldown;
    }

    // Update is called once per frame
    private void Update()
    {
        // YARN GRENADE
        if (Input.GetKeyDown(KeyCode.Q) && GetYarnGrenadeLevel() > 0 && yarnGrenadeCooldownCounter > yarnGrenadeCooldown)
        {
            FireYarnGrenade();
            yarnGrenadeCooldownCounter = 0;
        }
        
        // POWER MEOW
        if (_playerHealth.GetHealth() < 25 && GetPowerMeowLevel() > 0 && powerMeowCooldownCounter > powerMeowCooldown)
        {
            FirePowerMeow();
            powerMeowCooldownCounter = 0;
        }
        
        // increment cooldown counters
        yarnGrenadeCooldownCounter += Time.deltaTime;
        powerMeowCooldownCounter += Time.deltaTime;
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

    public void PowerMeowUpgrade()
    {
        powerMeowLevel++;
    }

    public int GetPowerMeowLevel()
    {
        return powerMeowLevel;
    }

    public void FirePowerMeow()
    {
        // TODO tweak numbers and add sound effect
        
        CharacterController charCont = _player.GetComponent<CharacterController>();
        // launch charCont into the air
        charCont.Move(new Vector3(0, 5, 0));
            //Mathf.Lerp(_player.transform.position.y, _player.transform.position.y + 5, Time.deltaTime * 10);

            Collider[] hits = Physics.OverlapSphere(_player.transform.position, 5);

        foreach (Collider hit in hits)
        {
            if (hit.gameObject.CompareTag("Enemy"))
            {
                if (hit.gameObject.GetComponent<EnemyHit>() == null)
                {
                    hit.gameObject.GetComponent<BossHit>().EnemyHurt(10);
                }
                else
                {
                    hit.gameObject.GetComponent<EnemyHit>().EnemyHurt(10);
                }
                hit.gameObject.GetComponent<Rigidbody>().AddExplosionForce(10f, _player.transform.position, 5f);
            }
        }
    }
}
