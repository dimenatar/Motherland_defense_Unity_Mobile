using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] private Animator _creditAnimator;
    private UserData _userData;
    void Start()
    {
        if (CheckCredits())
        {
            _creditAnimator.SetBool("isCredits", true);
            _userData.ShownCredits();
        }
    }

    public void Skip()
    {
        _creditAnimator.SetTrigger("Skip");
    }

    private bool CheckCredits()
    {
        _userData = UserProgressManager.LoadUserData(UserProgressManager.Path);
        if (_userData != null)
        {
            return _userData.IsNeedToShowCredits;
        }
        else
        {
            return false;
        }
    }
}
