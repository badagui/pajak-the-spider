using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossMode
{
    idle,
    goingUp,
    findingPlayer,
    goDownAndAttack,
    findingNest,
    rollingStones,
}


public class BirBossAI : MonoBehaviour
{
    private BossMode bossMode;
    private Animator animator;


    /*
    private void Awake()
    {
        bossMode = BossMode.idle;
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (bossMode == BossMode.goingUp)
        {
            animator.SetTrigger("idle");
        }

    }*/
}
