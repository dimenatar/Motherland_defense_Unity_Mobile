using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    [SerializeField] private GameObject _limitBox;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _scaleForce;
    [SerializeField] private float _maxHeight;
    [SerializeField] private float _minHeight;

    private Touch _touch1;
    private Touch _touch2;
    private Vector2 _startDifferense = Vector2.zero;
    private Vector2 _difference;

    private void FixedUpdate()
    {
        if (Input.touchCount == 2)
        {
            if (Input.GetTouch(0).position.x < Input.GetTouch(1).position.x)
            {
                _touch1 = Input.GetTouch(1);
                _touch2 = Input.GetTouch(0);
            }
            else
            {
                _touch1 = Input.GetTouch(0);
                _touch2 = Input.GetTouch(1);
            }
            if (_startDifferense != Vector2.zero)
            {
                _difference = _touch2.position - _touch1.position - _startDifferense;
                if (CheckLimits())
                {
                    _limitBox.transform.localScale -= new Vector3(_difference.x, 0, _difference.x) * _scaleForce;
                    _camera.localPosition += new Vector3(0, _difference.x * _scaleForce, 0);
                }
            }
            _startDifferense = _touch2.position - _touch1.position;
        }
        else
        {
            _startDifferense = Vector2.zero;
        }
    }

    private bool CheckLimits()
    {
        if (_difference.x > 0 && _camera.localPosition.y + _difference.x * _scaleForce < _maxHeight)
        {
            return true;
        }
        else if (_difference.x <= 0 && _camera.localPosition.y - _difference.x * _scaleForce > _minHeight)
        {
            return true;
        }
        return false;
    }
}
