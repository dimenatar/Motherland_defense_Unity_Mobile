using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    public event Action OnDied;
    public event Action OnDestroed;
}
