using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewPanel : MonoBehaviour
{
    [SerializeField] public GameObject _mainCamera;
    [SerializeField] public GameObject _addCamera;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _firstCharacteristicText;
    [SerializeField] private Text _secondCharacteristicText;
    [SerializeField] private Text _thirdCharacteristicText;
    [SerializeField] private List<Image> _imageList;
    [SerializeField] private List<Sprite> _characterSprites;
    [SerializeField] private List<Sprite> _towerSprites;
    [SerializeField] private GameObject _exitFromFollowingCameraButton;
    [SerializeField] private ObjectDisabler _disabler;

    private GameObject _followedObject;
    private bool _isFollowing;

    public void HidePanel()
    {
        if (!_isFollowing)
        {
            _isFollowing = false;
            _addCamera.transform.parent = null;
            _panel.SetActive(false);
            _addCamera.SetActive(false);
            _mainCamera.SetActive(true);
        }
    }

    public void ShowPanel()
    {
        _panel.SetActive(true);
    }

    public void ShowCharacterPanel(string enemyName, string health, string damage, string points, GameObject sender)
    {
        _nameText.text = enemyName;
        _firstCharacteristicText.text = health;
        _secondCharacteristicText.text = damage;
        _thirdCharacteristicText.text = points;
        _followedObject = sender;
        for (int i = 0; i < _imageList.Count; i++)
        {
            _imageList[i].sprite = _characterSprites[i];
        }
        ShowPanel();
    }

    public void ShowTowerPanel(string towerName, string damage, string radius, string reloadTime)
    {
        _nameText.text = towerName;
        _firstCharacteristicText.text = damage;
        _secondCharacteristicText.text = radius;
        _thirdCharacteristicText.text = reloadTime;
        for (int i = 0; i < _imageList.Count; i++)
        {
            _imageList[i].sprite = _towerSprites[i];
        }
        ShowPanel();
    }

    public void StopFollowingObject()
    {
        _followedObject.GetComponent<Enemy>().OnDied -= StopFollowingObject;
        _exitFromFollowingCameraButton.SetActive(false);
        _isFollowing = false;
        _disabler.Enable();
        _followedObject = null;
        HidePanel();
    }

    public void SetCameraOnFollowedObject()
    {
        _followedObject.GetComponent<Enemy>().OnDied += StopFollowingObject;
        _isFollowing = true;
        _addCamera.SetActive(true);
        _addCamera.transform.SetParent(_followedObject.transform);
        _addCamera.transform.localPosition = new Vector3(0, 2, -2f);
        _addCamera.transform.rotation = _followedObject.transform.rotation;
        _mainCamera.SetActive(false);
        _exitFromFollowingCameraButton.SetActive(true);
        _disabler.Disable();
    }
}
