using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [SerializeField]
    private Transform transformToFollow;

    [SerializeField]
    private float xOffset;

    [SerializeField]
    private float yOffset;

    public bool isFollowing = true;//
    /*
    private void OnValidate()
    {
        transform.position = new Vector3(transformToFollow.position.x + xOffset, transformToFollow.position.y + yOffset, transform.position.z);
    }*/

    private void LateUpdate()
    {
        if (isFollowing)
        {
            transform.position = new Vector3 (transformToFollow.position.x + xOffset, transformToFollow.position.y + yOffset, transform.position.z);
        }
    }
}
