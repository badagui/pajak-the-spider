using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Audio Events/ShootProgressive")]
public class ShootProgressiveAudioEvent : AudioEvent
{
    public AudioClip audioClip;

    public float volume;

    public float pitch;

    public override void Play(AudioSource audioSource)
    {
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.Play();
    }
    
}
