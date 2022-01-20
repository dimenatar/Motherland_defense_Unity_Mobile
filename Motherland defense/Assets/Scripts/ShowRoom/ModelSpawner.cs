using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModelSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _models;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _slowerRotationForce;

    private List<GameObject> _actualObjectList = new List<GameObject>();
    private GameObject _currentObject;
    private int index;

    public void LoadNextModel()
    {
        if (index < _actualObjectList.Count-1)
        {
            index++;
        }
        else
        {
            index = 0;
        }
        LoadModel(_actualObjectList[index]);
    }

    public void LoadPreviosModel()
    {
        if (index > 1)
        {
            index--;
        }
        else
        {
            index = _actualObjectList.Count-1;
        }
        LoadModel(_actualObjectList[index]);
    }
    public void LoadTower()
    {
        _actualObjectList = _models.Where(tower => tower.GetComponent<Tower>() != null).ToList();
        if (index < _actualObjectList.Count)
        {
            LoadModel(_actualObjectList[index]);
        }
    }

    public void LoadCharacter()
    {
        _actualObjectList = _models.Where(character => character.GetComponent<Enemy>() != null || character.GetComponent<Hero>() != null).ToList();
        index = 0;
        if (index < _actualObjectList.Count)
        {
            LoadModel(_actualObjectList[index]);
        }
    }

    public List<GameObject> GetObjects()
    {
        return _models;
    }

    private void SetTowerSize(GameObject tower)
    {
        tower.transform.localScale /= 2;
    }

    private void Start()
    {
        LoadTower();
    }

    private void LoadModel(GameObject model)
    {
        UnloadCurrentModel();
        _currentObject = Instantiate(model, _spawnPosition.position, Quaternion.Euler(0, 0, 0));
        _currentObject.AddComponent<Rotater>().SetRotationSpeed(_rotationSpeed, _slowerRotationForce);

        _currentObject.SetActive(true);
        if (_currentObject.GetComponent<Enemy>() || _currentObject.GetComponent<Hero>())
        { 

        }
        else if (_currentObject.GetComponent<Tower>())
        {
            SetTowerSize(_currentObject);
        }
    }

    private void UnloadCurrentModel()
    {
        Destroy(_currentObject);
    }
}
