using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField]
    private LineRenderer hookLineRenderer;

    [SerializeField]
    private LayerMask hookLayer;

    [SerializeField]
    private Transform hookTargetRotor;

    private DistanceJoint2D distanceJoint2D;

    private Rigidbody2D myRigidbody2D;
    private TransformRotator spriteTransformRotator;
    private JumpPathPlanner jumpPathPlanner;

    private float reelSpeed = 15f;
    private float horizontalForce = 9f;

    private float boostVelocityMultiplier = 1.5f;
    private float boostVelocityMax = 25f;

    private float hookMaxTime = 0.2f;
    private float hookCurrentTime;

    public bool IsHooked { get; private set; }

    private float hookMaxDistance = 8.5f;

    private Collider2D hookCollider;

    private PlayerAudio playerAudio;

    [SerializeField]
    private AudioEvent webShootSound;

    [SerializeField]
    private AudioEvent webHookSound;

    private void Awake()
    {
        distanceJoint2D = GetComponent<DistanceJoint2D>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        spriteTransformRotator = GetComponent<TransformRotator>();
        jumpPathPlanner = GetComponent<JumpPathPlanner>();
        playerAudio = GetComponent<PlayerAudio>();
    }

    private void Update()
    {
        if (hookCurrentTime > 0f)
        {
            hookCurrentTime -= Time.deltaTime;
            RenderHookLine(transform.position, -hookTargetRotor.up * 10 + transform.position);

            hookCollider = Physics2D.Raycast(transform.position, -hookTargetRotor.up, 10, hookLayer).collider;
            if (hookCollider)
            {
                HookToCollider(hookCollider);
            }
        }
        else
        {
            StopChannelingHook();
        }

        if (IsHooked)
        {
            RenderHookLine(transform.position, hookCollider.transform.position);
        }
    }

    public void StartChannelingHook()
    {
        hookCurrentTime = hookMaxTime;
        playerAudio.PlaySimpleAudio(webShootSound);
    }

    public void StopChannelingHook()
    {
        hookCurrentTime = 0f;
        hookLineRenderer.positionCount = 0;
    }


    public void HookJump()
    {
        // speed limit
        if ((myRigidbody2D.velocity * boostVelocityMultiplier).magnitude < boostVelocityMax)
        {
            myRigidbody2D.velocity = myRigidbody2D.velocity * boostVelocityMultiplier;
        }
        else
        {
            myRigidbody2D.velocity = myRigidbody2D.velocity.normalized * boostVelocityMax;
        }

        spriteTransformRotator.StopRotationAndReset();
        CalculatePathAndRotateAccordingToInput();
    }

    public void ChangeHookedDistance(float normalizedDelta)
    {
        float newDistance = distanceJoint2D.distance - normalizedDelta * reelSpeed;
        if (newDistance > hookMaxDistance)
        {
            distanceJoint2D.distance = hookMaxDistance;

        }
        else
        {
            distanceJoint2D.distance = newDistance;
        }
    }

    public void AddHookedForce(float normalizedDelta)
    {
        myRigidbody2D.AddForce(Vector2.right * horizontalForce * normalizedDelta, 0f);
    }

    public void DisableCurrentHook()
    {
        hookLineRenderer.positionCount = 0;
        distanceJoint2D.enabled = false;
        IsHooked = false;
    }

    private void HookToCollider(Collider2D hookCollider)
    {
        Rigidbody2D hookRb = hookCollider.GetComponent<Rigidbody2D>();
        if (hookRb)
        {
            distanceJoint2D.connectedBody = hookRb;
            distanceJoint2D.connectedAnchor = Vector3.zero;
        }
        else
        {
            distanceJoint2D.connectedBody = null;
            distanceJoint2D.connectedAnchor = hookCollider.transform.position;
        }
        distanceJoint2D.distance = (hookCollider.transform.position - transform.position).magnitude;
        distanceJoint2D.enabled = true;
        playerAudio.PlaySimpleAudio(webHookSound);
        IsHooked = true;

        IUsedByHook interactableHook = hookCollider.GetComponent<IUsedByHook>();
        if (interactableHook != null)
        {
            interactableHook.OnHooked();
        }
    }

    private void RenderHookLine(Vector3 A, Vector3 B)
    {
        hookLineRenderer.positionCount = 2;
        hookLineRenderer.SetPosition(0, A);
        hookLineRenderer.SetPosition(1, B);
    }

    private void CalculatePathAndRotateAccordingToInput()
    {
        List<Vector3> pathPoints = jumpPathPlanner.GetJumpPathPoints(myRigidbody2D);
        float rotationDuration = pathPoints.Count * jumpPathPlanner.PathPlanningTimeStep;

        float horizontalInput = Input.GetAxis("Horizontal");
        float direction;
        if (Input.GetAxis("Horizontal") != 0f)
        {
            direction = Input.GetAxis("Horizontal") > 0f ? -1f : 1f;
        }
        else
        {
            direction = myRigidbody2D.velocity.x >= 0f ? -1f : 1f;
        }
        
        spriteTransformRotator.Rotate(direction, rotationDuration);
    }
}
