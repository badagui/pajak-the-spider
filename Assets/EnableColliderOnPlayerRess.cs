using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableColliderOnPlayerRess : MonoBehaviour
{
    private void Awake()
    {
        Player.OnPlayerRess += EnableSelfCollider;
    }

    private void EnableSelfCollider()
    {
        GetComponent<Collider2D>().enabled = true;

    }

    private void OnDestroy()
    {
        Player.OnPlayerRess -= EnableSelfCollider;
    }
}
