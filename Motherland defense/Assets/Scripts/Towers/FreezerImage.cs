using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FreezerImage : TowerImage, IClickable, ITowerLoader
{
    public override GameObject LoadTower()
    {
        GameObject freezer = Instantiate(Resources.Load<GameObject>("Freezer"));
        freezer.name = "Freezer";
        return freezer;
    }
}
