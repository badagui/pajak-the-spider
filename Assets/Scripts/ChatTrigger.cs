using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatTrigger : MonoBehaviour
{
    [SerializeField][TextArea]
    string text;

    [SerializeField]
    float sizeOffset;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            BubbleChatSpawner.instance.SpawnChat(transform, 60, 90, text, sizeOffset);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        BubbleChatSpawner.instance.DespawnChat(transform);
    }

}
