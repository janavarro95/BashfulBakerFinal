﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Cos(Time.time / 1.5f) * 3f, transform.position.z);
    }
}
