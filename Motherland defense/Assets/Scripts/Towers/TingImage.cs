using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TingImage : TowerImage, IClickable, ITowerLoader
{
    public override GameObject LoadTower()
    {
        GameObject ting = Instantiate(Resources.Load<GameObject>("Ting"));
        ting.name = "Ting";
        return ting;
    }
}
