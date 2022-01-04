using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallExplode : MonoBehaviour
{
    private float _radius;
    public void Initialize(float radius)
    {
        _radius = radius;
    }

    private void HitEnemies()
    {
        var enemies = Physics.OverlapSphere(transform.position, _radius).ToList().Where(enemy => enemy.GetComponent<Enemy>() != null);
    }
}
