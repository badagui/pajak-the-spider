using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableComponentsAndActivateAnimatorsOnTrigger : MonoBehaviour
{
    [SerializeField]
    MonoBehaviour[] componentsToDisable;

    [SerializeField]
    MonoBehaviour[] componentsToActivate;

    [SerializeField]
    Animator[] animatorsToActivate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            foreach (MonoBehaviour mono in componentsToDisable)
            {
                mono.enabled = false;
            }

            foreach (MonoBehaviour mono in componentsToActivate)
            {
                mono.enabled = true;
            }

            foreach (Animator animator in animatorsToActivate)
            {
                animator.SetTrigger("activate");
            }        
        }

    }

}
