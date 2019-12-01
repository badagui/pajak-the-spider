using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Image>().enabled = true;
        GetComponent<Animator>().SetTrigger("activate");
    }
}
