using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadTing : ITowerLoader
{
    public GameObject LoadTower()
    {
        return Resources.Load<GameObject>("TingPrefab"); 
    }
}
