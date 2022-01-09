using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyLoader
{
    public void Load(float _health);
    public void Load(Transform point);
}
