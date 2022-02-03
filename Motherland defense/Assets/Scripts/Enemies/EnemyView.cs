using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour, IClickable
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private GameObject _selectCircle;
    private bool _isSelected;
    private ViewPanel _viewPanel;
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _enemy.OnDamageTaken += SetNewHealthValue;
        _enemy.OnDied += Deselect;
        _healthBar.SetSliderMaxValue(_enemy.GetHealth());
    }

    public void Initialise(ViewPanel viewPanel)
    {
        _viewPanel = viewPanel;
    }

    private void SetNewHealthValue(int newHealth, int damage)
    {
        if (_isSelected)
        {
            _viewPanel.ShowCharacterPanel(_enemy.Data.Name.ToString(), newHealth.ToString(), _enemy.Data.Damage.ToString(), _enemy.Data.Points.ToString(), gameObject);
        }
        _healthBar.SetSliderValue(newHealth);
    }

    public void ObjectClick()
    {
        _selectCircle.SetActive(true);
        _viewPanel.ShowCharacterPanel(_enemy.Data.Name.ToString(), _enemy.GetHealth().ToString(), _enemy.Data.Damage.ToString(), _enemy.Data.Points.ToString(), gameObject);
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
}
