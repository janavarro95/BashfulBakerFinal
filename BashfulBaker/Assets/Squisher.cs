﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squisher : MonoBehaviour
{
    public bool activate = false;

    // number ranges
    // horizontal
    public float horiLerp = 0;
    public float horiRange = 0;
    public float horiSpeed = 1;

    // vertical
    public float vertLerp = 0;
    public float vertRange = 0;
    public float vertSpeed = 1;

    // motions
    private Vector3 origin;
    private float startX;
    private float startY;
    private float goalX;
    private float goalY;

    // Start is called before the first frame update
    void Start()
    {
        origin = this.transform.localScale;
        startX = origin.x - horiRange;
        startY = origin.y - vertRange;
        goalX = origin.x + horiRange;
        goalY = origin.y + vertRange;

    }

    // Update is called once per frame
    void Update()
    {
        if (activate)
        {
            // lerps
            horiLerp += Time.deltaTime * horiSpeed;
            vertLerp += Time.deltaTime * vertSpeed;

            // change lerps
            if (horiLerp >= 1)
            {
                horiLerp = 0;
                float temp = goalX;
                goalX = startX;
                startX = temp;
            }
            if (vertLerp >= 1)
            {
                vertLerp = 0;
                float temp = goalY;
                goalY = startY;
                startY = temp;
            }

            // adjust saves
            float xx = Mathf.Lerp(startX, goalX, horiLerp);
            float yy = Mathf.Lerp(startY, goalY, vertLerp);
            this.transform.localScale = new Vector2(xx, yy);
        }
        else if (this.transform.localScale != origin)
        {
            ResetScale();
        }
    }

    public void ResetScale()
    {
        this.transform.localScale = origin;
    }
}
