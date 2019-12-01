using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerShootState : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            StartCoroutine(SetShootStateInX(0.8f, player));
            player.SetShootState();
        }
    }

    private IEnumerator SetShootStateInX(float time, Player player)
    {
        yield return new WaitForSeconds(time);
        player.SetShootState();
    }
}
