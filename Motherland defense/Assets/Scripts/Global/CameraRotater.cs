using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotater : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    private Vector2 _startPosition;
    private Vector2 _endPosition;
    private Vector2 _difference;
    private bool _isHolding;

    private void FixedUpdate()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _isHolding = true;
            _startPosition = Input.mousePosition;
        }
        else if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            _startPosition = Input.mousePosition;
            if (_endPosition != Vector2.zero && _isHolding)
            {
                _difference = SetRotationSpeed();
                //transform.Rotate(0, _difference.x, 0);
                transform.RotateAround(transform.parent.position, new Vector3(0, 1, 0), _difference.x);
            }
            _endPosition = _startPosition;
        }
        if (Input.touchCount == 1 && (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled))
        {
            _isHolding = false;
            _startPosition = Vector2.zero;
            _endPosition = Vector2.zero;
        }
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
