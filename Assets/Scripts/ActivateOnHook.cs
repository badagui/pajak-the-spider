using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnHook : MonoBehaviour, IUsedByHook
{
    [SerializeField]
    MonoBehaviour[] toBeActivated;

    [SerializeField]
    Animator[] toToggleBool;

    public void OnHooked()
    {
        foreach (MonoBehaviour mono in toBeActivated)
        {
            mono.enabled = true;
        }

        foreach (Animator animator in toToggleBool)
        {
            animator.SetBool("active", !animator.GetBool("active"));
        }
    }


}

public interface IUsedByHook
{
    void OnHooked();
}
