using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private float _hitRange; //specify range where we hit enemy even if we dont actually
    private GameObject _target;

    private void Start()
    {

    }


    private void Update()
    {
        MoveArrow();
        CheckDistance();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Enemy collidedEnemy = collision.collider.GetComponent<Enemy>();
        //if (collidedEnemy)
        //{
        //    collidedEnemy.TakeDamage(_damage);
        //}
        //Destroy(gameObject);
    }
    public void HitEnemy()
    {
        _target.GetComponent<Enemy>().TakeDamage(_damage);
        Destroy(this.gameObject);
    }
    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    public float GetSpeed()
    {
        return (transform.forward * _speed).magnitude;
    }

    private void MoveArrow()
    {
        transform.position += transform.forward * _speed;
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, Time.deltaTime * _speed);
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
