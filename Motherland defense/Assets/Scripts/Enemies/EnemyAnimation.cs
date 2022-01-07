using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
{
    [Header("0 - Walk, 1 - Fight, 2 - Dead, 3 - Idle,")]
    [SerializeField] private List<string> _enemyAnimationNames;
    [SerializeField] private float _animationSpeed;
    private Animator _enemyAnimator;
    private CharacterStates? _enemyState = null;
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _enemy.OnStartFight += StartFight;
        _enemy.OnStartMove += StartWalk;
        _enemy.OnDied += Dead;
        _enemy.OnFoundOpponent += FoundOpponent;
        GetComponent<EnemyFight>().OnHitOpponent += PlayFightAnimation;
        _enemyAnimator = GetComponent<Animator>();
        _enemyAnimator.SetFloat("Speed", _animationSpeed);
    }

    private void FoundOpponent()
    {
        ChangeState(CharacterStates.Idle);
    }

    private void StartWalk()
    {
        ChangeState(CharacterStates.Walk);
    }

    private void StartFight(GameObject opponent)
    {
        transform.LookAt(opponent.transform.position);
        ChangeState(CharacterStates.Fight);
    }

    private void PlayFightAnimation(Hero hero)
    {
        _enemyAnimator.SetTrigger("Hit");
    }

    private void Dead()
    {
        ChangeState(CharacterStates.Dead);
        _enemy.OnStartFight -= StartFight;
        _enemy.OnStartMove -= StartWalk;
        _enemy.OnDied -= Dead;
    }

    private void ChangeState(CharacterStates newState)
    {
        if (_enemyState == newState || _enemyState == CharacterStates.Dead)
        {
            return;
        }

        switch (newState)
        {
            case CharacterStates.Walk:
                {
                    _enemyAnimator.SetBool("IsIdle", false);
                    _enemyAnimator.Play(_enemyAnimationNames[0]);
                    break;
                }
            case CharacterStates.Fight:
                {
                    _enemyAnimator.SetTrigger("Hit");
                    _enemyAnimator.Play(_enemyAnimationNames[1]);
                    break;
                }
            case CharacterStates.Dead:
                {
                    _enemyAnimator.Play(_enemyAnimationNames[2]);
                    break;
                }
            case CharacterStates.Idle:
                {
                    _enemyAnimator.SetBool("IsIdle", true);
                    _enemyAnimator.Play(_enemyAnimationNames[3]);
                    break;
                }
        }
        _enemyState = newState;
    }
}
