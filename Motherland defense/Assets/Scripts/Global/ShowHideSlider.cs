using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ShowHideSlider : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ShowHide()
    {
        bool isOpen = _animator.GetBool("Show");
        _animator.SetBool("Show", !isOpen);
    }

    public bool CheckAnimatorIdle()
    {
        return !_animator.IsInTransition(0) && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;
    }
}
