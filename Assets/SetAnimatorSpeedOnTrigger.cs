using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimatorSpeedOnTrigger : MonoBehaviour
{
    [SerializeField]
    Animator animatorToChange;

    [SerializeField]
    float newPlaybackSpeed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animatorToChange.speed = newPlaybackSpeed;
    }
}
