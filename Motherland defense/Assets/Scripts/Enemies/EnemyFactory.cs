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
    [SerializeField] private EnemyCounter _enemyCounter;
    [SerializeField] private LevelStatistics _levelStatistics;

    private EnemyCheckPoints _enemyCheckPoints;

    public void SpawnEnemy(CharacterList type)
    {
        GameObject enemy = Instantiate(_characterBundle.Characters.Where(t => t.Name == type).Select(prefab => prefab.Model).First(), _spawnPoint.position, transform.rotation);
        if (!enemy)
        {
            throw new FileNotFoundException();
        }
        enemy.transform.LookAt(_enemyCheckPoints.GetEnemyCheckPointByIndex(0).transform);
        enemy.GetComponent<Enemy>().Initialize(_characterBundle.Characters.Where(enemyName => enemyName.Name == type).First(), _enemyCheckPoints, _money, _viewPanel);
        _levelStatistics.AddEnemy();
        SubscribeEnemy(enemy.GetComponent<Enemy>());
    }

    private void Start()
    {
        _enemyCheckPoints = GetComponent<EnemyCheckPoints>();
    }

    private void SubscribeEnemy(Enemy enemy)
    {
        enemy.OnDied += _enemyCounter.ReduceEnemy;
        enemy.OnDamageTaken += _levelStatistics.AddTotalDamage;
    }
}
