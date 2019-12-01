using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCursorOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
