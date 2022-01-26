using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private bool _isHolding;
    private Vector2 _startPosition = Vector2.zero;
    private Vector2 _endPosition = Vector2.zero;
    private Vector2 _difference;

    private void Update()
    {
        if (Time.deltaTime != 0)
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
                    _difference = _endPosition - _startPosition;
                    transform.position += new Vector3(_difference.x, 0, _difference.y) / 10;
                }
                _endPosition = _startPosition;
            }
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                _isHolding = false;
                _startPosition = Vector2.zero;
                _endPosition = Vector2.zero;
            }
        }











        //if (Input.touchCount > 0)
        //{
        //    Debug.Log("1");
        //    Touch touch = Input.GetTouch(0);
        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        _startPosition = touch.position;
        //    }
        //    else if (touch.phase == TouchPhase.Moved)
        //    {
        //        _difference = touch.position - _startPosition;
        //        transform.position += new Vector3(_difference.x, _difference.y);
        //    }
        //}

    }

}
