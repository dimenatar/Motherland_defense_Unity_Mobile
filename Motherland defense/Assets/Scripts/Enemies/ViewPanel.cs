using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _firstCharacteristicText;
    [SerializeField] private Text _secondCharacteristicText;
    [SerializeField] private Text _thirdCharacteristicText;
    [SerializeField] private List<Image> _imageList;
    [SerializeField] private List<Sprite> _characterSprites;
    [SerializeField] private List<Sprite> _towerSprites;
    
    public void HidePanel()
    {
        _panel.SetActive(false);
    }

    public void ShowPanel()
    {
        _panel.SetActive(true);
    }

    public void ShowCharacterPanel(string enemyName, string health, string damage, string points)
    {
        _nameText.text = enemyName;
        _firstCharacteristicText.text = health;
        _secondCharacteristicText.text = damage;
        _thirdCharacteristicText.text = points;
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
}
