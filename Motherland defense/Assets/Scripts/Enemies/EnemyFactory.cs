using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

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
    [Header("Set up Rabbit")]
    [SerializeField] private int _rabbitHealth;
    [SerializeField] private int _rabbitPoints;
    [SerializeField] private float _rabbitSpeed;
    [SerializeField] private int _rabbitDamage;
    [SerializeField] private float _rabbitPauseBetweenAttacks;
    [SerializeField] private float _rabbitAnimationSpeed;
    [Header("Set up Zombie")]
    [SerializeField] private int _zombieHealth;
    [SerializeField] private int _zombiePoints;
    [SerializeField] private float _zombieSpeed;
    [SerializeField] private int _zombieDamage;
    [SerializeField] private float _zombiePauseBetweenAttacks;
    [SerializeField] private float _zombieAnimationSpeed;

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
            case EnemyList.Rabbit:
                {
                    SpawnRabbit();
                    break;
                }
            case EnemyList.Zombie:
                {
                    SpawnZombie();
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

    private void SpawnRabbit()
    {
        var rabbit = Instantiate(Resources.Load<GameObject>("RabbitPrefab"), _spawnPoint.position, transform.rotation);
        if (!rabbit)
        {
            throw new FileNotFoundException();
        }
        rabbit.transform.LookAt(_enemyCheckPoints.GetEnemyCheckPointByIndex(0).transform);
        rabbit.GetComponent<Enemy>().Initialize(EnemyList.Rabbit, _rabbitPoints, _rabbitHealth, _rabbitPauseBetweenAttacks, _rabbitDamage, _enemyCheckPoints, _rabbitSpeed, _rabbitAnimationSpeed);
    }

    private void SpawnZombie()
    {
        var zombie = Instantiate(Resources.Load<GameObject>("ZombiePrefab"), _spawnPoint.position, transform.rotation);
        if (!zombie)
        {
            throw new FileNotFoundException();
        }
        zombie.transform.LookAt(_enemyCheckPoints.GetEnemyCheckPointByIndex(0).transform);
        zombie.GetComponent<Enemy>().Initialize(EnemyList.Zombie, _zombiePoints, _zombieHealth, _zombiePauseBetweenAttacks, _zombieDamage, _enemyCheckPoints, _zombieSpeed, _zombieAnimationSpeed);
    }
}
