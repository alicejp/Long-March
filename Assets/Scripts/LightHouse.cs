using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            if (tag == "ZombieLand")
            {
                FindObjectOfType<Ocean>().SetCollider2DEnable(false);
                FindObjectOfType<GameSession>().LightHouseSwitchIsOn = true;
                MoveWithTheBoat();
            }
            else if (tag == "Formosa")
            {
                FindObjectOfType<Ocean>().SetCollider2DEnable(true);
                FindObjectOfType<GameSession>().LightHouseSwitchIsOn = false;
            }
        }
    }

    private void MoveWithTheBoat()
    {
        FindObjectOfType<Player>().MoveAutomatically(new Vector2(1f, 0f));
        FindObjectOfType<Boat>().transform.position = FindObjectOfType<Player>().transform.position;
        FindObjectOfType<Boat>().Move(new Vector2(1f, 0f));
    }
}
