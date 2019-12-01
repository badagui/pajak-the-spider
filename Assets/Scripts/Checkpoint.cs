using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponentInChildren<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            bool newCheckpoint = player.SetCheckpoint(transform.position);

            if (newCheckpoint && animator) animator.SetTrigger("activate");

            if (newCheckpoint && audioSource) audioSource.Play();
            print("game saved");
        }
    }
}
