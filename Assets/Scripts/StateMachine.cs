using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMachine : MonoBehaviour
{
    public BaseState CurrentState { get; private set; }
    private List<BaseState> availableStates;
    //public event Action<BaseState> OnStateChanged;

    private void Update()
    {
        if (CurrentState == null)
        {
            CurrentState = availableStates[0];
        }
    }

    public void SetAvailableStates(List<BaseState> states)
    {
        availableStates = states;
    }

    public void SetCurrentState(Type newStateType)
    {
        if (CurrentState.GetType() == newStateType)
        {
            //print("same state " + CurrentState);
            return; // same state, do nothing
        }

        CurrentState.OnLeaveState();
        CurrentState = null;

        foreach (BaseState state in availableStates)
        {
            if (state.GetType() == newStateType)
            {
                CurrentState = state;
                break;
            }
        }

        if (CurrentState != null)
        {
            CurrentState.OnEnterState();
        }
        //print("new state " + CurrentState);
    }

}
