using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnEnable : MonoBehaviour
{
    [SerializeField]
    private SimpleAudioEvent audioEvent;

    private void OnEnable()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource) audioEvent.Play(audioSource);
    }
}
