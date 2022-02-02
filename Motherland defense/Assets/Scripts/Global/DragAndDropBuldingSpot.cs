using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDropBuldingSpot : MonoBehaviour
{
    [SerializeField] private TerrainCollider _terrainCollider;
    [SerializeField] private UserMoney _userMoney;
    [SerializeField] private TowerFactory _towerFactory;
    [SerializeField] private ViewPanel _viewPanel;
    [SerializeField] private Sprite _allowedPlace;
    [SerializeField] private Sprite _badPlace;
    [SerializeField] private GameObject _spotPrefab;
    [SerializeField] private int _totalAmount;
    [SerializeField] private Text _amountText;
    [SerializeField] private GameObject _stateImagePrefab;
    [SerializeField] private GameObject _circleBorder;
    [SerializeField] private ShowHideSlider _showHideSlider;

    private GameObject _stateImage;
    private GameObject _spot;
    private bool _isAllowedPlace;

    private void Start()
    {
        ChangeAmount(_totalAmount);
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonUp(0))
        {
            PlaceSpot();
        }
        else if (Input.GetMouseButtonDown(0) && IsHoweringObject(gameObject))
        {
            CreateSpot();
        }
        else if (Input.GetMouseButton(0))
        {
            DragSpot();
        }
    }

    public void CreateSpot()
    {
        if (_totalAmount > 0 && _showHideSlider.CheckAnimatorIdle())
        {
            _spot = Instantiate(_spotPrefab, GetPosition(), Quaternion.identity);
            _stateImage = Instantiate(_stateImagePrefab);
            _stateImage.transform.SetParent(_spot.transform);
            _stateImage.transform.localPosition = Vector3.zero + new Vector3(0,0.1f,0);
            _spot.GetComponent<TowerSpot>().Initialise(_towerFactory, _viewPanel);
            _spot.transform.SetParent(transform);
            _spot.AddComponent<TowerSpotCollisionChecker>();
            _spot.GetComponent<TowerSpotCollisionChecker>().OnCollisionWithBorder += ChangeStateToFalse;
            _spot.GetComponent<TowerSpotCollisionChecker>().OnNoCollision += ChangeStateToAllow;
            _stateImage.transform.SetParent(_spot.transform);
            ChangeStateToAllow();
            ChangeAmount(--_totalAmount);
        }
    }

    private void ChangeStateToFalse()
    {
        _stateImage.GetComponent<SpriteRenderer>().sprite = _badPlace;
        _isAllowedPlace = false;
    }

    private void ChangeStateToAllow()
    {
        _stateImage.GetComponent<SpriteRenderer>().sprite = _allowedPlace;
        _isAllowedPlace = true;
    }

    private void DragSpot()
    {
        if (_spot)
        {
            _spot.transform.position = GetPosition();
        }
    }

    private void PlaceSpot()
    {
        if (_spot)
        {
            Destroy(_spot.GetComponent<TowerSpotCollisionChecker>());
            Destroy(_stateImage);
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
        Destroy(_spot);
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

    private Vector3 GetPosition()
    {
        Vector3 worldPosition = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;

        if (_terrainCollider.Raycast(ray, out hitData, 4000))
        {
            worldPosition = hitData.point;
        }
        return new Vector3(worldPosition.x, hitData.point.y + 0.2f, worldPosition.z);
    }
}
