using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CanonBall : MonoBehaviour
{
    [SerializeField] private float _timeToFlyUp;
    [SerializeField] private float _fireForce;
    [SerializeField] private float _speed;
    [SerializeField] private float _flyHeight;
    [SerializeField] private float _hitRadius;
    [SerializeField] private float _damage;

    private float _animation;
    private Rigidbody _rigidbody;
    private GameObject _target;
    private AudioSource _source;

    public bool IsReadyToFire { get; private set; } = true;

    public void FireBall()
    {
        _animation = 0;
        StopCoroutine(nameof(MoveBall));
        IsReadyToFire = false;
        if (!_rigidbody)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
        }
        _rigidbody.AddForce(Vector3.up* _fireForce, ForceMode.Impulse);
        StartCoroutine(nameof(MoveBall));
    }
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void ReturnToStartPoint(Vector3 point)
    {
        transform.position = point;
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    private void RemoveTarget()
    {
        _target = null;
    }

    IEnumerator MoveBall()
    {
        float mn = 0;
        yield return new WaitForSeconds(_timeToFlyUp);
        while (true)
        {
            mn += Time.deltaTime*2.5f;
            _animation += Time.deltaTime;
            _animation %= 4f;
            transform.position = MathParabola.Parabola(transform.position, _target.transform.position, _flyHeight, _animation/4f*mn);
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Ground" || other.GetComponent<Enemy>())
        {
            Explode();
            gameObject.SetActive(false);
        }
    }

    private void Explode()
    {
        RemoveTarget();
        IsReadyToFire = true;
        CreateExplosion();
        HitEnemies(FindEnemies());
    }

    private List<Collider> FindEnemies()
    {
        return Physics.OverlapSphere(transform.position, _hitRadius).ToList().Where(enemy => enemy.GetComponent<Enemy>() != null).ToList();
    }

    private void HitEnemies(List<Collider> enemies)
    {
        Vector3 explosionPosition = transform.position;
        float proximity, effect;
        foreach (Collider enemy in enemies)
        {
            proximity = (explosionPosition - enemy.transform.position).magnitude;
            effect = 1 - (proximity / _hitRadius);
            enemy.GetComponent<Enemy>().TakeDamage(Mathf.RoundToInt(effect*_damage));
        }
    }

    private void CreateExplosion()
    {
        GameObject explosion = Resources.Load<GameObject>("CanonBallExplosion");
        if (explosion)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }
}
