using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BirdBossHealth : MonoBehaviour
{
    [SerializeField]
    Image healthImage;

    [SerializeField]
    Animator transitionOutAnimator;

    [SerializeField]
    SimpleAudioEvent killAudioEvent;

    public Animator hitAnimator;

    public int MaxHealth = 100;
    public int Health = 100;

    //true if dead
    public void TakeDamage(int dmg)
    {
        Health -= dmg;
        if (Health <= 0) Health = 0;
        healthImage.transform.localScale = new Vector3((float)Health / MaxHealth, healthImage.transform.localScale.y, healthImage.transform.localScale.z);

        if (hitAnimator)
        {
            hitAnimator.SetTrigger("active");
        }

        if (Health <= 0)
        {
            Time.timeScale = 0.05f;
            FindObjectOfType<Player>().InputDisabled = true;
            killAudioEvent.Play(GetComponent<AudioSource>());
            transitionOutAnimator.SetTrigger("activate");

            
            
            //LoadScene
            //winScreen.SetActive(true);
            //print("win game");
        }
    }

    private void SetHealthMax()
    {
        Health = MaxHealth;
    }

    private void OnEnable()
    {
        Health = MaxHealth;
        Player.OnPlayerRess += SetHealthMax;
    }

    private void OnDestroy()
    {
        Player.OnPlayerRess -= SetHealthMax;
    }
}
