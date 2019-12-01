using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathCollider : MonoBehaviour
{
    [SerializeField]
    int damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BirdBossHealth bossHealth = collision.GetComponent<BirdBossHealth>();
        if (bossHealth)
        {
            bossHealth.TakeDamage(damage);

        }
    }

}
