using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class GymImage : TowerImage, IClickable, ITowerLoader
{
    public  override GameObject LoadTower()
    {
        GameObject gym = Instantiate(Resources.Load<GameObject>("Gym"));
        gym.name = "Gym";
        return gym;
    }
}
