using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    [Header("Place in right order")]
    [SerializeField] private List<CharacterList> _enemies;
    [Header("Seconds")]
    [SerializeField] private float _spawnPause;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private EnemyFactory _enemyFactory;

    public List<CharacterList> Enemies => _enemies;

    private int _currentEnemyIndex = -1;


    public void StartWave()
    {
        StartCoroutine(nameof(SpawnNextEnemy));
    }

    public void StopWave()
    {
        StopCoroutine(nameof(SpawnNextEnemy));
    }

    private IEnumerator SpawnNextEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnPause);
            if (_currentEnemyIndex >= _enemies.Count-1)
            {
                StopWave();
            }
            else
            {
                _currentEnemyIndex++;
                SpawnEnemy();
            }
            
        }
    }

    private void Start()
    {

    }

    private void SpawnEnemy()
    {
        if (_enemyFactory)
        {
            _enemyFactory.SpawnEnemy(_enemies[_currentEnemyIndex]);
        }
    }
}
