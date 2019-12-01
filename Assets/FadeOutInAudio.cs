using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutInAudio : MonoBehaviour
{
    [SerializeField]
    SimpleAudioEvent bossSong;

    [SerializeField]
    float timeToFade;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(SoundFadeOut(audioSource, timeToFade));
    }

    private IEnumerator SoundFadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;

        if (bossSong)
        {
            bossSong.Play(audioSource);
        }
    }
}
