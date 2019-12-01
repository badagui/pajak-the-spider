using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField]
    bool isRenderingPreview;

    [SerializeField]
    private LineRenderer jumpLineRenderer;

    [SerializeField]
    private AudioEvent jumpChargeSound;

    [SerializeField]
    private AudioEvent jumpGoSound;

    [SerializeField]
    private Transform targetRotor;
    public Transform TargetRotor => targetRotor;

    public bool IsChargingJump { get; private set; }

    private Rigidbody2D myRigidbody2D;
    private JumpPathPlanner jumpPathPlanner;
    private PlayerAudio playerAudio;
    private TransformRotator spriteTransformRotator;

    private float rotorDegsPerSecond = 300f;

    public float JumpSpeedCurrent { get; private set; }
    public float JumpSpeedMax { get; private set; }
    private float jumpSpeedChargeRate = 30f;
    private float jumpSide = 1f;

    private void Awake()
    {
        JumpSpeedMax = 36f;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        jumpPathPlanner = GetComponent<JumpPathPlanner>();
        playerAudio = GetComponent<PlayerAudio>();
        spriteTransformRotator = GetComponent<TransformRotator>();
    }

    private void Update()
    {
        if (IsChargingJump)
        {
            JumpSpeedCurrent += jumpSpeedChargeRate * Time.deltaTime;
            if (isRenderingPreview)
            {
                float rotorCos = Mathf.Cos(Mathf.Deg2Rad * targetRotor.eulerAngles.z);
                float rotorSin = Mathf.Sin(Mathf.Deg2Rad * targetRotor.eulerAngles.z);
                Vector2 futureVelocity = new Vector2(rotorCos, rotorSin) * JumpSpeedCurrent;
                List<Vector3> pathPoints = jumpPathPlanner.GetJumpPathPoints(myRigidbody2D, futureVelocity);
                jumpPathPlanner.RenderLine(jumpLineRenderer, pathPoints);

            }

            if (JumpSpeedCurrent >= JumpSpeedMax)
            {
                DoChargedJump();
            }
        }
    }

    public void DoChargedJump()
    {
        if (IsChargingJump)
        {
            playerAudio.PlaySimpleAudio(jumpGoSound);
            myRigidbody2D.velocity = targetRotor.right * JumpSpeedCurrent;
            CalculatePathAndRotateAccordingToCurrentVelocity();
        }

        StopChargingJump();
    }

    public void StartChargingJump()
    {
        IsChargingJump = true;
        playerAudio.PlayProgressiveAudio(jumpChargeSound, JumpSpeedMax / jumpSpeedChargeRate);
    }

    public void StopChargingJump()
    {
        jumpLineRenderer.positionCount = 0;
        playerAudio.StopProgressiveAudio();
        JumpSpeedCurrent = 0f;
        IsChargingJump = false;
    }

    public void RotateAim(float normalizedDelta, float direction)
    {
        float currentAngle = targetRotor.localEulerAngles.z * Mathf.Deg2Rad;

        if ((direction > 0.1f && Mathf.Cos(currentAngle) < 0) || (direction < -0.1f && Mathf.Cos(currentAngle) > 0))
        {
            // invert
            float mirrorAngle = Mathf.Atan2(Mathf.Sin(currentAngle), -Mathf.Cos(currentAngle)) * Mathf.Rad2Deg;
            targetRotor.localEulerAngles = new Vector3(0, 0, mirrorAngle);
        }

        if (direction > 0.1f)
        {
            jumpSide = 1f;
        }
        if (direction < -0.1f)
        {
            jumpSide = -1f;
        }

        float currentAngleDeg = targetRotor.localEulerAngles.z;
        float angleStep = rotorDegsPerSecond * normalizedDelta;

        if (jumpSide > 0f) // right
        {
            float newAngleDeg = currentAngleDeg + angleStep;
            if (newAngleDeg > 90f && newAngleDeg < 180f)
            {
                targetRotor.localEulerAngles = new Vector3(0, 0, 90f);
            }
            else if (newAngleDeg < 270f && newAngleDeg > 180f)
            {
                targetRotor.localEulerAngles = new Vector3(0, 0, 270f);
            }
            else
            {
                targetRotor.Rotate(0, 0, angleStep);
            }
        }
        else // left
        {
            float newAngleDeg = currentAngleDeg - angleStep;
            if ((newAngleDeg > 270f) && (newAngleDeg < 360f))
            {
                targetRotor.localEulerAngles = new Vector3(0, 0, 270f);
            }
            else if ((newAngleDeg < 90f) && (newAngleDeg > 0f))
            {
                targetRotor.localEulerAngles = new Vector3(0, 0, 90f);
            }
            else
            {
                targetRotor.Rotate(0, 0, -angleStep);
            }
        }
    }

    private void CalculatePathAndRotateAccordingToCurrentVelocity()
    {
        List<Vector3> pathPoints = jumpPathPlanner.GetJumpPathPoints(myRigidbody2D);
        float rotationDuration = pathPoints.Count * jumpPathPlanner.PathPlanningTimeStep;
        float velocityDirection = myRigidbody2D.velocity.x >= 0f ? -1f : 1f;
        spriteTransformRotator.Rotate(velocityDirection, rotationDuration);
    }
}
