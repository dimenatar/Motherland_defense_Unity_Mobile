using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroView : MonoBehaviour, IClickable
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private GameObject _selectCircle;

    private bool _isSelected;
    private ViewPanel _viewPanel;
    private Hero _hero;



    private void Start()
    {
        _hero = GetComponent<Hero>();
        _hero.OnDamageTaken += SetNewHealthValue;
        _healthBar.SetSliderMaxValue(_hero.GetHealth());
    }

    public void Initialise(ViewPanel viewPanel)
    {
        _viewPanel = viewPanel;
    }

    public void ObjectClick()
    {
        _selectCircle.SetActive(true);
        _viewPanel.ShowCharacterPanel(_hero.HeroData.Name.ToString(), _hero.GetHealth().ToString(), _hero.HeroData.Damage.ToString(), _hero.HeroData.Points.ToString(), gameObject);
        _isSelected = true;
    }

    public void Deselect()
    {
        if (_isSelected)
        {
            _selectCircle.SetActive(false);
            _viewPanel.HidePanel();
            _isSelected = false;
        }
    }

    private void SetNewHealthValue(int newHealth, int damage)
    {
        if (_isSelected)
        {
            _viewPanel.ShowCharacterPanel(_hero.HeroData.Name.ToString(), newHealth.ToString(), _hero.HeroData.Damage.ToString(), _hero.HeroData.Points.ToString(), gameObject);
        }
        _healthBar.SetSliderValue(newHealth);
    }

}
