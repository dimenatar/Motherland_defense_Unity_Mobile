using System.Collections;
using System;
using UnityEngine;

public class HeroMove : MonoBehaviour
{
    //[SerializeField] Transform _targetPointToMove;
    public event Action OnStartMove;
    public Transform _basePointToMove;
    public Transform currentPointToMove;
    public float _arrivalToPointRange = 0.5f;
    [SerializeField] private float _moveSpeed;
    public Hero hero;

    private void SetNewPoint(GameObject target)
    {
        Debug.Log("set new point");
        if (target != null)
        {
           
            if (target.GetComponent<Enemy>())
            {
                target.GetComponent<Enemy>().FoundOpponent();
                currentPointToMove = target.transform;
            }
            else
            {
                Debug.Log(target.name);
                throw new FormatException();
            }
        }
        else
        {
            currentPointToMove = _basePointToMove;
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

    private void Start()
    {
        SetNewPoint(null);
        hero = GetComponent<Hero>();
        hero.OnTargetChanged += SetNewPoint;
        hero.OnStartFight += StopMove;
        OnStartMove += StartMove;
    }        


    private IEnumerator MoveToPoint()
    {
        while (true)
        {
            if (CheckDistance(transform.position, currentPointToMove.position))
            {
                if (currentPointToMove.gameObject.GetComponent<Enemy>())
                {
                    Debug.Log("Invoke");
                    currentPointToMove.gameObject.GetComponent<Enemy>().StartFightWith(gameObject);
                    hero.OnStartFight?.Invoke(currentPointToMove.gameObject);
                }
                else
                {
                    GetComponent<Hero>().ReachBasePoint();
                }
                StopMove(null);
            }
            else
            {
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, currentPointToMove.position, 1, 5f));
                Vector2 pos = new Vector2(transform.position.x, transform.position.z);
                pos = Vector2.MoveTowards(pos, new Vector2(currentPointToMove.position.x, currentPointToMove.position.z), _moveSpeed);
                transform.position = new Vector3(pos.x, transform.position.y, pos.y);
            }
            yield return new WaitForFixedUpdate();
        }
    }
    private bool CheckDistance(Vector3 currentPosition, Vector3 targetPosition)
    {
        return Vector3.Distance(currentPosition, targetPosition) <= _arrivalToPointRange;
    }
}
