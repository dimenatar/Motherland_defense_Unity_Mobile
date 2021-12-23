using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    [Header("Place in right order")]
    [SerializeField] private List<GameObject> _enemies;
    [Header("Seconds")]
    [SerializeField] private float _spawnPause;
    [SerializeField] private Transform _spawnPoint;

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
        if (_currentEnemyIndex >= _enemies.Count)
        {
            StopWave();
        }
        else
        {
            _currentEnemyIndex++;
            SpawnEnemy();
        }
        yield return new WaitForSeconds(_spawnPause);
    }

    private void SpawnEnemy()
    {
        GameObject enemy = _enemies[_currentEnemyIndex];
        enemy.transform.position =_spawnPoint.position;
        enemy.transform.rotation = _spawnPoint.rotation;
        enemy.SetActive(true);
    }

}
