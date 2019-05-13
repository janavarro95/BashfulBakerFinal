﻿using Assets.Scripts.GameInformation;
using Assets.Scripts.Utilities.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racoon : MonoBehaviour
{
    public bool hasNoticedRacoon;

    public Dialogue racoonDialogue;
    public Dialogue fedDialogue;
    public Dialogue noFoodDialogue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Game.Player.gameObject && hasNoticedRacoon == false)
        {
            hasNoticedRacoon = true;
            Game.DialogueManager.StartDialogue(racoonDialogue);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (Game.Player.activeItem != null && collision.gameObject==Game.Player.gameObject)
        {
            Game.Player.removeActiveItem();
            Game.DialogueManager.StartDialogue(fedDialogue);
            Destroy(this.gameObject);
        }
        else if (Game.Player.activeItem == null && collision.gameObject == Game.Player.gameObject)
        {
            Game.DialogueManager.StartDialogue(noFoodDialogue);
            DeltaTimer anxiety = Game.Player.gameObject.GetComponent<PlayerMovement>().anxietyTimer;
            if (anxiety.state==Assets.Scripts.Enums.TimerState.Initialized || anxiety.state== Assets.Scripts.Enums.TimerState.Finished)
            {
                anxiety.restart();
            }
        }
    }
}