using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private List<GameObject> _hintList;
    [SerializeField] private GameObject _upperPanel;
    [SerializeField] private GameObject _slidingPanel;
    [SerializeField] private DragAndDropBuldingSpot _dragAndDropBuldingSpot;
    [SerializeField] private GameObject _startWaveIcon;
    [SerializeField] private GameObject _speedButton;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private ClickManager _clickManager;

    private int index = 0;
    private bool _isSliderClicked = false;
    private bool _isPlaceDropped = false;

    public void LoadNextHint()
    {
        if (index != 0)
        {
            _hintList[index-1].SetActive(false);
        }
        _hintList[index].SetActive(true);
        EnableAdditionalObjects(index);
        index++;
        if (index == _hintList.Count)
        {
            CompleteTutorial();
        }
    }

    public void OpenSliderMenu()
    {
        if (!_isSliderClicked)
        {
            LoadNextHint();
            _isSliderClicked = true;
        }
    }

    public void CompleteTutorial()
    {
        _dragAndDropBuldingSpot.OnPlaceDropped -= PlaceDroped;
        DisableEveryHint();
        EnableEveryComponent(_upperPanel, _slidingPanel, _startWaveIcon, _speedButton, _pauseButton);
        SaveProgress();
        Destroy(this);
    }

    private void EnableEveryComponent(params GameObject[] objects)
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(true);
        }
        _clickManager.enabled = true;
    }

    private void DisableEveryHint()
    {
        _hintList.ForEach(hint => hint.SetActive(false));
    }

    private void SaveProgress()
    {
        UserData data = UserProgressManager.LoadUserData(UserProgressManager.Path);
        data.CompleteTutorial();
        UserProgressManager.SaveUserData(UserProgressManager.Path, data);
    }

    private void Start()
    {
        _dragAndDropBuldingSpot.OnPlaceDropped += PlaceDroped;
        UserData data = UserProgressManager.LoadUserData(UserProgressManager.Path);
        if (!data.IsCompletedTutorial)
        {
            LoadNextHint();
        }
        else
        {
            CompleteTutorial();
        }
    }

    private void PlaceDroped()
    {
        if (!_isPlaceDropped)
        {
            LoadNextHint();
            _isPlaceDropped = true;
        }
    }

    private void DoAnimation(GameObject obj)
    {

    }

    private void EnableAdditionalObjects(int index)
    {
        switch (index)
        {
            case 1:
                {
                    _upperPanel.SetActive(true);
                    break;
                }
            case 2:
                {
                    _slidingPanel.SetActive(true);
                    break;
                }
            case 3:
                {
                    _isSliderClicked = true;
                    break;
                }
            case 4:
                {
                    _isPlaceDropped = true;
                    break;
                }
            case 7:
                {
                    _startWaveIcon.SetActive(true);
                    break;
                }
        }
    }
}
