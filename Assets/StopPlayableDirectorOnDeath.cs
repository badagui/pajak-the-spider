using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StopPlayableDirectorOnDeath : MonoBehaviour
{
    private void Awake()
    {
        Player.OnPlayerRess += StopDirector;
    }

    private void StopDirector()
    {
        GetComponent<PlayableDirector>().Stop();
    }

    private void OnDestroy()
    {
        Player.OnPlayerRess -= StopDirector;
    }
}
