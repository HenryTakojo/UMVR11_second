using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseStateControl : StateScript
{
    protected PlayerStateMachineControl stateMachine;

    public PlayerBaseStateControl(PlayerStateMachineControl stateMachine)
    {
        this.stateMachine = stateMachine;
    }

}
