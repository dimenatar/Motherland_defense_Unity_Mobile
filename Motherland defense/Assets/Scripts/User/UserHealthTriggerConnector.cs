using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserHealthTriggerConnector : MonoBehaviour
{
    [SerializeField] private int _startHealth;
    [SerializeField] private Health _health;
    [SerializeField] private EndTrigger _trigger;
    [SerializeField] private LosePanel _losePanel;
    [SerializeField] private ObjectDisabler _disabler; 

    public void ReduceHealth(Enemy enemy)
    {
        _health.ReduceHealth(enemy.GetPoints());
    }

    private void Start()
    {
        _health.Initialise(_startHealth);
        _trigger.OnEnemyPassed += ReduceHealth;
        _health.OnLose += _losePanel.ShowLosePanel;
        _health.OnLose += _disabler.DisableForever;
    }
}
