﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("enter") || Input.GetKeyDown("space") || Input.GetKeyDown("e"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
