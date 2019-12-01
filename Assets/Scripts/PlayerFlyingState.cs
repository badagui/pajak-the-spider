using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerFlyingState : BaseState
{
    Player player;
    Hook playerHook;
    Animator animator;
    SpriteRenderer playerJumpTargetRenderer;

    public PlayerFlyingState(Player _player) : base(_player.gameObject)
    {
        player = _player;
        playerHook = player.GetComponent<Hook>();
        playerJumpTargetRenderer = player.GetComponent<Jump>().TargetRotor.GetComponentInChildren<SpriteRenderer>();
        animator = player.GetComponentInChildren<Animator>();
    }

    public override Type Tick()
    {
        if (player.IsOnGround)
        {
            return typeof(PlayerGroundState);
            //animator.SetTrigger("onGround");
        }

        if (Input.GetButtonDown("Jump"))
        {
            Time.timeScale = 0.2f;
        }

        if (Input.GetButtonUp("Jump") && Time.timeScale == 0.2f)
        {
            Time.timeScale = 1f;
            playerHook.StartChannelingHook();
        }

        if (playerHook.IsHooked)
        {
            return typeof(PlayerHookedState);
        }

        return typeof(PlayerFlyingState);
    }

    public override void OnEnterState()
    {
        playerJumpTargetRenderer.enabled = false;
        animator.SetTrigger("onAir");
    }

    public override void OnLeaveState()
    {
        playerJumpTargetRenderer.enabled = true;
        animator.ResetTrigger("onAir");
        playerHook.StopChannelingHook();
        Time.timeScale = 1f;
    }

    

}