using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerShootState : BaseState
{
    private Player player;
    private Jump jump;
    private Shoot shoot;
    private Transform targetRotor;
    private Animator animator;
    private Rigidbody2D myRigidbody2D;


    public PlayerShootState(Player _player) : base(_player.gameObject)
    {
        player = _player;
        jump = player.GetComponent<Jump>();
        shoot = player.GetComponent<Shoot>();
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

        if (!shoot.IsChargingShoot && myRigidbody2D.velocity.magnitude < 10f)
        {
            float walkDirection = Input.GetAxis("Horizontal");
            myRigidbody2D.velocity = new Vector2(walkDirection * 8, myRigidbody2D.velocity.y);
            animator.SetFloat("walkDirection", walkDirection);
        }

        float normalizedRotationStep = Input.GetAxis("Vertical") * Time.deltaTime;
        jump.RotateAim(normalizedRotationStep, Input.GetAxis("Horizontal"));


        if (Input.GetButtonDown("Jump"))
        {
            animator.SetFloat("walkDirection", 0f);
            animator.SetTrigger("holdingJump");
            shoot.StartChargingShoot();
        }

        if (Input.GetButtonUp("Jump"))
        {
            animator.ResetTrigger("holdingJump");
            animator.SetTrigger("onGround");
            shoot.DoChargedShoot();
            return typeof(PlayerGroundState);
            //return typeof(PlayerFlyingState); ? 
            //return typeof(PlayerGroundState); ? 
        }


        return typeof(PlayerShootState);
    }

    public override void OnEnterState()
    {
        animator.SetTrigger("onGround");
    }

    public override void OnLeaveState()
    {
        shoot.StopChargingShoot();
        animator.SetFloat("walkDirection", 0f);
        animator.ResetTrigger("holdingJump");
        animator.ResetTrigger("onGround");
    }


    

    
}
