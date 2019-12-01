using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField]
    Transform objectTransform;

    [SerializeField]
    private float degsPerSecond;

    private void Update()
    {
        float axisHorizontal = Input.GetAxis("Horizontal");
        objectTransform.localEulerAngles += new Vector3(0, 0, degsPerSecond) * Time.deltaTime * -axisHorizontal;
    }
}
