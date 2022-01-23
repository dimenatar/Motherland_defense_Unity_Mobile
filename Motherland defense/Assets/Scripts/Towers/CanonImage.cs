using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CanonImage : TowerImage, IClickable, ITowerLoader
{
    public override GameObject LoadTower()
    {
        GameObject canon = Instantiate(Resources.Load<GameObject>("Canon"));
        canon.name = "Canon";
        return canon;
    }
}
