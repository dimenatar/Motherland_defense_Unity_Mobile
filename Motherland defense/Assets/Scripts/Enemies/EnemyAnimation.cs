using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
{
    private float _animationSpeed;
    private Animator _enemyAnimator;
    private CharacterStates? _enemyState = null;
    private Enemy _enemy;

    private readonly string _walkAnimation = "Walk";
    private readonly string _idleAnimation = "Idle";
    private readonly string _fightAnimation = "Fight";
    private readonly string _deadAnimation = "Death";

    public void InitialiseAnimation(float animationSpeed)
    {
        _animationSpeed = animationSpeed;
        _enemyAnimator.SetFloat("Speed", _animationSpeed);
    }

    private void Awake()
    {
        _enemyAnimator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
        GetComponent<EnemyMove>().OnStop += StopMove;
        _enemy.OnStartFight += StartFight;
        _enemy.GetComponent<EnemyMove>().OnStartMove += StartWalk;
        _enemy.OnDied += Dead;
        _enemy.OnFoundOpponent += StopMove;
        GetComponent<EnemyFight>().OnHitOpponent += PlayFightAnimation;
    }

    private void StopMove()
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
                    _enemyAnimator.Play(_walkAnimation);
                    break;
                }
            case CharacterStates.Fight:
                {
                    _enemyAnimator.SetTrigger("Hit");
                    //_enemyAnimator.Play(_fightAnimation);
                    break;
                }
            case CharacterStates.Dead:
                {
                    _enemyAnimator.Play(_deadAnimation);
                    break;
                }
            case CharacterStates.Idle:
                {
                    _enemyAnimator.SetBool("IsIdle", true);
                    _enemyAnimator.Play(_idleAnimation);
                    break;
                }
        }
        _enemyState = newState;
    }
}
