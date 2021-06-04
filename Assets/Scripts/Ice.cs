using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    public void Melt()
    {
        Destroy(gameObject);
    }

    public void SetSpeed(Vector2 speed, float sign)
    {
        GetComponent<Rigidbody2D>().velocity = speed * sign;
    }
}
