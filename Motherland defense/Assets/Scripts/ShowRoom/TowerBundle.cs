using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Bundle", menuName = "Tower Bundle", order = 11)]
public class TowerBundle : ScriptableObject
{
    [SerializeField] private List<TowerData> _towers;

    public List<TowerData> Towers => _towers;
}
