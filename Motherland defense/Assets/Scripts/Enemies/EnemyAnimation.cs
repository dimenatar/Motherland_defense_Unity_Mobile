using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
{
    //[Header("0 - Walk, 1 - Fight, 2 - Dead")]
    //[SerializeField] private List<AnimationClip> _enemyAnimations;
    private Animator _enemyAnimator;
    private EnemyStates _enemyState;
    private Enemy _enemy;
    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _enemy.OnStartFight += StartFight;
        _enemy.OnStartMove += StartWalk;
        _enemy.OnDied += Dead;
        _enemyAnimator = GetComponent<Animator>();
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
                    //_enemyAnimations[2].wrapMode = WrapMode.Once;
                    _enemyAnimator.Play("Death");
                    break;
                }
            case EnemyStates.Fight:
                {
                    _enemyAnimator.Play("Fight");
                    break;
                }
            case EnemyStates.Walk:
                {
                    _enemyAnimator.Play("Walk");
                    break;
                }
        }
        _enemyState = newState;
    }
}
