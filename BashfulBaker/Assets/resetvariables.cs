using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;
using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;
using Assets.Scripts.QuestSystem.Quests;
using Assets.Scripts.Items;


public class resetvariables : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Game.Day1JebTalkedTo = false;
        Game.Day2JebTalkedTo = false;
        Game.Day3JebTalkedTo = false;
        Game.Day4JebTalkedTo = false;
        Game.TalkedtoSully = false;
    }
}

