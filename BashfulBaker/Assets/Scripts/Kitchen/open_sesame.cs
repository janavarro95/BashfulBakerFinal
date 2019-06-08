using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameInformation;

public class open_sesame : MonoBehaviour
{
    GameObject Jeb;
    private void Start()
    {
        if (Game.Day1JebTalkedTo && Game.CurrentDayNumber == 1)
        {
            this.gameObject.SetActive(false);
        }
        if (Game.Day2JebTalkedTo && Game.CurrentDayNumber == 2)
        {
            this.gameObject.SetActive(false);
        }
        if (Game.Day3JebTalkedTo && Game.CurrentDayNumber == 3)
        {
            this.gameObject.SetActive(false);
        }
        if (Game.Day4JebTalkedTo && Game.CurrentDayNumber == 4)
        {
            this.gameObject.SetActive(false);
        }

        Jeb = GameObject.Find("Jeb");
    }
    private void Update()
    {
        if(Jeb.transform.position.x > -.4)
        {
            this.gameObject.SetActive(false);
        }
    }
}
