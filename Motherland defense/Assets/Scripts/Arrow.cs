using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Arrow : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _hitRange; //specify range where we hit enemy even if we dont actually
    private int _damage;
   
    private GameObject _target;
    private Rigidbody _rigidbody;
    private bool _isHit = false;

    private void Update()
    {
        MoveArrow();
        CheckDistance();
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void RemoveTarget()
    {
        _target = null;
        if (!_isHit)
        { 
            Destroy(gameObject);
        }
    }

    public void HitEnemy()
    {
        _target.GetComponent<Enemy>().TakeDamage(_damage);
        _isHit = true;
        Destroy(gameObject);
    }
    public void SetTarget(GameObject target, int damage)
    {
        _target = target;
        _damage = damage;
        _target.GetComponent<Enemy>().OnDied += RemoveTarget;
    }

    private void MoveArrow()
    {
        transform.LookAt(_target.transform.position);
        _rigidbody.velocity = transform.forward *_speed;
    }

    //this is pretty bad and heavy function 
    private void CheckDistance()
    {
        if (Vector3.Distance(_target.transform.position, transform.position) <= _hitRange)
        {
            HitEnemy();
        }
    }
}
