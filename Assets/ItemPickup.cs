using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField]
    Sprite itemSprite;

    private void OnEnable()
    {
        if (!itemSprite)
        {
            itemSprite = GetComponent<SpriteRenderer>().sprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player)
        {
            player.HoldItem(itemSprite);
            gameObject.SetActive(false);
        }
    }


}
