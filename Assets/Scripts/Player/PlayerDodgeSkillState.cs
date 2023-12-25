using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeSkillState : PlayerBaseState
{
    private readonly int dodgeBackwardHash = Animator.StringToHash("Player_Dodge-Backward");
    private const float CrossFadeDuration = 0.1f;


    public PlayerDodgeSkillState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(dodgeBackwardHash, CrossFadeDuration);

    }

    public override void Tick(float deltaTime)
    {

        float normalizedTime = stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

        if (normalizedTime >= 1f)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }



    }
    public override void Exit()
    {

    }


}
