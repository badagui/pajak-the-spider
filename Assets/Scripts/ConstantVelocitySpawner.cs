using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantVelocitySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject prefabToSpawn;

    [SerializeField]
    float velocityX;

    [SerializeField]
    float velocityY;

    public bool isSpawning;


    [SerializeField]
    float spawnTimer;

    private float startTime;

    private void Awake()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        if (isSpawning)
        {
            float elapsed = Time.time - startTime;
            if (elapsed > spawnTimer)
            {
                GameObject obj = Instantiate(prefabToSpawn, transform);
                ConstantVelocity constantVelocity = obj.GetComponent<ConstantVelocity>();
                if (constantVelocity)
                {
                    obj.GetComponent<ConstantVelocity>().SetVelocity(velocityX, velocityY);
                }
                startTime = Time.time;
            }
        }
    }

}
