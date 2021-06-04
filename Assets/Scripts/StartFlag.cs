using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFlag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();

        if (!player)
        {
            return;
        }


        if (FindObjectOfType<LevelLoader>().IsTheLastScene)
        {
            FindObjectOfType<GameSession>().ReachFormosa = true;

            if (!FindObjectOfType<GameSession>().HasEveryThingToWin())
            {
                FindObjectOfType<GameSession>().ShowInfoLabel();
            }
        }
        else
        {
            // It is in the Dumgeon
            FindObjectOfType<LevelLoader>().LoadGameScene();
        }   
    }
}
