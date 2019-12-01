using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorWithJumpCharge : MonoBehaviour
{
    [SerializeField]
    private Color startColor;

    [SerializeField]
    private Color endColor;

    [SerializeField]
    private Image image;

    [SerializeField]
    private Jump refJump;

    private float startWidth;

    private void Awake()
    {
        startWidth = image.rectTransform.sizeDelta.x;
    }

    private void Update()
    {
        float chargeProgress = refJump.JumpSpeedCurrent / refJump.JumpSpeedMax;
        Color newColor = Color.Lerp(startColor, endColor, chargeProgress);
        image.color = newColor;

        image.rectTransform.sizeDelta = new Vector2(startWidth * chargeProgress, image.rectTransform.sizeDelta.y);

    }
}
