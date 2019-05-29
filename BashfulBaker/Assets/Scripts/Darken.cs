using Assets.Scripts.GameInformation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Darken : MonoBehaviour
{
    public Material mat;
    public float transparency, darkenSpeed;
    public bool darken = true;
    public bool startAtZero = true;
    // Start is called before the first frame update
    void Start()
    {
        if (startAtZero)
            transparency = 0;
        mat.SetFloat("_Transparency", transparency);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(transparency < .9)
            transparency += darkenSpeed;
        */
        if (darken)
        {
            if (Game.PhaseTimer != null)
            {
                transparency = Mathf.Lerp(.5f, .9f, (float)(Game.PhaseTimer.currentTime / Game.PhaseTimer.maxTime));
                mat.SetFloat("_Transparency", transparency);
            }
            else
            {
                transparency = 0.5f;
                mat.SetFloat("_Transparency", transparency);
            }
        }

        if (SceneManager.GetActiveScene().name == "EndOfDay")
        {
            transparency = 0.5f;
            mat.SetFloat("_Transparency", transparency);
        }
    }
}
