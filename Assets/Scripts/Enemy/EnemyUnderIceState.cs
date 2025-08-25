using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnderIceState : EnemyBaseState
{
    private float frozenTime = 2f;


    private const float CrossFadeDuration = 0.1f;
    public EnemyUnderIceState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
    }

    public override void Tick(float deltaTime)
    {
        frozenTime -= deltaTime;
        stateMachine.Animator.speed = 0f;
        if (frozenTime <= 0)
        {
            frozenTime = 2f;
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }
    }
}
