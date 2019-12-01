using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGameObjectOnTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject gameObjectToEnable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObjectToEnable.SetActive(true);
    }
}
