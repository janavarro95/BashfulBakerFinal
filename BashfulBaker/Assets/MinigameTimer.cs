using Assets.Scripts;
using Assets.Scripts.GameInformation;
using Assets.Scripts.Utilities.Timers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameTimer : MonoBehaviour
{

    public DeltaTimer timer;


    private TMPro.TextMeshProUGUI displayText;
    // Start is called before the first frame update
    void Start()
    {
        timer = new DeltaTimer(100000000, Assets.Scripts.Enums.TimerType.CountUp, false, null);
        timer.start();
        displayText = this.gameObject.transform.Find("Canvas").Find("Image").Find("timePassed").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timer.Update();
        displayText.text = timer.minSecString();
    }

    public void finishGame(Enums.CookingStationMinigame station)
    {
        this.timer.pause();
        Game.AddMinigameTime(station, this.timer.hours, this.timer.minutes, this.timer.seconds);
    }

}
