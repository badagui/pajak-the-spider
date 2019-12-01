using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayerAnimatorOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player)
        {
            Animator playerAnimator = player.GetComponentInChildren<Animator>();

            playerAnimator.SetTrigger("onGround");
        }


    }
}
