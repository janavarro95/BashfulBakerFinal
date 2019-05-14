﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// from https://forum.unity.com/threads/drawing-simple-pixel-perfect-shapes-circles-lines-etc-like-in-gamemaker.479181/

public class CircleDraw : MonoBehaviour
{
    public float theta_scale = 0.01f;        //Set lower to add more points
    public int size; //Total number of points in circle
    public float radius = 3f;
    public LineRenderer lineRenderer;

    void Awake()
    {
        float sizeValue = (2.0f * Mathf.PI) / theta_scale;
        size = (int)sizeValue;
        size++;
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("TextMeshPro/Bitmap"));
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = size;
    }

    void Update()
    {
        Vector3 pos;
        float theta = 0f;
        for (int i = 0; i < size; i++)
        {
            theta += (2.0f * Mathf.PI * theta_scale);
            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta);
            x += gameObject.transform.position.x;
            y += gameObject.transform.position.y;
            pos = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, pos);
        }
    }
}