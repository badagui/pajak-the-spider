using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformRotator : MonoBehaviour
{
    [SerializeField]
    Transform transformToRotate;

    private float direction;
    private float totalRotationTime;
    private float currentRotationTime;

    private void Update()
    {
        if (totalRotationTime > currentRotationTime)
        {
            currentRotationTime += Time.deltaTime;
            float rotationProgress = currentRotationTime / totalRotationTime;
            transformToRotate.localEulerAngles = new Vector3(0f, 0f, rotationProgress * 360f * direction);
        }
        else
        {
            StopRotationAndReset();
        }
    }

    /// <summary>
    /// direction: +1, -1
    /// </summary>
    public void Rotate(float _direction, float duration)
    {
        direction = _direction;
        totalRotationTime = duration;
    }

    public void StopRotationAndReset()
    {
        totalRotationTime = 0f;
        currentRotationTime = 0f;
        transformToRotate.localEulerAngles = new Vector3(0f, 0f, 0f);
    }
}
