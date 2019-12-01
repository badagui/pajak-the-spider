using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource simpleAudioSource;

    [SerializeField]
    private AudioSource progressiveAudioSource;

    [SerializeField]
    private float pitchIncreaseValue;

    private float pitchIncreaseTimer;

    float pitchIncreasePerSecond;

    private bool isIncreasingPitch;

    private void Update()
    {
        if (isIncreasingPitch && pitchIncreaseTimer > 0f)
        {
            pitchIncreaseTimer -= Time.deltaTime;
            progressiveAudioSource.pitch += pitchIncreasePerSecond * Time.deltaTime;
        }
    }

    public void PlayProgressiveAudio(AudioEvent audioEvent, float t)
    {
        //print("progressive play volume: " + progressiveAudioSource.volume.ToString() + "pitch: " + progressiveAudioSource.pitch.ToString());
        
        audioEvent.Play(progressiveAudioSource);
        pitchIncreaseTimer = t;
        pitchIncreasePerSecond = pitchIncreaseValue / t;
        isIncreasingPitch = true;
    }

    public void StopProgressiveAudio()
    {
        //print("progressive stop!");
        progressiveAudioSource.Stop();
    }

    public void PlaySimpleAudio(AudioEvent audioEvent)
    {
        audioEvent.Play(simpleAudioSource);
    }





}
