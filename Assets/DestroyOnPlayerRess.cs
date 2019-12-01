using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnPlayerRess : MonoBehaviour
{
    private void Awake()
    {
        Player.OnPlayerRess += DestroySelf;
    }

    private void DestroySelf()
    {
        Destroy(gameObject);

    }

    private void OnDestroy()
    {
        Player.OnPlayerRess -= DestroySelf;
    }
}
