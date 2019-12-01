using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PointToMove
{
    public string pointName;
    public Vector3 destination;
    public float travelTime;
}

public class MoveTransformWithRoute : MonoBehaviour
{
    [SerializeField]
    private PointToMove[] route;

    [SerializeField]
    private bool isLooping;

    [SerializeField]
    private bool disableScriptAtEnd;

    [SerializeField]
    private int startStepInd;

    private float stepStartTime;
    private Vector3 stepStartPosition;
    private int currentStep;

    private void Awake()
    {
        SetupStep(startStepInd);
    }

    private void OnEnable()
    {
        SetupStep(startStepInd);
    }

    private void Update()
    {
        float traveledRatio = (Time.time - stepStartTime) / route[currentStep].travelTime;

        if (traveledRatio >= 1f) traveledRatio = 1f;

        Vector3 destinationVector = route[currentStep].destination - stepStartPosition;
        transform.localPosition = destinationVector * traveledRatio + stepStartPosition;

        if (traveledRatio >= 1f)
        {
            bool noNextStep = currentStep + 1 >= route.Length;
            if (noNextStep)
            {
                if (disableScriptAtEnd)
                {
                    this.enabled = false;
                    return;
                }
                if (isLooping)
                {
                    SetupStep(0);
                }
                /*
                else
                {
                    this.enabled = false; // single route
                    return;
                }*/
            }
            else
            {
                SetupStep(currentStep + 1);
            }
        }
    }

    private void SetupStep(int stepInd)
    {
        currentStep = stepInd;
        stepStartTime = Time.time;
        stepStartPosition = transform.localPosition;
    }

}



