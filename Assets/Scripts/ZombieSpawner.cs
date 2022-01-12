using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] Zombie zombiePrefab;
    [SerializeField] int count = 5;
    [SerializeField] float timeBetweenSpawns = 3f;

    List<Zombie> zombies = new List<Zombie>();

    public float AverageDistanceWithPlayer
    {
        get
        {
            float sum = 0;
            foreach(Zombie zombie in zombies)
            {
                float distance = zombie.distanceWithPlayer;
                sum += distance;
            }

            float result = sum / zombies.Count;
            return result;
        }
    }

    void Start()
    {
        StartCoroutine(Spawn());
    }


    private IEnumerator Spawn()
    {
        for(int i = 0; i< count; i++)
        {
            Zombie zombie = Instantiate(zombiePrefab, transform.position, Quaternion.identity) as Zombie;
            zombies.Add(zombie);

            var deltaTime = UnityEngine.Random.Range(0, timeBetweenSpawns);
            yield return new WaitForSeconds(deltaTime);
        }
    }
}
