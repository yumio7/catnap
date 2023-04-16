using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int enemyCap = 10;

    public int enemyCount = 0;

    public int enemiesKilled = 0;

    public int killsForBossSpawn = 2;

    [SerializeField] private GameObject[] boss;
    public int numBosses;

    public static EnemyManager Instance;
    
    private bool _spawned = false;
    // Start is called before the first frame update
    private void Start()
    {
        Instance = this;
        numBosses = boss.Length;
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(numBosses);
        if (enemiesKilled >= killsForBossSpawn && !_spawned)
        {
            Vector3 offset = new Vector3(0, 0, 0);
            foreach (GameObject b in boss)
            {
                Instantiate(b, transform.position + offset, transform.rotation);
                offset += new Vector3(1, 3, 1);
            }
            _spawned = true;
        }
    }
}
