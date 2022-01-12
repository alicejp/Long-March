using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFlag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If it is player who collides with the flag.
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
                FindObjectOfType<LevelController>().ShowInfoLabel();
            }
        }
        else
        {
            // It is in the Dumgeon
            FindObjectOfType<LevelLoader>().LoadGameScene();
        } 
    }
}
