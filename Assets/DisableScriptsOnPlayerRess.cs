using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableScriptsOnPlayerRess : MonoBehaviour
{
    [SerializeField]
    MonoBehaviour[] scriptsToDisable;

    private void Awake()
    {
        Player.OnPlayerRess += DisableScripts;
    }

    private void DisableScripts()
    {
        foreach (MonoBehaviour mono in scriptsToDisable)
        {
            mono.enabled = false;
        }

    }

    private void OnDestroy()
    {
        Player.OnPlayerRess -= DisableScripts;
    }
}

