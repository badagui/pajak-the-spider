using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessWithJumpKey : MonoBehaviour
{
    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            gameObject.SetActive(false);
            player.RessAtCheckpoint();
        }
    }
}
