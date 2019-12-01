using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisableSpriteAndColliderForXSeconds : MonoBehaviour
{
    [SerializeField]
    private float minDisableTime;

    [SerializeField]
    private float maxDisableTime;

    private float refDisableTime;

    private SpriteRenderer spriteRenderer;

    private new Collider2D collider2D;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        spriteRenderer.enabled = false;
        collider2D.enabled = false;
        refDisableTime = Time.time;
        float totalDisableTime = Random.Range(minDisableTime, maxDisableTime);
        StartCoroutine(ReactivateInXSeconds(totalDisableTime));
    }

    private IEnumerator ReactivateInXSeconds(float time)
    {
        while (Time.time - refDisableTime < time)
        {
            yield return new WaitForSeconds(0.2f);
        }
        spriteRenderer.enabled = true;
        collider2D.enabled = true;
    }
}
