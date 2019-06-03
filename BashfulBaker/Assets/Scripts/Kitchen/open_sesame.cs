using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameInformation;

public class open_sesame : MonoBehaviour
{
    GameObject Jeb;
    private void Start()
    {
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
        if (GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().currentStep >= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        if(Jeb.transform.position.x > -.4)
        {
            this.gameObject.SetActive(false);
        }
    }
}
