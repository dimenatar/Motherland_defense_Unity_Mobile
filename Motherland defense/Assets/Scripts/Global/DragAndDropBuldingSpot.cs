using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDropBuldingSpot : MonoBehaviour
{
    public event Action OnPlaceDropped;

    [SerializeField] private TerrainCollider _terrainCollider;
    [SerializeField] private UserMoney _userMoney;
    [SerializeField] private TowerFactory _towerFactory;
    [SerializeField] private ViewPanel _viewPanel;
    [SerializeField] private Sprite _allowedPlace;
    [SerializeField] private Sprite _badPlace;
    [SerializeField] private Sprite _defaultPlace;
    [SerializeField] private GameObject _spotPrefab;
    [SerializeField] private int _totalAmount;
    [SerializeField] private Text _amountText;
    [SerializeField] private GameObject _stateImagePrefab;
    [SerializeField] private GameObject _circleBorder;
    [SerializeField] private ShowHideSlider _showHideSlider;
    [SerializeField] private GameObject _spotImage;
    [SerializeField] private Camera _UICamera;
    [SerializeField] private CameraMove _cameraMove;
    [SerializeField] private GameObject _stateImage;

    private GameObject _spot;
    private bool _isAllowedPlace;

    private void Start()
    {
        ChangeAmount(_totalAmount);
    }

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetMouseButtonDown(0) && IsHoweringObject(_spotImage))
            {
                CreateSpot();
            }
            else if (Input.GetMouseButton(0))
            {
                DragSpot();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                PlaceSpot();
            }
        }
    }

    public void CreateSpot()
    {
        if (_totalAmount > 0 && _showHideSlider.CheckAnimatorIdle() && !_spot)
        {
            _cameraMove.StopMove();
            _spot = Instantiate(_spotPrefab, GetRayCoordinate(Vector3.zero), Quaternion.identity);
            _spot.GetComponent<TowerSpot>().Initialise(_towerFactory, _viewPanel, _userMoney, _terrainCollider);
            _spot.transform.SetParent(transform);
            _spot.AddComponent<TowerSpotCollisionChecker>();
            _spot.GetComponent<TowerSpotCollisionChecker>().OnCollisionWithBorder += ChangeStateToFalse;
            _spot.GetComponent<TowerSpotCollisionChecker>().OnNoCollision += ChangeStateToAllow;
            Canvas canvas = _spot.transform.Find("TowerMenu").GetComponent<Canvas>();
            canvas.worldCamera = _UICamera;
            ChangeStateToAllow();
            ChangeAmount(--_totalAmount);
        }
    }

    private void ChangeStateToFalse()
    {
        if (_stateImage)
        {
            _stateImage.GetComponent<Image>().sprite = _badPlace;
        }
        _isAllowedPlace = false;
    }

    private void ChangeStateToDefault()
    {
        if (_stateImage)
        {
            _stateImage.GetComponent<Image>().sprite = _defaultPlace;
        }
    }

    private void ChangeStateToAllow()
    {
        if (_stateImage)
        {
            _stateImage.GetComponent<Image>().sprite = _allowedPlace;
        }
        _isAllowedPlace = true;
    }

    private void DragSpot()
    {
        if (_spot)
        {
            _spot.transform.position = GetPosition(_spot);
        }
    }

    private void PlaceSpot()
    {
        if (_spot)
        {
            OnPlaceDropped?.Invoke();
            Destroy(_spot.GetComponent<TowerSpotCollisionChecker>());
            _spot.GetComponent<BoxCollider>().isTrigger = false;
            if (_isAllowedPlace)
            {
                AllowPlace();
            }
            else
            {
                CancelPlace();
            }
            _spot = null;
            _cameraMove.StartMove();
            ChangeStateToDefault();
        }
    }

    private void AllowPlace()
    {
        GameObject border = Instantiate(_circleBorder, _spot.transform);
        border.name = "Border";
        border.transform.localPosition = Vector3.zero; 
    }

    private void CancelPlace()
    {
        ChangeAmount(++_totalAmount);
        Debug.Log("destroy");
        _spot.SetActive(false);
    }

    private void ChangeAmount(int value)
    {
        _totalAmount = value;
        _amountText.text = _totalAmount.ToString();
    }

    private bool IsHoweringObject(GameObject gameObject)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        if (raycastResults.Count > 0)
        {
            foreach (var go in raycastResults)
            {
                if (go.gameObject == gameObject)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private Vector3 GetPosition(GameObject spot)
    {
        Vector3 highestPoint = GetHighestYPoint(new Vector3[] { GetRayCoordinate(Vector3.zero), GetRayCoordinate(new Vector3(spot.GetComponent<RectTransform>().localScale.x, 0, 0)), GetRayCoordinate(-new Vector3(spot.GetComponent<RectTransform>().localScale.x, 0, 0)), GetRayCoordinate(new Vector3(0, spot.GetComponent<RectTransform>().localScale.x, 0)), GetRayCoordinate(-new Vector3(0, spot.GetComponent<RectTransform>().localScale.x, 0)) });
        Vector3 inputPosition =  GetRayCoordinate(Vector3.zero);
        return new Vector3(inputPosition.x, highestPoint.y + 0.2f, inputPosition.z);
    }

    private Vector3 GetRayCoordinate(Vector3 modifier)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition + modifier*9);
        if (_terrainCollider.Raycast(ray, out RaycastHit hitData, 4000))
        {
            return hitData.point;
        }
        return Vector3.zero;
    }

    private Vector3 GetHighestYPoint(Vector3[] points)
    {
        Vector3 max = Vector3.zero;
        for (int i = 0; i < points.Length; i++)
        {
            Debug.DrawLine(points[i] + new Vector3(0,5,0), points[i], Color.red, 0.5f);
            if (points[i].y > max.y)
            {
                max = points[i];
            }
        }
        Debug.DrawLine(max + new Vector3(0, 5, 0), max, Color.blue, 0.5f);
        return max;
    }
}
