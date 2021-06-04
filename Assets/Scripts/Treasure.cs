using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Treasure : MonoBehaviour
{
    [SerializeField] int gameKeyCountTarget = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();

        if (!player)
        {
            return;
        }
        FindObjectOfType<LevelLoader>().LoadDumgeonScene();
    }
}
