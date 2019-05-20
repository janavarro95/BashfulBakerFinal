using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameInformation;

public class GuardDayActivator : MonoBehaviour
{
    public bool[] manualActivate = new bool[4];

    // Start is called before the first frame update
    void Start()
    {
        // what day is it?
        SetGuards(Game.CurrentDayNumber);
    }

    public void SetGuards(int day)
    {
        GameObject act = GameObject.Find("DayOneGuards");
        if (day >= 1 || manualActivate[0])
            act.SetActive(true);
        else
            act.SetActive(false);

        act = GameObject.Find("DayTwoGuards");
        if (day >= 2 || manualActivate[1])
            act.SetActive(true);
        else
            act.SetActive(false);

        act = GameObject.Find("DayThreeGuards");
        if (day >= 3 || manualActivate[2])
            act.SetActive(true);
        else
            act.SetActive(false);

        act = GameObject.Find("DayFourGuards");
        if (day >= 4 || manualActivate[3])
            act.SetActive(true);
        else
            act.SetActive(false);

        if (day <= 0 || day >= 5)
        {
            Debug.Log("THIS IS NOT A DAY");
        }
    }
}
