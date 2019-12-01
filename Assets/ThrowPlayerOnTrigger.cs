using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPlayerOnTrigger : MonoBehaviour
{
    [SerializeField]
    Vector2 velocity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            //collision.GetComponent<Rigidbody2D>().velocity = velocity;
            StartCoroutine(ThrowAfterXTime(collision.GetComponent<Rigidbody2D>()));
        }
    }

    private IEnumerator ThrowAfterXTime(Rigidbody2D rb2D)
    {
        yield return new WaitForSeconds(1);
        rb2D.velocity = velocity;
    }
}
