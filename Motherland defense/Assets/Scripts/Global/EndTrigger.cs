using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    [SerializeField] private EnemyCounter _enemyCounter;

    public delegate void EnemyPassed(Enemy enemy);
    public event EnemyPassed OnEnemyPassed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            OnEnemyPassed?.Invoke(other.GetComponent<Enemy>());
            _enemyCounter.ReduceEnemy();
            Destroy(other.gameObject);
        }
    }
}
