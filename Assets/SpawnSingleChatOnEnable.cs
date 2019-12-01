using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSingleChatOnEnable : MonoBehaviour
{
    [SerializeField]
    [TextArea]
    string text;

    [SerializeField]
    Transform targetTransform;

    [SerializeField]
    float fontSizeOffset;

    private void OnEnable()
    {
        if (fontSizeOffset != 0f)
        {
            BubbleChatSpawner.instance.SpawnChat(targetTransform, 120, 80, text, fontSizeOffset);
        }
        else
        {
            BubbleChatSpawner.instance.SpawnChat(targetTransform, 120, 80, text);

        }

    }
    private void OnDisable()
    {
        BubbleChatSpawner.instance.DespawnChat(targetTransform);        
    }

}
