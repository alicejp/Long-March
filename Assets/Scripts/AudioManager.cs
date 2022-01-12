using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource gameplayTheme;
    public AudioSource zombieTheme;
    public float dangerRange = 10f;

    ZombieSpawner zombieSpawner;

    private void Awake()
    {
        zombieSpawner = FindObjectOfType<ZombieSpawner>();
    }

    private void Update()
    {
        if (zombieSpawner == null)
        {
            Debug.LogWarning("ZombieSpawner is null");
            return;
        }

        Debug.Log(zombieSpawner.AverageDistanceWithPlayer);

        if (zombieSpawner.AverageDistanceWithPlayer < dangerRange)
        {
            float averageDistance = zombieSpawner.AverageDistanceWithPlayer;
            float ratio = MapDistanceToVolumeAmplify(averageDistance);
            UpdateVolume(ratio);
        }
    }

    void UpdateVolume(float ratio)
    {
        gameplayTheme.volume = 1-ratio;
        zombieTheme.volume = ratio;
    }

    private float MapDistanceToVolumeAmplify(float distance)
    {
        //get closer, the volume is higher
        //y(volume) = 1 - dx/dangerRange
        return (dangerRange - distance) / dangerRange;
    }
}
