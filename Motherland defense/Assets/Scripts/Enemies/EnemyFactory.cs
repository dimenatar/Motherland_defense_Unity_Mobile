using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private UserMoney _money;
    [SerializeField] private CharacterBundle _characterBundle;
    [SerializeField] private ViewPanel _viewPanel;
    private EnemyCheckPoints _enemyCheckPoints;

    public void SpawnEnemy(CharacterList type)
    {
        switch (type)
        {
            case CharacterList.Ork:
                {
                    SpawnOrk();
                    break;
                }
            case CharacterList.Rabbit:
                {
                    SpawnRabbit();
                    break;
                }
            case CharacterList.Zombie:
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
        ork.GetComponent<Enemy>().Initialize(_characterBundle.Characters.Where(ork => ork.Name == CharacterList.Ork).First(), _enemyCheckPoints, _money, _viewPanel);
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
        rabbit.GetComponent<Enemy>().Initialize(_characterBundle.Characters.Where(rabbit => rabbit.Name == CharacterList.Rabbit).First(), _enemyCheckPoints, _money, _viewPanel);
    }

    private void SpawnZombie()
    {
        var zombie = Instantiate(Resources.Load<GameObject>("ZombiePrefab"), _spawnPoint.position, transform.rotation);
        if (!zombie)
        {
            throw new FileNotFoundException();
        }
        zombie.transform.LookAt(_enemyCheckPoints.GetEnemyCheckPointByIndex(0).transform);
        zombie.GetComponent<Enemy>().Initialize(_characterBundle.Characters.Where(zombie => zombie.Name == CharacterList.Zombie).First(), _enemyCheckPoints, _money, _viewPanel);
    }
}
