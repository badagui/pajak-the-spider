using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHookedState : BaseState
{
    private Player player;
    private Hook hook;
    private Animator animator;
    private Rigidbody2D myRigidbody2D;

    public PlayerHookedState(Player _player) : base(_player.gameObject)
    {
        player = _player;
        hook = player.GetComponent<Hook>();
        animator = player.GetComponentInChildren<Animator>();
        myRigidbody2D = player.GetComponent<Rigidbody2D>();
    }

    public override Type Tick()
    {
        if (Input.GetButtonDown("Jump"))
        {
            hook.DisableCurrentHook();
            hook.HookJump();
        }

        if (!hook.IsHooked)
        {
            return typeof(PlayerFlyingState);
        }

        if (Input.GetAxis("Horizontal") != 0f)
        {
            hook.AddHookedForce(Input.GetAxis("Horizontal")); // todo: * Time.deltaTime (test later)
        }

        if (Input.GetAxis("Vertical") != 0f)
        {
            hook.ChangeHookedDistance(Input.GetAxis("Vertical") * Time.deltaTime);
        }

        animator.SetFloat("walkDirection", Input.GetAxis("Horizontal"));
        //animator.SetFloat("walkDirection", myRigidbody2D.velocity.x);


        return typeof(PlayerHookedState);

    }

    public override void OnEnterState()
    {
        animator.SetFloat("walkDirection", myRigidbody2D.velocity.x);
        animator.SetTrigger("onHook");
        hook.ChangeHookedDistance(Input.GetAxis("Vertical") * Time.deltaTime);
    }

    public override void OnLeaveState()
    {
        hook.DisableCurrentHook();
        animator.SetFloat("walkDirection", 0f);
        animator.ResetTrigger("onHook");
    }

}
