using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeSpawnZombie : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private EnemyFactory _enemyFactory;

    public void SpawnZombies()
    {
        GameObject zombie;
        for (int i = 0; i < _spawnPoints.Count; i++)
        {
            zombie = _enemyFactory.SpawnEnemy(CharacterList.Zombie);
            zombie.transform.position = _spawnPoints[i].position;
        }
    }
}
