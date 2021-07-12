using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    public void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }

    public bool IsTheLastScene
    {
        get
        {
            return SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1;
        }
    }

    public void LoadMenuScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        FindObjectOfType<GameSession>().ResetEverything();
    }

    public bool IsPlayerInTheTunnel()
    {
        return IsTheLastScene;
    }

    public void LoadDumgeonScene()
    {
        SceneManager.LoadScene("Dumgeon");
    }

    public void LoadTunnelScene()
    {
        SceneManager.LoadScene("Tunnel");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }
}
