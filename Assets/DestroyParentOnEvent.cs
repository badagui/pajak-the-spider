using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParentOnEvent : MonoBehaviour
{
    public void DestroyParent(float waitTime)
    {

            Destroy(transform.parent.gameObject, waitTime);

    }
}
