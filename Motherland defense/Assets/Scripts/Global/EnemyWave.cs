using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    public delegate void StartedWave(EnemyWave enemyWave);
    public event StartedWave OnStartedWave;

    [Header("Place in right order")]
    [SerializeField] private List<CharacterList> _enemies;
    [Header("Seconds")]
    [SerializeField] private float _spawnPause;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private List<EnemyWave> _parallelWaves;
    [SerializeField] private float _wavePause;
    [SerializeField] private WaveIcon _waveIcon;

    private int _currentEnemyIndex = -1;

    public List<CharacterList> Enemies => _enemies;

    public float WavePause => _wavePause;

    public void StartWave()
    {
        _waveIcon.StopFilling();
        _waveIcon.gameObject.SetActive(false);
        OnStartedWave?.Invoke(this);
        StartCoroutine(nameof(SpawnNextEnemy));
        foreach (EnemyWave wave in _parallelWaves)
        {
            wave.StartWave();
        }
    }

    public int GetEnemyCount()
    {
        int counter = 0;
       _parallelWaves.ForEach(wave => counter += wave.Enemies.Count);
        return counter + _enemies.Count;
    }

    public void StopWave()
    {
        StopCoroutine(nameof(SpawnNextEnemy));
    }

    public void EnableIcon(float seconds)
    {
        _waveIcon.gameObject.SetActive(true);
        _waveIcon.StartFilling(seconds);
        foreach (EnemyWave wave in _parallelWaves)
        {
            wave.EnableIcon(seconds);
        }
    }

    //public void EnableIcon(float seconds)
    //{
    //    _waveIcon.gameObject.SetActive(true);
    //    _waveIcon.StartFilling(seconds);
    //}

    private IEnumerator SpawnNextEnemy()
    {
        while (true)
        {
            if (_currentEnemyIndex >= _enemies.Count-1)
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
    }

    private void SpawnEnemy()
    {
        if (_enemyFactory)
        {
            _enemyFactory.SpawnEnemy(_enemies[_currentEnemyIndex], _spawnPoint);
        }
    }
}
