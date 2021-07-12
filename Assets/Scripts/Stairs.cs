using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            //Leave the tunnel, we need to reset the reach formosa to false;
            if (FindObjectOfType<LevelLoader>().IsTheLastScene)
            {
                FindObjectOfType<GameSession>().ReachFormosa = false;
            }

            FindObjectOfType<LevelLoader>().LoadGameScene();
        }
    }
}
