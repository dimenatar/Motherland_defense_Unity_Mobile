using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    // change type later
    [SerializeField] private GameObject _healthBar;

    public Enemy ThisEnemy;

    private void Start()
    {
        ThisEnemy.OnDamageTaken += SetNewHealthValue;
    }

    private void OnDestroy()
    {
        ThisEnemy.OnDamageTaken -= SetNewHealthValue;
    }

    private void SetNewHealthValue(int value)
    {
        
    }
}
