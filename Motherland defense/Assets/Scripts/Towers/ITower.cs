using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITower
{
    public IEnumerator Shoot();
    public void AddEnemyInArea(GameObject enemy);
    public void RemoveEnemyInArea(GameObject enemy);
}
