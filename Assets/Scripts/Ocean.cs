﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCollider2DEnable(bool enable)
    {
        GetComponent<Collider2D>().enabled = enable;
    }
}
