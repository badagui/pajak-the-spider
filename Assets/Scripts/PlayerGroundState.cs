using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerGroundState : BaseState
{
    private Player player;
    private Jump jump;
    private Transform targetRotor;
    private Animator animator;
    private Rigidbody2D myRigidbody2D;


    public PlayerGroundState(Player _player) : base(_player.gameObject)
    {
        player = _player;
        jump = player.GetComponent<Jump>();
        targetRotor = jump.TargetRotor;
        animator = player.GetComponentInChildren<Animator>();
        myRigidbody2D = player.GetComponent<Rigidbody2D>();
    }

    public override Type Tick()
    {
        if (!player.IsOnGround)
        {
            return typeof(PlayerFlyingState);
        }

        if (!jump.IsChargingJump && myRigidbody2D.velocity.magnitude < 10f)
        {
            float walkDirection = player.InputDisabled ? 0f : Input.GetAxis("Horizontal");
            myRigidbody2D.velocity = new Vector2(walkDirection * 8, myRigidbody2D.velocity.y);
            animator.SetFloat("walkDirection", walkDirection);
        }

        if (!player.InputDisabled)
        {
            float normalizedRotationStep = Input.GetAxis("Vertical") * Time.deltaTime;
            jump.RotateAim(normalizedRotationStep, Input.GetAxis("Horizontal"));
        }

        if (Input.GetButtonDown("Jump") && !player.InputDisabled)
        {
            animator.SetFloat("walkDirection", 0f);
            animator.SetTrigger("holdingJump");
            jump.StartChargingJump();
        }

        if (Input.GetButtonUp("Jump"))
        {
            animator.ResetTrigger("holdingJump");
            animator.SetTrigger("onGround");
            jump.DoChargedJump();
            //return typeof(PlayerFlyingState); ? 
            //return typeof(PlayerGroundState); ? 
        }


        return typeof(PlayerGroundState);
    }

    public override void OnEnterState()
    {

        animator.SetTrigger("onGround");
    }

    public override void OnLeaveState()
    {
        jump.StopChargingJump(); // ver suspeita de stopsound, bug para se remover isso?
        animator.SetFloat("walkDirection", 0f);
        animator.ResetTrigger("holdingJump");
        animator.ResetTrigger("onGround");
    }


    

    
}
