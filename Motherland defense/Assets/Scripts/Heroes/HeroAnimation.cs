using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAnimation : MonoBehaviour
{
    [SerializeField] private float _animationSpeed;
    [Header("0 - Walk, 1 - Fight, 2 - Dead, 3 - Idle,")]
    [SerializeField] private List<string> _animationNames;
    private Animator _animator;
    private CharacterStates? _state = null;
    private Hero hero;

    private void Start()
    {
        hero = GetComponent<Hero>();
        hero.OnDied += Dead;
        hero.OnStartFight += StartFight;
        hero.OnBasePointReached += StartWait;
        GetComponent<HeroMove>().OnStartMove += StartWalk;
        _animator = GetComponent<Animator>();
    }

    private void StartWait()
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
        _animator.SetTrigger("Hit");
    }

    private void Dead()
    {
        ChangeState(CharacterStates.Dead);
    }

    private void ChangeState(CharacterStates newState)
    {
        if (_state == newState || _state == CharacterStates.Dead)
        {
            return;
        }

        switch (newState)
        {
            case CharacterStates.Walk:
                {
                    _animator.SetBool("IsIdle", false);
                    _animator.Play(_animationNames[0]);
                    break;
                }
            case CharacterStates.Fight:
                {
                    _animator.SetTrigger("Hit");
                    _animator.Play(_animationNames[1]);
                    break;
                }
            case CharacterStates.Dead:
                {
                    _animator.Play(_animationNames[2]);
                    break;
                }
            case CharacterStates.Idle:
                {
                    _animator.SetBool("IsIdle", true);
                    _animator.Play(_animationNames[3]);
                    break;
                }
        }
        _state = newState;
    }
}
