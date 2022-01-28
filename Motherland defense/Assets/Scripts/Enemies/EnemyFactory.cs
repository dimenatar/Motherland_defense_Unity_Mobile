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
    [SerializeField] private int _startCheckPointIndex;
    private EnemyCheckPoints _enemyCheckPoints;

    public EnemyCounter Counter => _enemyCounter;

    public GameObject SpawnEnemy(CharacterList type)
    {
        GameObject enemy = Instantiate(_characterBundle.Characters.Where(t => t.Name == type).Select(prefab => prefab.Model).First(), _spawnPoint.position, transform.rotation);
        if (!enemy)
        {
            throw new FileNotFoundException();
        }
        enemy.transform.LookAt(_enemyCheckPoints.GetEnemyCheckPointByIndex(0).transform);
        enemy.transform.SetParent(transform);
        enemy.GetComponent<Enemy>().Initialize(_characterBundle.Characters.Where(enemyName => enemyName.Name == type).First(), _enemyCheckPoints, _enemyCheckPoints.GetEnemyCheckPointByIndex(_startCheckPointIndex), _money, _viewPanel);
        _levelStatistics.AddEnemy();
        SubscribeEnemy(enemy.GetComponent<Enemy>());
        return enemy;
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
