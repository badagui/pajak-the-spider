using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeSceneTrigger : MonoBehaviour
{
    [SerializeField]
    string sceneName;

    [SerializeField]
    float teleportDelay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            StartCoroutine(LoadSceneAfterXTime(teleportDelay));
        }
    }

    private IEnumerator LoadSceneAfterXTime(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneName);
    }
}
