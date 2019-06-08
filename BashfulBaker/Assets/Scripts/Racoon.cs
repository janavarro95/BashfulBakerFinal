using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;
using Assets.Scripts.Utilities.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Racoon : MonoBehaviour
{
    public bool hasNoticedRacoon;

    public Dialogue racoonDialogue;
    public Dialogue fedDialogue;
    public Dialogue noFoodDialogue;

    public Sprite daneFace;
    public Sprite raccoonFace;

    private DeltaTimer movementTimer;
    private Vector3 ogPosition;
    public SpriteRenderer bButton;
    private bool hasBeenFed;

    // Start is called before the first frame update
    void Start()
    {
        ogPosition = this.gameObject.transform.position;
        bButton.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.movementTimer != null)
        {
            this.movementTimer.Update();
            this.gameObject.transform.position = Vector3.Lerp(ogPosition, ogPosition + new Vector3(20, 0), (float)movementTimer.TimeFractionRemaining);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Game.Player.gameObject)
        {
            if (hasNoticedRacoon == false)
            {
                GameObject.Find("Headshot").GetComponent<Image>().sprite = daneFace;
                hasNoticedRacoon = true;
                Game.DialogueManager.StartDialogue(racoonDialogue);
            }

            bButton.enabled = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Game.Player.gameObject)
        {
            bButton.enabled = false;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Game.Player.activeItem != null && collision.gameObject == Game.Player.gameObject && InputControls.BPressed)
        {
            GameObject.Find("Headshot").GetComponent<Image>().sprite = raccoonFace;
            Game.Player.removeActiveItem();
            Game.DialogueManager.StartDialogue(fedDialogue);
            hasBeenFed = true;
            moveRight();
            bButton.enabled = false;
        }
        else if (Game.Player.activeItem == null && collision.gameObject == Game.Player.gameObject && InputControls.BPressed && hasBeenFed==false)
        {
            Game.DialogueManager.StartDialogue(noFoodDialogue);
            DeltaTimer anxiety = Game.Player.gameObject.GetComponent<PlayerMovement>().anxietyTimer;
            if (anxiety.state == Assets.Scripts.Enums.TimerState.Initialized || anxiety.state == Assets.Scripts.Enums.TimerState.Finished)
            {
                anxiety.restart();
            }
        }
    }

    public void moveRight()
    {
        if (movementTimer == null) movementTimer = new DeltaTimer(3, Assets.Scripts.Enums.TimerType.CountUp, false, disappear);
        movementTimer.start();
    }

    private void disappear()
    {
        //Game.Player.PlayerMovement.saveSpeed += 0.1f;
        Destroy(this.gameObject);
    }
    

}
