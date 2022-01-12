using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashLevelManager : MonoBehaviour
{
    public float autoLoadnextLevelAfter;

    void Start()
    {
        Invoke("LoadNextLevel", autoLoadnextLevelAfter);
    }

    void LoadNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }
}
