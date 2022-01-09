using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [Header("Set up ORK")]
    [SerializeField] private int _orkHealth;
    [SerializeField] private int _orkPoints;
    [SerializeField] private float _orkSpeed;
    [SerializeField] private int _orkDamage;
    [SerializeField] private float _orkPauseBetweenAttacks;
    [SerializeField] private float _orkAnimationSpeed;

    [SerializeField] private int _rabbitHealth;
    [SerializeField] private int _rabbitPoints;
    [SerializeField] private float _rabbitSpeed;
    [SerializeField] private int _rabbitDamage;
    [SerializeField] private float _rabbitPauseBetweenAttacks;
    [SerializeField] private float _rabbitAnimationSpeed;

    private EnemyCheckPoints _enemyCheckPoints;

    public void SpawnEnemy(EnemyList type)
    {
        switch (type)
        {
            case EnemyList.Ork:
                {
                    SpawnOrk();
                    break;
                }
        }
    }

    private void Start()
    {
        _enemyCheckPoints = GetComponent<EnemyCheckPoints>();
    }

    private void SpawnOrk()
    {
        var ork = LoadOrk();
        ork.transform.LookAt(_enemyCheckPoints.GetEnemyCheckPointByIndex(0).transform);
        ork.GetComponent<Enemy>().Initialize(EnemyList.Ork, _orkPoints, _orkHealth, _orkPauseBetweenAttacks, _orkDamage, _enemyCheckPoints, _orkSpeed, _orkAnimationSpeed);
    }

    private GameObject LoadOrk()
    {
        return Instantiate(Resources.Load<GameObject>("OrkPrefab"), _spawnPoint.position, transform.rotation);
    }
}
