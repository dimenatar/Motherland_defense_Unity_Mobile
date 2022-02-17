using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class EnemyCheckPoint : MonoBehaviour
{
    [SerializeField] private int _checkPointOrderNumber;

    public int CheckPointOrderNumber => _checkPointOrderNumber;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyMove>())
        {
            other.GetComponent<EnemyMove>().ChangeNextCheckPoint(GetComponent<EnemyCheckPoint>());
        }
    }
}
