using Assets.Scripts.GameInformation;
using Assets.Scripts.Menus;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script which controls moving the player about the environment.
/// </summary>
public class PlayerMovement : MonoBehaviour {


    [Tooltip("The default speed the player should move.")]
    /// <summary>
    /// The default speed the player should move.
    /// </summary>
    public float defaultSpeed = 1.0f;

    [Tooltip("How much to dampen the player's speed. Bigger numbers means a slower player.")]
    /// <summary>
    /// How much to slow the player's speed down by since Unity calculates speed every update tick. ~60 times a second!
    /// </summary>
    public float movementDampening = 20.0f;


    private Animator animator;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private AudioClip woodStepSound;
    [SerializeField]
    private AudioClip currentWalkingSound;

    private DeltaTimer walkingSoundTimer;


    public bool CanPlayerMove
    {
        get
        {
            if (Game.IsMenuUp == false && Game.IsScreenTransitionHappening == false) return true;
            else return false;
        }
    }

    public AudioClip CurrentWalkingSound
    {
        get
        {
            return currentWalkingSound;
        }
    }

    /// <summary>
    /// How fast the player should move. This can be modified by anything we want later.
    /// </summary>
    public float MovementSpeed
    {
        get
        {
            return (defaultSpeed) / movementDampening;
        }
    }

	// Use this for initialization
	void Start () {
        animator = this.GetComponent<Animator>();
        currentWalkingSound = woodStepSound;
        walkingSoundTimer = new DeltaTimer(0.4m, Assets.Scripts.Enums.TimerType.CountDown, false);
        walkingSoundTimer.start();
        this.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        walkingSoundTimer.Update();
        checkForMenuInteraction();

	}

    /// <summary>
    /// Checks for the player to open up a menu.
    /// </summary>
    private void checkForMenuInteraction()
    {

        //If the player is visible they probably should be able to open a menu.
        if (this.spriteRenderer.enabled)
        {
            if (CanPlayerMove)
            {
                checkForMenuOpening();

                Vector3 offset = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * MovementSpeed;

                this.gameObject.transform.position += offset;

                if ((Mathf.Abs(offset.x) > 0 || Mathf.Abs(offset.y) > 0) && walkingSoundTimer.IsFinished && this.spriteRenderer.enabled)
                {
                    Game.SoundManager.playSound(CurrentWalkingSound, Random.Range(2f, 3f));
                    this.walkingSoundTimer.restart();
                }

                playCharacterMovementAnimation(offset);

            }
            else if (Game.IsMenuUp == true)
            {
                if (Assets.Scripts.GameInput.InputControls.StartPressed)
                {
                    Game.Menu.exitMenu();
                }
            }
        }
        else
        {
            //If a mini game is open maybe open up a different menu? Maybe just have that code inside the minigame.
        }
    }

    private void checkForMenuOpening()
    {
        if (Assets.Scripts.GameInput.InputControls.RightBumperPressed)
        {
            Menu.Instantiate<InventoryMenu>();
        }
    }

    private void playCharacterMovementAnimation(Vector3 offset)
    {

        if (Mathf.Abs(offset.x) > Mathf.Abs(offset.y))
        {
            if (offset.x < 0)
            { //left walking animation
                animator.Play("LWalk");
                Game.Player.facingDirection = Assets.Scripts.Enums.FacingDirection.Left;
            }
            else
            {
                animator.Play("RWalk");
                Game.Player.facingDirection = Assets.Scripts.Enums.FacingDirection.Right;
            }
        }
        else if (Mathf.Abs(offset.x) < Mathf.Abs(offset.y))
        {
            if (offset.y > 0)
            {
                animator.Play("BWalk");
                Game.Player.facingDirection = Assets.Scripts.Enums.FacingDirection.Up;
            }
            else
            {
                animator.Play("FWalk");
                Game.Player.facingDirection = Assets.Scripts.Enums.FacingDirection.Down;
            }
        }

        else if (offset.x == 0 && offset.y == 0)
        {
            if (Game.Player.facingDirection == Assets.Scripts.Enums.FacingDirection.Down) animator.Play("FIdle");
            else if (Game.Player.facingDirection == Assets.Scripts.Enums.FacingDirection.Left) animator.Play("LIdle");
            else if (Game.Player.facingDirection == Assets.Scripts.Enums.FacingDirection.Right) animator.Play("RIdle");
            else if (Game.Player.facingDirection == Assets.Scripts.Enums.FacingDirection.Up) animator.Play("BIdle");
        }
    }

}
