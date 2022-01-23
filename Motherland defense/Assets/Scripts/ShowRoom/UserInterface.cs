using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private UIPanel _towerPanel; 
    [SerializeField] private UIPanel _characterPanel;
    [SerializeField] TowerBundle _towers;
    [SerializeField] CharacterBundle _characters;
    [SerializeField] ModelSpawner _spawner;
    [SerializeField] Button _nextButton;
    [SerializeField] Button _previousButton;
    [SerializeField] private Text _currentIndex;
    [SerializeField] private Text _listCount;

    private void Awake()
    {
        SetMaxValues();
        _spawner.OnTowerSpawned += SetTowerValues;
        _spawner.OnCharacterSpawned += SetCharacterValues;
        _spawner.OnSwitchedToCharacters += SwitchPanelToCharacters;
        _spawner.OnSwitchedToTowers += SwitchPanelToTowers;
        _spawner.OnIndexChanged += UpdateCurrentIndex;
    }

    private void Start()
    {
        _listCount.text = _towers.Towers.Count.ToString();
    }

    public void SetMaxValues()
    {
        SetTowerMaxValues();
        SetCharacterMaxValues();
    }

    private void SwitchPanelToTowers()
    {
        _towerPanel.gameObject.SetActive(true);
        _characterPanel.gameObject.SetActive(false);
        _listCount.text = _towers.Towers.Count.ToString();
    }

    private void SwitchPanelToCharacters()
    {
        _towerPanel.gameObject.SetActive(false);
        _characterPanel.gameObject.SetActive(true);
        _listCount.text = _characters.Characters.Count.ToString();
    }

    private void SetTowerValues(string radius, string damage, string cost, string name)
    {
        _towerPanel.SetValues(radius, damage, cost, name);
    }

    private void SetCharacterValues(string speed, string damage, string health, string name)
    {
        _characterPanel.SetValues(speed, damage, health, name);
    }

    private void SetCharacterMaxValues()
    {
        float maxSpeed = _characters.Characters.Select(speed => speed.Speed).Max();
        int maxCharacterDamage = _characters.Characters.Select(damage => damage.Damage).Max();
        int maxHealth = _characters.Characters.Select(health => health.Health).Max();
        _characterPanel.SetMaxValues(maxSpeed.ToString(), maxCharacterDamage.ToString(), maxHealth.ToString());
    }

    private void SetTowerMaxValues()
    {
        float maxRadius = _towers.Towers.Select(radius => radius.Radius).Max();
        int maxTowerDamage = _towers.Towers.Select(damage => damage.Damage).Max();
        int maxCost = _towers.Towers.Select(cost => cost.Cost).Max();
        _towerPanel.SetMaxValues(maxRadius.ToString(), maxTowerDamage.ToString(), maxCost.ToString());
    }

    private void UpdateCurrentIndex(int index)
    {
        _currentIndex.text = (index+1).ToString();
        if (int.Parse(_currentIndex.text) >= int.Parse(_listCount.text))
        {
            _nextButton.interactable = false;
        }
        else
        {
            _nextButton.interactable = true;
        }
        Debug.Log(int.Parse(_currentIndex.text) + " " + int.Parse(_listCount.text));
        if (int.Parse(_currentIndex.text) == 1)
        {
            _previousButton.interactable = false;
        }
        else
        {
            _previousButton.interactable = true;
        }
    }
}
