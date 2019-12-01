using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterXSecondsOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        Destroy(gameObject, 10f);
    }
}
