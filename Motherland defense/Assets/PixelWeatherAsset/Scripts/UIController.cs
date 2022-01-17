using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private int type;

    public GameObject settingsBar;

    public GameObject rainObject;
    public GameObject snowObject;

    public GameObject rainSliders;
    public GameObject snowSliders;

    void Start()
    {
        ChangeType(0);
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.H))
            settingsBar.SetActive(!settingsBar.activeSelf);
    }

    public void ChangeType(int t)
    {
        type = t;
        if (type == 0)
        {
            rainObject.SetActive(true);
            rainSliders.SetActive(true);
            snowSliders.SetActive(false);
            snowObject.SetActive(false);
        }
        else if (type == 1)
        {
            rainObject.SetActive(false);
            rainSliders.SetActive(false);
            snowSliders.SetActive(true);
            snowObject.SetActive(true);
        }
    }
}
