using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    private void Update()
    {
        if (TouchedTheLand())
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    private bool TouchedTheLand()
    {
        return GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Formosa"));
    }

    public void Move(Vector2 velocity)
    {
        if (TouchedTheLand())
        {
            return;
        }

        GetComponent<Rigidbody2D>().velocity = velocity;
    }
}
