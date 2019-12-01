using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Audio Events/Simple")]
public class SimpleAudioEvent : AudioEvent
{
    public AudioClip[] audioClips;

    [MinMaxRange(0, 2)]
    public RangedFloat volume;

    [MinMaxRange(0, 2)]
    public RangedFloat pitch;

    public override void Play(AudioSource audioSource)
    {
        if (audioClips.Length == 0) return;

        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.volume = Random.Range(volume.minValue, volume.maxValue);
        audioSource.pitch = Random.Range(pitch.minValue, pitch.maxValue);
        audioSource.Play();
    }
}
