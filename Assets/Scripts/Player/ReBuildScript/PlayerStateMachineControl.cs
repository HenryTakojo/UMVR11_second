using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachineControl : StateMachineControl
{
    public float moveSpeed;
    public float rotationDamping;
    public float jumpForce;
    private float verticalVelocity;

    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public CharacterController cc;
    [HideInInspector]
    public Transform mainCameraTrans;

    [HideInInspector] public InputDeal inputD;

    void Start()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        mainCameraTrans = Camera.main.transform;
        inputD = GetComponent<InputDeal>();

        SwitchState(new PlayerController(this));
    }

}
