using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{

    public void CreateTower(ITower _tower, Vector2 position, DirectionEnum.Directions direction)
    {
        GameObject tower = _tower.CreateTower();
        SetTowerPosition(tower, position, direction);
    }
    public void SetTowerPosition(GameObject towerObject, Vector2 position, DirectionEnum.Directions direction)
    {
        towerObject.transform.position = position;
        float angle = 0;
        DirectionDictionaryValues.DirectionValues.TryGetValue(direction, out angle);
        towerObject.transform.rotation = Quaternion.Euler(0, angle, 0);
    }
}
