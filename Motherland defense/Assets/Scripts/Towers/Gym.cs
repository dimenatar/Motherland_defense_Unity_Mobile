using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gym : Tower
{
    [SerializeField] private List<GameObject> _heroes;
    public override IEnumerator Shoot()
    {
        yield return null;
    }

}
