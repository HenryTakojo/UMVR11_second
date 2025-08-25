using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDeal : MonoBehaviour
{

    [HideInInspector]
    public Vector2 vMove;
    public event Action OnAttack;

    public void OnMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        vMove = h * Vector2.right + v * Vector2.up;
    }

    public void OnAttackEvent()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (OnAttack != null)
            {
                OnAttack();
            }
        }
    }
}
