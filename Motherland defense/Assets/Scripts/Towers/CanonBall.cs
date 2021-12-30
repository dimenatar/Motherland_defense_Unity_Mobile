using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CanonBall : MonoBehaviour
{
    [SerializeField] private float _timeToFlyUp;
    [SerializeField] private float _fireForce;
    [SerializeField] private float _speed;
    [SerializeField] private float _flyHeight;
    private float _animation;
    private Rigidbody _rigidbody;
    private GameObject _target;
    private Vector3 _fallPoint;

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

    public void SetToStartPoint(Vector3 point)
    {
        transform.position = point;
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    private void Update()
    {
       //  MoveBall();
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
        if (LayerMask.LayerToName(other.gameObject.layer) == "Ground")
        {
            gameObject.SetActive(false);
        }
    }

    private void Explode()
    {
        IsReadyToFire = true;
    }
}
