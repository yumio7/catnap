using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private string bossName;
    [SerializeField] private GameObject bossHealthBarPrefab;

    private Slider healthBar;
    private TextMeshProUGUI bossTextGUI;
    private EnemyHit enemyHit;
    
    void Start()
    {
        enemyHit = GetComponent<EnemyHit>();
        
        var bossHealthBarInstance = Instantiate(bossHealthBarPrefab);
        
        healthBar = bossHealthBarInstance.GetComponentInChildren<Slider>();

        healthBar.maxValue = enemyHit.GetMaxHealth();

        bossTextGUI = bossHealthBarInstance.GetComponentInChildren<TextMeshProUGUI>();

        bossTextGUI.text = bossName;
    }

    void Update()
    {
        if (healthBar.maxValue == 0)
        {
            healthBar.maxValue = enemyHit.GetMaxHealth();
        }
        
        healthBar.value = enemyHit.GetCurrentHealth();
    }
}
