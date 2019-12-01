using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTargetOnTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            target.SetActive(false);
        }
    }
}
