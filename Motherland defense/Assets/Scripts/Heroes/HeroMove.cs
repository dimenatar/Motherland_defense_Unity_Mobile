using System.Collections;
using System;
using UnityEngine;

public class HeroMove : MonoBehaviour
{
    public event Action OnStartMove;

    private Transform _basePointToMove;
    public Transform _currentPointToMove;
    public float _arrivalToPointRange;
    private float _moveSpeed;
    private Hero hero;

    public void Initialise(Transform basePointToMove, float moveSpeed, float arrivalToPointRange)
    {
        _basePointToMove = basePointToMove;
        _moveSpeed = moveSpeed;
        _arrivalToPointRange = arrivalToPointRange;

        hero = GetComponent<Hero>();
        hero.OnTargetChanged += SetNewPoint;
        hero.OnStartFight += StopMove;
        hero.OnDied += StopMove;
        OnStartMove += StartMove;
        SetNewPoint(null);
    }


    private void SetNewPoint(GameObject target)
    {
        if (target != null)
        {
            if (target.GetComponent<Enemy>())
            {
                Debug.Log("enemy found opponent");
                target.GetComponent<Enemy>().FoundOpponent();
                _currentPointToMove = target.transform;
            }
            else
            {
                Debug.Log(target.name);
                throw new FormatException();
            }
        }
        else
        {
            _currentPointToMove = _basePointToMove;
        }
        OnStartMove?.Invoke();
    }

    public void StartMove()
    {
        StartCoroutine(nameof(MoveToPoint));
    }

    public void StopMove(GameObject target)
    {
        StopCoroutine(nameof(MoveToPoint));
    }

    public void StopMove()
    {
        StopCoroutine(nameof(MoveToPoint));
    }   

    private IEnumerator MoveToPoint()
    {
        while (true)
        {
            if (CheckDistance(transform.position, _currentPointToMove.position))
            {
                if (_currentPointToMove.gameObject.GetComponent<Enemy>())
                {
                    _currentPointToMove.gameObject.GetComponent<Enemy>().StartFightWith(gameObject);
                    hero.OnStartFight?.Invoke(_currentPointToMove.gameObject);
                }
                else
                {
                    GetComponent<Hero>().ReachBasePoint();
                }
                StopMove(null);
            }
            else
            {
                transform.LookAt(_currentPointToMove);
                Vector2 pos = new Vector2(transform.position.x, transform.position.z);
                pos = Vector2.MoveTowards(pos, new Vector2(_currentPointToMove.position.x, _currentPointToMove.position.z), _moveSpeed);
                transform.position = new Vector3(pos.x, transform.position.y, pos.y);
            }
            yield return new WaitForFixedUpdate();
        }
    }
    private bool CheckDistance(Vector3 currentPosition, Vector3 targetPosition)
    {
        return Vector2.Distance(new Vector2(currentPosition.x, currentPosition.z), new Vector2(targetPosition.x, targetPosition.z)) <= _arrivalToPointRange;
    }
}
