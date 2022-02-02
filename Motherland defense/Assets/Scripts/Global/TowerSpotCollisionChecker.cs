using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpotCollisionChecker : MonoBehaviour
{
    public event Action OnCollisionWithBorder;
    public event Action OnNoCollision;

    private List<GameObject> _collisions = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Border")
        {
            AddCollision(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Border")
        {
            ReduceCollision(other.gameObject);
        }
    }

    private void AddCollision(GameObject collision)
    {
        _collisions.Add(collision);
        OnCollisionWithBorder?.Invoke();
    }

    private void ReduceCollision(GameObject collision)
    {
        _collisions.Remove(collision);
        if (_collisions.Count == 0)
        {
            OnNoCollision?.Invoke();
        }
    }
}
