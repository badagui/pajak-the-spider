using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayableDirectorTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            GetComponent<PlayableDirector>().Play();
            GetComponent<Collider2D>().enabled = false;
        }
    }




}
