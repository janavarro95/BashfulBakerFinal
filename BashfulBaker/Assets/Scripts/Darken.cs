using Assets.Scripts.GameInformation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darken : MonoBehaviour
{
    public Material mat;
    public float transparency, darkenSpeed;
    // Start is called before the first frame update
    void Start()
    {
        transparency = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(transparency < .9)
            transparency += darkenSpeed;
        */
        try
        {
            transparency = 1 - (float)(Game.PhaseTimer.currentTime / Game.PhaseTimer.maxTime);
            mat.SetFloat("_Transparency", transparency);
        }
        catch(Exception err)
        {

        }
    }
}
