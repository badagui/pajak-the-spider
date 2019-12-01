using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnEvent : MonoBehaviour
{
    public void DestroySelf(float waitTime)
    {

            Destroy(gameObject, waitTime);

    }
}
