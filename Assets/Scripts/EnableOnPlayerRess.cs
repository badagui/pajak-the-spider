using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnPlayerRess : MonoBehaviour
{
    private void Awake()
    {
        Player.OnPlayerRess += enableThis;
    }

    private void enableThis()
    {
        this.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        Player.OnPlayerRess -= enableThis;
    }

}
