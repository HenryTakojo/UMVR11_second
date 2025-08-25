using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintState : PlayerBaseState
{
    private readonly int SprintHash = Animator.StringToHash("Sprint");

    private Vector3 momentum;

    private float sprintTime = 0.1f;
    public PlayerSprintState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private const float crossFadeDuratoin = 0.1f;
    public override void Enter()
    {
        momentum = CalculateMovement();
        momentum.y = 0f;

        stateMachine.Animator.CrossFadeInFixedTime(SprintHash, crossFadeDuratoin);
    }

    public override void Tick(float deltaTime)
    {
        Move(momentum * stateMachine.FreeLookMovementSpeed*5, deltaTime);

        FaceMovementDirection(momentum, deltaTime);

        sprintTime -= Time.deltaTime;
        if (sprintTime <= 0f)
        {
            sprintTime = 0.1f;
            ReturnToLocomotion();
        }
    }

    public override void Exit()
    {

    }

    private Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();
        Vector3 vMoveValue = forward * stateMachine.InputReader.MovementValue.y +
            right * stateMachine.InputReader.MovementValue.x;

        return vMoveValue;

    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            deltaTime * stateMachine.RotationDamping);
    }
}
