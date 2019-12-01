using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableHookOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        FindObjectOfType<Hook>().DisableCurrentHook();
    }
}
