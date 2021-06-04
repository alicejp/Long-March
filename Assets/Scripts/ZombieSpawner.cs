using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] GameObject zombiePrefab;
    [SerializeField] int count = 5;
    [SerializeField] float timeBetweenSpawns = 3f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        for(int i = 0; i< count; i++)
        {
            Instantiate(zombiePrefab, transform.position, Quaternion.identity);
            var deltaTime = UnityEngine.Random.Range(0, timeBetweenSpawns);
            yield return new WaitForSeconds(deltaTime);
        }
    }
}
