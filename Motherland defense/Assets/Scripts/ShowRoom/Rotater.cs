using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    private bool _isHolding = false;
    private Vector2 _startPosition;
    private Vector2 _endPosition;
    private Vector2 _difference;
    private Vector2 _saveForce;
    private float _rotateSpeed;
    private float _slowerRotationForce;
    private float _defaultRotateSpeed = 0.1f;

    private void FixedUpdate()
    {
        Rotate();
    }

    public void SetRotationSpeed(float speed, float slowerRotationForce, float defaultRotateSpeed)
    {
        _rotateSpeed = speed;
        _slowerRotationForce = slowerRotationForce;
        _defaultRotateSpeed = defaultRotateSpeed;
    }

    private void Rotate()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _saveForce = Vector2.zero;
            _isHolding = true;
            _startPosition = Input.mousePosition;
        }
        else if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            _startPosition = Input.mousePosition;
            if (_endPosition != Vector2.zero && _isHolding)
            {
                _difference = SetRotationSpeed();
                if (_difference != Vector2.zero)
                {
                    _saveForce = _difference;
                }
                transform.Rotate(0, _difference.x, 0);
            }
            _endPosition = _startPosition;
        }
        else
        {
            _saveForce = ManageRemainingForce(_saveForce);
            _difference = _saveForce;
            transform.Rotate(0, _difference.x, 0);
        }
        if (Input.touchCount == 1 && (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled))
        {
            _isHolding = false;
            _startPosition = Vector2.zero;
            _endPosition = Vector2.zero;
        }
    }

    private Vector2 ManageRemainingForce(Vector2 force)
    {
        if (CheckZeroLimit(force))
        {
            return SlowerForce(force);
        }
        else
        {
            return new Vector2(_defaultRotateSpeed, 0);
        }
    }


    private bool CheckZeroLimit(Vector2 force)
    {
        return (force*Mathf.Sign(force.x) - Mathf.Sign(force.x) * new Vector2(Time.deltaTime * _slowerRotationForce, 0)).x > 0;
    }

    private Vector2 SlowerForce(Vector2 force)
    {
        return force - Mathf.Sign(force.x) * new Vector2(Time.deltaTime * _slowerRotationForce, 0);
    }

    private Vector2 SetRotationSpeed()
    {
        return LimitRotationSpeed((_endPosition - _startPosition) * _rotateSpeed);
    }

    private Vector2 LimitRotationSpeed(Vector2 difference)
    {
        if (Mathf.Abs(difference.x) > 30)
        {
            return new Vector2(Mathf.Sign(difference.x) * 30, 0);
        }
        else
        {
            return difference;
        }
    }
}
