using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BubbleChat
{
    public GameObject bubbleObject;
    public Transform transformToFollow;
    public float offsetX;
    public float offsetY;
}

public class BubbleChatSpawner : MonoBehaviour
{
    public static BubbleChatSpawner instance;

    [SerializeField]
    new Camera camera;

    [SerializeField]
    GameObject bubbleChatPrefab;

    List<BubbleChat> spawnedBubbles = new List<BubbleChat>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.LogError("double singleton");
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("singleton already set");
        }
    }

    private void LateUpdate()
    {
        if (spawnedBubbles.Count > 0)
        {
            foreach(BubbleChat bubbleChat in spawnedBubbles)
            {
                Vector3 screenPosition = camera.WorldToScreenPoint(bubbleChat.transformToFollow.position);
                bubbleChat.bubbleObject.transform.position = screenPosition + new Vector3(bubbleChat.offsetX, bubbleChat.offsetY, 0);
            }
        }
    }

    public void SpawnChat(Transform transformToFollow, float offsetX, float offsetY, string textString)
    {
        BubbleChat bubbleChat = new BubbleChat();
        bubbleChat.bubbleObject = Instantiate(bubbleChatPrefab, transform);
        bubbleChat.bubbleObject.GetComponentInChildren<TextMeshProUGUI>().text = textString;
        bubbleChat.transformToFollow = transformToFollow;
        bubbleChat.offsetX = offsetX;
        bubbleChat.offsetY = offsetY;

        spawnedBubbles.Add(bubbleChat);
    }

    public void SpawnChat(Transform transformToFollow, float offsetX, float offsetY, string textString, float fontSizeOffset)
    {
        BubbleChat bubbleChat = new BubbleChat();
        bubbleChat.bubbleObject = Instantiate(bubbleChatPrefab, transform);
        bubbleChat.bubbleObject.GetComponentInChildren<TextMeshProUGUI>().text = textString;
        bubbleChat.bubbleObject.GetComponentInChildren<TextMeshProUGUI>().fontSize += fontSizeOffset;
        bubbleChat.transformToFollow = transformToFollow;
        bubbleChat.offsetX = offsetX;
        bubbleChat.offsetY = offsetY;

        spawnedBubbles.Add(bubbleChat);
    }

    public void DespawnChat(Transform transform)
    {
        for (int i = 0; i < spawnedBubbles.Count; i++)
        {
            if (spawnedBubbles[i].transformToFollow == transform)
            {
                Destroy(spawnedBubbles[i].bubbleObject);
                spawnedBubbles.Remove(spawnedBubbles[i]);
                return;
            }
        }
    }


}
