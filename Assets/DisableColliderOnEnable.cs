using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableColliderOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Collider2D>().enabled = false;
    }
}
