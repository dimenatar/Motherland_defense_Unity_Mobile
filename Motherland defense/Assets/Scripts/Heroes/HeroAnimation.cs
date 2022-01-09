using UnityEngine;

public class HeroAnimation : MonoBehaviour
{
    private Animator _animator;
    private CharacterStates? _state = null;
    private Hero hero;

    private readonly string _walkAnimation = "Walk";
    private readonly string _idleAnimation = "Idle";
    private readonly string _fightAnimation = "Fight";
    private readonly string _deadAnimation = "Dead";

    private void Start()
    {
        hero = GetComponent<Hero>();
        hero.OnDied += Dead;
        hero.OnStartFight += StartFight;
        hero.OnBasePointReached += StartWait;
        GetComponent<HeroMove>().OnStartMove += StartWalk;
        GetComponent<HeroFight>().OnHitEnemy += PlayFightAnimation;
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

    private void PlayFightAnimation(Enemy enemy)
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
                    _animator.Play(_walkAnimation);
                    break;
                }
            case CharacterStates.Fight:
                {
                    _animator.SetTrigger("Hit");
                    //_animator.Play(_fightAnimation);
                    break;
                }
            case CharacterStates.Dead:
                {
                    _animator.Play(_deadAnimation);
                    break;
                }
            case CharacterStates.Idle:
                {
                    _animator.SetBool("IsIdle", true);
                    _animator.Play(_idleAnimation);
                    break;
                }
        }
        _state = newState;
    }
}
