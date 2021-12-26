using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
{
    [Header("0 - Walk, 1 - Fight, 2 - Dead")]
    [SerializeField] private List<string> _enemyAnimationNames;
    [SerializeField] private float _animationSpeed;
    private Animator _enemyAnimator;
    private EnemyStates? _enemyState = null;
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _enemy.OnStartFight += StartFight;
        Debug.Log("+=");
        _enemy.OnStartMove += StartWalk;
        _enemy.OnDied += Dead;
        _enemyAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _enemyAnimator.SetFloat("Speed", _animationSpeed);
    }

    private void StartWalk()
    {
        ChangeState(EnemyStates.Walk);
    }

    private void StartFight()
    {
        ChangeState(EnemyStates.Fight);
    }

    private void Dead()
    {
        ChangeState(EnemyStates.Dead);
        _enemy.OnStartFight -= StartFight;
        _enemy.OnStartMove -= StartWalk;
        _enemy.OnDied -= Dead;
    }

    private void ChangeState(EnemyStates newState)
    {
        if (_enemyState == newState || _enemyState == EnemyStates.Dead)
        {
            return;
        }
        switch (newState)
        {
            case EnemyStates.Dead:
                {
                    _enemyAnimator.Play(_enemyAnimationNames[2]);
                    break;
                }
            case EnemyStates.Fight:
                {
                    _enemyAnimator.Play(_enemyAnimationNames[1]);
                    break;
                }
            case EnemyStates.Walk:
                {
                    _enemyAnimator.Play(_enemyAnimationNames[0]);
                    break;
                }
        }
        _enemyState = newState;
    }
}
