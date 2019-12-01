using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CaveLock : MonoBehaviour
{
    [SerializeField]
    private SimpleAudioEvent unlockAudioEvent;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player && player.IsHoldingItem)
        {
            unlockAudioEvent.Play(audioSource);
            player.UseItem();
            GetComponent<Collider2D>().enabled = false;
            GetComponent<PlayableDirector>().Play();
        }
    }

}
