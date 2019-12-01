using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject prefabToSpawn;

    public bool isSpawning;


    [SerializeField]
    float spawnTimer;

    private float startTime;

    private void OnEnable()
    {
        startTime = Time.time;
        if (isSpawning)
        {
            Instantiate(prefabToSpawn, transform);
        }
    }

    private void Update()
    {
        if (isSpawning)
        {
            float elapsed = Time.time - startTime;
            if (elapsed > spawnTimer)
            {
                Instantiate(prefabToSpawn, transform);
                startTime = Time.time;
            }
        }
    }

}
