using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableComponentAfterXTime : MonoBehaviour
{
    [SerializeField]
    MonoBehaviour scriptToEnable;

    [SerializeField]
    float time;

    private void OnEnable()
    {
        StartCoroutine(EnableAfterXTime());
    }

    private IEnumerator EnableAfterXTime()
    {
        yield return new WaitForSeconds(time);
        scriptToEnable.enabled = true;
    }
}
