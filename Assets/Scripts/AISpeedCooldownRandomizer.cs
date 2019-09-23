using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpeedCooldownRandomizer : MonoBehaviour
{

    [SerializeField]
    float maxSpeedFactor;
    [SerializeField]
    float minSpeedFactor;

    [SerializeField]
    float maxCooldownFactor;
    [SerializeField]
    float minCooldownFactor;
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<EnemyAvatar>().MaxSpeed *= Random.Range(minSpeedFactor, maxSpeedFactor);
        float randomCooldown =  Random.Range(minCooldownFactor, maxCooldownFactor);
        foreach (Shooter shooter in GetComponentsInChildren<Shooter>())
        {
            shooter.Cooldown *= randomCooldown;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
