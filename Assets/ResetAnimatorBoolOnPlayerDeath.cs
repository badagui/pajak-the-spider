using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimatorBoolOnPlayerDeath : MonoBehaviour
{
    private void Awake()
    {
        Player.OnPlayerRess += ResetAnimatorBool;
    }

    private void ResetAnimatorBool()
    {
        Animator anim = GetComponent<Animator>();
        if (anim)
        {
            anim.SetBool("active", false);
            anim.SetTrigger("reset");
        }
    }

    private void OnDestroy()
    {
        Player.OnPlayerRess -= ResetAnimatorBool;
    }
}
