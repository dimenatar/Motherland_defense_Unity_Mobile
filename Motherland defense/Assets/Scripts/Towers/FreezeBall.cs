using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FreezeBall : MonoBehaviour
{
    [SerializeField] private string _animationName;
    private Animator _animator;
    private float _freezeForce;
    private float _slowDownDuration;

    public void SetSlowDownDuration(float duration)
    {
        _slowDownDuration = duration;
    }

    public void Fire()
    {
        _animator.Play(_animationName, -1, 0);
    }

    public void SetFreezeForce(float force)
    {
        _freezeForce = force;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyMove>())
        {
            if (!other.GetComponent<Slower>())
            {
                other.gameObject.AddComponent<Slower>();
                other.gameObject.GetComponent<Slower>().SetSlowDownDuration(_slowDownDuration);
                other.gameObject.GetComponent<Slower>().SlowEnemy(_freezeForce);
            }
            else
            {
                other.GetComponent<Slower>().UpdateTimer();
            }
        }
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
}
