using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModelSpawner : MonoBehaviour
{
    [SerializeField] private CharacterBundle _characters; 
    [SerializeField] private TowerBundle _towers;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _rotationSlowerForce;
    [SerializeField] private float _modelScale;

    public delegate void ModelSpawned(int characteristic1, int characteristic2, int characteristic3, string name);
    public event ModelSpawned OnTowerSpawned;
    public event ModelSpawned OnCharacterSpawned;

    public delegate void IndexChanged(int index);
    public event IndexChanged OnIndexChanged;

    public event Action OnSwitchedToTowers;
    public event Action OnSwitchedToCharacters;

    private GameObject _currentModel;
    private bool _isCurrentTower = true;
    private int index = 0;

    public void LoadModel()
    {
        UnloadCurrentModel();
        if (_isCurrentTower)
        {
            LoadTower();
        }
        else
        {
            LoadCharacter();
        }
        _currentModel.transform.localScale *= _modelScale;
        _currentModel.AddComponent<Rotater>().SetRotationSpeed(_rotationSpeed, _rotationSlowerForce);
    }

    public void SwitchToTowers()
    {
        UpdateIndex(0);
        _isCurrentTower = true;
        LoadModel();
        OnSwitchedToTowers?.Invoke();
    }

    public void SwitchToCharacters()
    {
        UpdateIndex(0);
        _isCurrentTower = false;
        LoadModel();
        OnSwitchedToCharacters?.Invoke();
    }

    public void LoadNextModel()
    {
        UpdateIndex(++index);
        LoadModel();
    }

    public void LoadPreviousModel()
    {
        UpdateIndex(--index);
        LoadModel();
    }

    private void Start()
    {
        LoadModel();
    }

    private void LoadTower()
    {
        _currentModel = Instantiate(_towers.Towers[index].Model, _spawnPoint.position, Quaternion.Euler(0, 0, 0));
        OnTowerSpawned.Invoke(_towers.Towers[index].Radius, _towers.Towers[index].Damage, _towers.Towers[index].Cost, _towers.Towers[index].Name);
    }

    private void LoadCharacter()
    {
        _currentModel = Instantiate(_characters.Characters[index].Model, _spawnPoint.position, Quaternion.Euler(0, 0, 0));
        OnCharacterSpawned?.Invoke(_characters.Characters[index].Speed, _characters.Characters[index].Damage, _characters.Characters[index].Speed, _characters.Characters[index].Name);
    }

    private void UnloadCurrentModel()
    {
        Destroy(_currentModel);
    }
    private void UpdateIndex(int value)
    {
        index = value;
        OnIndexChanged?.Invoke(index);
    }
}
