using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BaseState
{
    protected GameObject gameObject;
    protected Transform transform;
    
    public BaseState(GameObject _gameObject)
    {
        gameObject = _gameObject;
        transform = _gameObject.transform;
    }

    public abstract Type Tick();

    public virtual void OnEnterState() { }

    public virtual void OnLeaveState() { }
}
