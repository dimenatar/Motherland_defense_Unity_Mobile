using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    public GameObject CreateTower(ITowerLoader towerLoader, Vector3 position, DirectionEnum.Directions direction)
    {
        GameObject tower = towerLoader.LoadTower();
        SetTowerPosition(tower, position, direction);
        return tower;
    }

    public void SetTowerPosition(GameObject towerObject, Vector3 position, DirectionEnum.Directions direction)
    {
        DirectionDictionaryValues.DirectionValues.TryGetValue(direction, out float angle);
        towerObject.transform.SetPositionAndRotation(position, Quaternion.Euler(0, angle, 0));
    }
}
