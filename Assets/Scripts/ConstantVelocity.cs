using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantVelocity : MonoBehaviour
{
    [SerializeField]
    private float velocityX;

    [SerializeField]
    private float velocityY;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(velocityX, velocityY);
    }

    public void SetVelocity(float _velocityX, float _velocityY)
    {
        velocityX = _velocityX;
        velocityY = _velocityY;
    }




}
