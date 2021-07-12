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
            if (tag == "ZombieLand") // The LightHouseSwitch on the ZombieLand
            {
                FindObjectOfType<GameSession>().LightHouseSwitchIsOn = true;
            }
            else if (tag == "Formosa")// The LightHouseSwitch on the Formosa
            {
                FindObjectOfType<Ocean>().SetCollider2DEnable(true);
                FindObjectOfType<GameSession>().LightHouseSwitchIsOn = false;
                FindObjectOfType<Player>().SwitchSprite(false);
            }
        }
    }
    
}
