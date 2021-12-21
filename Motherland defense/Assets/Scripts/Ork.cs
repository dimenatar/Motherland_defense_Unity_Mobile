using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ork : MonoBehaviour, IEnemy
{
    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public void Move()
    {
        throw new System.NotImplementedException();
    }

    public void StartMove()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator IEnemyMove.Move()
    {
        throw new System.NotImplementedException();
    }
}
