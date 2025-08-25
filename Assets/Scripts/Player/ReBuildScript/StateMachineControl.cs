using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineControl : MonoBehaviour
{
    private StateScript currentState;

    public void SwitchState(StateScript newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
    private void Update()
    {
        if(currentState != null)
        {
            currentState.Tick(Time.deltaTime);
        }
    }
}
