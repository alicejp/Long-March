using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


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
        SceneManager.LoadScene(0);
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
