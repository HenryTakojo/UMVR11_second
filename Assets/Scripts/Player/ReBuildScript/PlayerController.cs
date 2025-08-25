using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerController : PlayerBaseStateControl
{

    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private const float AnimatorDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;

    public PlayerController(PlayerStateMachineControl stateMachine) : base(stateMachine)
    {
    }
    
    public Vector3 CalculateMovement()
    {
        stateMachine.inputD.OnMove();

        float ver = stateMachine.inputD.vMove.y;
        float hor = stateMachine.inputD.vMove.x;

        Vector3 forward = stateMachine.mainCameraTrans.forward;
        Vector3 right = stateMachine.mainCameraTrans.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * ver +
            right * hor;
    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        if(movement!= Vector3.zero)
        {
            stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(movement), deltaTime * stateMachine.rotationDamping);
        }
    }

    public override void Enter()
    {
        stateMachine.inputD.OnAttack += PlayerAttackingEvent;
    }

    public override void Tick(float deltaTime)
    {
        Vector3 vMove = CalculateMovement();
        stateMachine.cc.Move(vMove * deltaTime * stateMachine.moveSpeed);
    }

    public override void Exit()
    {
        stateMachine.inputD.OnAttack -= PlayerAttackingEvent;
    }

    public void PlayerAttackingEvent()
    {
        Debug.Log("Attack");
    }
}
