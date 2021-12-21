using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ting : MonoBehaviour, ITower
{
    private GameObject target = null;
    private List<GameObject> enemiesInArea = new List<GameObject>();
    public GameObject CreateTower()
    {
        return Resources.Load<GameObject>("TingPrefab");
    }

    public void Shoot()
    {
        if (target)
        {
            //todo
        }
    }

    public void AddEnemyInArea(GameObject enemy)
    {
        enemiesInArea.Add(enemy);
    }

    public void RemoveEnemyInArea(GameObject enemy)
    {
        enemiesInArea.Remove(enemy);
        if (target == enemy)
        {
            ChangeTarget();
        }
    }

    private void ChangeTarget()
    {
        if (enemiesInArea.Count > 0)
        {
            target = enemiesInArea[Random.Range(0, enemiesInArea.Count - 1)];
        }
    }
}
