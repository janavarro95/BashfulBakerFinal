﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    private GameObject player;
    private Vector3 position1;
    private Vector3 position2;

    private void Start()
    {
        position1 = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        position2 = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 2);
    }

    private void Update()
    {
        player = GameObject.Find("Player(Clone)");
        this.transform.position = this.transform.position.y > player.transform.position.y ? position1 : position2;
    }
}
