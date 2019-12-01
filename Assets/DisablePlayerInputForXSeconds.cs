using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePlayerInputForXSeconds : MonoBehaviour
{
    [SerializeField]
    bool disableSelf;

    [SerializeField]
    float time;

    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnEnable()
    {
        player.InputDisabled = true;
        StartCoroutine(EnableAfterXTime(time));

    }

    private IEnumerator EnableAfterXTime(float t)
    {
        yield return new WaitForSeconds(t);
        player.InputDisabled = false;
        if (disableSelf)
        {
            this.enabled = false;
        }
    }
}
