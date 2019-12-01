using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventHandler : MonoBehaviour
{
    [SerializeField]
    private AudioEvent audioEvent;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponentInParent<AudioSource>();

    }
    public void PlayAudioEvent()
    {
        audioEvent.Play(audioSource);
    }
}
