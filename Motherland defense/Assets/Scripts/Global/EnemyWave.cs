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
    [SerializeField] private List<EnemyWave> _parallelWaves;
    public List<CharacterList> Enemies => _enemies;
    public float WavePause => _spawnPause * (_enemies.Count + 1);

    private int _currentEnemyIndex = -1;

    public void StartWave()
    {
        StartCoroutine(nameof(SpawnNextEnemy));
        foreach (EnemyWave wave in _parallelWaves)
        {
            wave.StartWave();
        }
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

    private void SpawnEnemy()
    {
        if (_enemyFactory)
        {
            _enemyFactory.SpawnEnemy(_enemies[_currentEnemyIndex], _spawnPoint);
        }
    }
}
