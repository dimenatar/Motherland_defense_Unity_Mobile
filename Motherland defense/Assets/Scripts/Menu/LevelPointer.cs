using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPointer : MonoBehaviour
{
    [SerializeField] private readonly string _levelName;

    public void Deselect()
    {

    }

    public void ObjectClick()
    {
        Debug.Log('1');
        SceneManager.LoadScene(_levelName);
    }
    private void OnMouseDown()
    {
        Debug.Log("1");
    }
}
