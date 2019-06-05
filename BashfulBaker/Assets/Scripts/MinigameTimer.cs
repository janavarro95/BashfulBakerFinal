using Assets.Scripts;
using Assets.Scripts.GameInformation;
using Assets.Scripts.Utilities.Timers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameTimer : MonoBehaviour
{

    public DeltaTimer timer;
    private Image timerImage;
    private Image timerRotation;

    // Start is called before the first frame update
    void Start()
    {
        timer = new DeltaTimer(100000000, Assets.Scripts.Enums.TimerType.CountUp, false, null);
        timer.start();
        timerImage = this.gameObject.transform.Find("Canvas").Find("Image").GetComponent<Image>();
        timerRotation= this.gameObject.transform.Find("Canvas").Find("Image").Find("Image").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.currentTime>0 && timer.currentTime < 15)
        {
            this.timerImage.color = new Color(0,0.5f,0);
        }
        else if(timer.currentTime>=15 && timer.currentTime < 25)
        {
            this.timerImage.color = Color.yellow;
        }
        else
        {
            this.timerImage.color = Color.red;
        }
        timer.Update();
        updateKnobRotation();
    }

    public void finishGame(Enums.CookingStationMinigame station)
    {
        this.timer.pause();
        Game.AddMinigameTime(station, this.timer.hours, this.timer.minutes, this.timer.seconds);
    }

    private void updateKnobRotation()
    {
        Quaternion q = timerRotation.rectTransform.localRotation;
        Vector3 euler = q.eulerAngles;
        euler.z = ((360f * (float)timer.currentTime / (float)100f) * 10f);
        q.eulerAngles = euler;
        timerRotation.rectTransform.localRotation = q;

    }

}
