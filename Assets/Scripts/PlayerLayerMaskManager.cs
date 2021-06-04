using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLayerMaskManager : MonoBehaviour
{
    public bool TouchedTheBoat
    {
        get
        {
            return GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Boat"));
        }
    }

    public bool TouchedTheHazradAndWater
    {
        get
        {
            return GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Hazards", "Ocean"));
        }
    }

    public bool TouchedTheCoconut
    {
        get
        {
            return GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Coconut"));
        }
    }

    public bool TouchedTheSeed
    {
        get
        {
            return GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Seed"));
        }
    }

    public bool TouchedTheBook
    {
        get
        {
            return GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Book"));
        }
    }

    public bool TouchedTheTunnel
    {
        get
        {
            return GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Tunnel"));
        }
    }
}
