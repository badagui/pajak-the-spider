using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirBossMoveCallbacks : MonoBehaviour
{
    [SerializeField]
    Animator birdActionsAnimator;

    [SerializeField]
    GameObject eggPrefab;

    [SerializeField]
    Transform eggSpawnPoint;

    public void BirdAttack()
    {
        birdActionsAnimator.SetTrigger("attack");

    }

    public void BirdFly()
    {
        birdActionsAnimator.SetTrigger("fly");
    }

    public void BirdEgg()
    {
        print("spawning egg");
        birdActionsAnimator.SetTrigger("egg");
        StartCoroutine(EggSpawnXSecondsFromNow(0.6f));
    }

    private IEnumerator EggSpawnXSecondsFromNow(float time)
    {
        yield return new WaitForSeconds(time);
        if (transform.localPosition.y < 0f) // debug hack (timeline on loop have animation events all triggering at once after first pass)
        {
            GameObject egg = Instantiate(eggPrefab, eggSpawnPoint.position, Quaternion.identity);
            Rigidbody2D eggRigidbody = egg.GetComponent<Rigidbody2D>();
            eggRigidbody.AddForce(Vector2.right * 65 + Vector2.up * 12, ForceMode2D.Impulse);
        }

        //yield return new WaitForSeconds(0.4f);
        //eggRigidbody.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
        //yield return new WaitForSeconds(0.2f);
        //eggRigidbody.AddForce(Vector2.right * 5, ForceMode2D.Impulse);
        //yield return new WaitForSeconds(0.2f);
        //eggRigidbody.AddForce(Vector2.right * 5, ForceMode2D.Impulse);
    }
}
