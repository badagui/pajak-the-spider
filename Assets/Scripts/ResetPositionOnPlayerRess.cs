using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPositionOnPlayerRess : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;
    private Rigidbody2D myRigidbody2D;

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        startRotation = transform.rotation;
        Player.OnPlayerRess += ResetPosition;
    }

    private void ResetPosition()
    {
        transform.rotation = startRotation;
        transform.position = startPosition;
        if (myRigidbody2D)
        {
            myRigidbody2D.velocity = Vector2.zero;
            myRigidbody2D.angularVelocity = 0f;
        }
    }

    private void OnDestroy()
    {
        Player.OnPlayerRess -= ResetPosition;
    }
}
