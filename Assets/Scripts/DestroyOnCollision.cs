using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BirdBossHealth>())
        {
            Destroy(this.gameObject);
        }
    }
}
