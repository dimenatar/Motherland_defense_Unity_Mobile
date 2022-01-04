using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMove : MonoBehaviour
{
    //[SerializeField] Transform _targetPointToMove;
    public Transform _basePointToMove;
    public Transform currentPointToMove;
    public float _arrivalToPointRange = 0.5f;
    [SerializeField] private float _moveSpeed;
    public Hero hero;

    private void SetNewPoint(GameObject target)
    {
        
        if (target)
        {
            Debug.Log(target.gameObject.name);
            currentPointToMove = target.transform;
        }
        else
        {
            currentPointToMove = _basePointToMove;
        }
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
        
    }

    private IEnumerator MoveToPoint()
    {
        while (true)
        {
            if (CheckDistance(transform.position, currentPointToMove.position))
            {
                if (currentPointToMove.gameObject.GetComponent<Enemy>())
                {
                    hero.OnStartFight?.Invoke(currentPointToMove.gameObject);
                }
                else
                {
                    StopMove(null);
                }
            }
            else
            {
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, currentPointToMove.position, 1, 0.1f));
                Vector2 pos = new Vector2(transform.position.x, transform.position.z);
                pos = Vector2.MoveTowards(pos, new Vector2(currentPointToMove.position.x, currentPointToMove.position.z), _moveSpeed);
                transform.position = new Vector3(pos.x, transform.position.y, pos.y);
            }
            yield return new WaitForEndOfFrame();
        }
    }
    private bool CheckDistance(Vector3 currentPosition, Vector3 targetPosition)
    {
        return Vector3.Distance(currentPosition, targetPosition) <= _arrivalToPointRange;
    }
}
