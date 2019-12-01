using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGameObjectOnPlayerDeath : MonoBehaviour
{
    private void Awake()
    {
        Player.OnPlayerRess += DestroySelf;
    }

    private void DestroySelf()
    {
        gameObject.SetActive(false);

    }

    private void OnDestroy()
    {
        Player.OnPlayerRess -= DestroySelf;
    }
}
