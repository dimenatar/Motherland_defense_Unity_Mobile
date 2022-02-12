using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeSpawnZombie : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private int _maxAmount;
    [SerializeField] private float _zombieSpawnDelay;

    private EnemyFactory _enemyFactory;
    private int _spawnedAmount;

    public void SpawnZombies()
    {
        if (_spawnedAmount < _maxAmount)
        {
            if (_enemyFactory)
            {
                GameObject zombie;
                for (int i = 0; i < _spawnPoints.Count; i++)
                {
                    zombie = _enemyFactory.SpawnEnemy(CharacterList.Zombie, _spawnPoints[i]);
                    zombie.transform.position = _spawnPoints[i].position;
                    zombie.GetComponent<EnemyMove>().ChangeNextCheckPoint(GetComponent<EnemyMove>().TargetCheckPointIndex);
                    _spawnedAmount++;
                }
                _enemyFactory.Counter.AddEnemies(_spawnPoints.Count);
            }
        }
    }

    private void Start()
    {
        if (transform.parent)
        {
            _enemyFactory = transform.parent.GetComponent<EnemyFactory>();
            StartCoroutine(nameof(Spawn));
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(nameof(Spawn));
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(_zombieSpawnDelay);
            SpawnZombies();
        }
    }
}
