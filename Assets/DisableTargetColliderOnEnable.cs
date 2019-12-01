using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTargetColliderOnEnable : MonoBehaviour
{
    [SerializeField]
    Collider2D colliderToDisable;

    private void OnEnable()
    {
        colliderToDisable.enabled = false;
    }
}
