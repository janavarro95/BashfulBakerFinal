using Assets.Scripts.GameInformation;
using Assets.Scripts.Items;
using Assets.Scripts.Menus;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private List<AudioClip> woodStepSounds;
    [SerializeField]
    private AudioClip currentWalkingSound;

    private DeltaTimer walkingSoundTimer;

    public bool hidden;


    public GameObject heldObject;
    public SpriteRenderer heldObjectRenderer;

    private float height;

    public bool CanPlayerMove
    {
        get
        {
            if (Game.IsMenuUp == false && Game.IsScreenTransitionHappening == false) return true;
            else return false;
        }
        set
        {
            CanPlayerMove = value;
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

    public int currentStep;
    public GameObject arrow;

	// Use this for initialization
	void Start () {
        animator = this.GetComponent<Animator>();
        getRandomFootstepSound();
        walkingSoundTimer = new DeltaTimer(0.4d, Assets.Scripts.Enums.TimerType.CountDown, false);
        walkingSoundTimer.start();
        this.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        currentStep = -1;
        arrow = GameObject.FindWithTag("Arrow");
        hidden = false;

        heldObject = this.gameObject.transform.Find("HeldItem").gameObject;
        heldObjectRenderer = heldObject.GetComponent<SpriteRenderer>();

        height = GetComponent<SpriteRenderer>().sprite.texture.height / 2;
    }
	
	// Update is called once per frame
	void Update () {

        walkingSoundTimer.Update();
        checkForMovement();
        checkForPlayerVisibility();

        if (hidden && spriteRenderer.color.a > 0.2f)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - .02f);
        }
        else if (!hidden && spriteRenderer.color.a < 1)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a + .02f);
        }

        if (Game.Player.activeItem != null)
        {
            if(Game.Player.activeItem is Dish)
            {
                (Game.Player.activeItem as Dish).Update();
            }
        }
    }

    /// <summary>
    /// Checks if the player is hidden or not.
    /// </summary>
    private void checkForPlayerVisibility()
    {
        if (hidden == true && Game.Player.hidden == false)
        {
            Debug.Log("HIDE");
            Game.Player.hidden = true;
            //Game.Player.setPlayerHidden(Assets.Scripts.Enums.Visibility.Invisible);
        }
        else if (hidden == false && Game.Player.hidden == true)
        {
            Debug.Log("UNHIDE");
            Game.Player.hidden = false;
            //Game.Player.setPlayerHidden(Assets.Scripts.Enums.Visibility.Visible);
        }
    }

    /// <summary>
    /// Gets a random walking sound depending on the location that the player is at.
    /// </summary>
    private void getRandomFootstepSound()
    {
        if (SceneManager.GetActiveScene().name.Contains("Kitchen"))
        {
            int rand = Random.Range(0, woodStepSounds.Count);
            this.currentWalkingSound = woodStepSounds[rand];
            return;
        }
        else
        {
            this.currentWalkingSound = null;
            return;
        }
        
    }

    /// <summary>
    /// Plays the footstep walking sound for the player.
    /// </summary>
    private void playFootstepSound()
    {
        if (this.currentWalkingSound != null)
        {
            Game.SoundManager.playSound(CurrentWalkingSound);
            return;
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// Checks for the player to open up a menu.
    /// </summary>
    private void checkForMovement()
    {

        //If the player is visible they probably should be able to open a menu.
        if (this.spriteRenderer.enabled)
        {
            if (CanPlayerMove)
            {
                checkForMenuOpening();

                Vector3 offset = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * MovementSpeed;

                //Non-diagonal movement.
                if ((Mathf.Abs(offset.x) > Mathf.Abs(offset.y)))
                {

                    this.gameObject.transform.position += new Vector3(offset.x, 0, 0);
                }
                else
                {
                    this.gameObject.transform.position += new Vector3(0, offset.y, 0);
                }

                if ((Mathf.Abs(offset.x) > 0 || Mathf.Abs(offset.y) > 0) && walkingSoundTimer.IsFinished && this.spriteRenderer.enabled)
                {
                    getRandomFootstepSound();
                    playFootstepSound();
                    this.walkingSoundTimer.restart();
                }

                if ((Mathf.Abs(offset.x) > 0 || Mathf.Abs(offset.y) > 0 && Game.Player.hidden)){
                    Debug.Log("Unhide while moving!");
                    this.hidden = false;
                }

                playCharacterMovementAnimation(offset);

                if (!SceneManager.GetActiveScene().name.Contains("Kitchen"))
                {
                    transform.position = new Vector3 (transform.position.x, transform.position.y, (transform.position.y) * .01f);
                }
            }
            else
            {
                if (Assets.Scripts.GameInput.InputControls.StartPressed && Game.IsMenuUp)
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

    /// <summary>
    /// Checks if the player presses start to open up the game menu.
    /// </summary>
    private void checkForMenuOpening()
    {
        if (Assets.Scripts.GameInput.InputControls.StartPressed)
        {
            Menu.Instantiate<GameMenu>();
        }
        
    }

    /// <summary>
    /// Plays the animation for the player's movement.
    /// </summary>
    /// <param name="offset"></param>
    private void playCharacterMovementAnimation(Vector3 offset)
    {
        if (Game.Player.activeItem == null)
        {
            if (Mathf.Abs(offset.x) > Mathf.Abs(offset.y))
            {
                if (offset.x < 0)
                { //left walking animation
                    animator.Play("LWalk");
                    Game.Player.facingDirection = Assets.Scripts.Enums.FacingDirection.Left;
                    heldObject.transform.localPosition = new Vector3(-1f, 0, heldObject.transform.localPosition.z);
                    heldObjectRenderer.enabled = true;

                }
                else
                {
                    animator.Play("RWalk");
                    Game.Player.facingDirection = Assets.Scripts.Enums.FacingDirection.Right;
                    heldObject.transform.localPosition = new Vector3(1f, 0, heldObject.transform.localPosition.z);
                    heldObjectRenderer.enabled = true;
                }
            }
            else if (Mathf.Abs(offset.x) < Mathf.Abs(offset.y))
            {
                if (offset.y > 0)
                {
                    animator.Play("BWalk");
                    Game.Player.facingDirection = Assets.Scripts.Enums.FacingDirection.Up;
                    heldObject.transform.localPosition = new Vector3(0, 0, heldObject.transform.localPosition.z);
                    heldObjectRenderer.enabled = false;
                }
                else
                {
                    animator.Play("FWalk");
                    Game.Player.facingDirection = Assets.Scripts.Enums.FacingDirection.Down;
                    heldObject.transform.localPosition = new Vector3(0, 0, heldObject.transform.localPosition.z);
                    heldObjectRenderer.enabled = true;
                }
            }
            else if (Mathf.Abs(offset.x) == Mathf.Abs(offset.y) && (offset.x != 0 && offset.y != 0))
            {
                if (offset.x < 0)
                { //left walking animation
                    animator.Play("LWalk");
                    Game.Player.facingDirection = Assets.Scripts.Enums.FacingDirection.Left;
                    heldObject.transform.localPosition = new Vector3(-1f, 0, heldObject.transform.localPosition.z);
                    heldObjectRenderer.enabled = true;
                }
                else
                {
                    animator.Play("RWalk");
                    Game.Player.facingDirection = Assets.Scripts.Enums.FacingDirection.Right;
                    heldObject.transform.localPosition = new Vector3(1f, 0, heldObject.transform.localPosition.z);
                    heldObjectRenderer.enabled = true;
                }
            }

            else if (offset.x == 0 && offset.y == 0)
            {
                if (Game.Player.facingDirection == Assets.Scripts.Enums.FacingDirection.Down)
                {
                    animator.Play("FIdle");
                    heldObject.transform.localPosition = new Vector3(0, 0, heldObject.transform.localPosition.z);
                    heldObjectRenderer.enabled = true;
                }
                else if (Game.Player.facingDirection == Assets.Scripts.Enums.FacingDirection.Left)
                {
                    animator.Play("LIdle");
                    heldObject.transform.localPosition = new Vector3(-1f, 0, heldObject.transform.localPosition.z);
                    heldObjectRenderer.enabled = true;
                }
                else if (Game.Player.facingDirection == Assets.Scripts.Enums.FacingDirection.Right)
                {
                    animator.Play("RIdle");
                    heldObject.transform.localPosition = new Vector3(1f, 0, heldObject.transform.localPosition.z);
                    heldObjectRenderer.enabled = true;
                }
                else if (Game.Player.facingDirection == Assets.Scripts.Enums.FacingDirection.Up)
                {
                    animator.Play("BIdle");
                    heldObject.transform.localPosition = new Vector3(0, 0, heldObject.transform.localPosition.z);
                    heldObjectRenderer.enabled = false;
                }
            }
        }
        else if(Game.Player.activeItem!=null)
        {
            if (Mathf.Abs(offset.x) > Mathf.Abs(offset.y))
            {
                if (offset.x < 0)
                { //left walking animation
                    animator.Play("CarryingLWalk");
                    Game.Player.facingDirection = Assets.Scripts.Enums.FacingDirection.Left;
                    Game.Player.playHeldObjectAnimation(true);
                    heldObject.transform.localPosition = new Vector3(0, 0, heldObject.transform.localPosition.z);
                    heldObjectRenderer.enabled = true;

                }
                else
                {
                    animator.Play("CarryingRWalk");
                    Game.Player.facingDirection = Assets.Scripts.Enums.FacingDirection.Right;
                    Game.Player.playHeldObjectAnimation(true);

                    heldObject.transform.localPosition = new Vector3(0, 0, heldObject.transform.localPosition.z);
                    heldObjectRenderer.enabled = true;
                }
            }
            else if (Mathf.Abs(offset.x) < Mathf.Abs(offset.y))
            {
                if (offset.y > 0)
                {
                    animator.Play("CarryingBWalk");
                    Game.Player.facingDirection = Assets.Scripts.Enums.FacingDirection.Up;
                    Game.Player.playHeldObjectAnimation(true);
                    heldObject.transform.localPosition = new Vector3(0, 0, heldObject.transform.localPosition.z);
                    heldObjectRenderer.enabled = false;
                }
                else
                {
                    animator.Play("CarryingFWalk");
                    Game.Player.facingDirection = Assets.Scripts.Enums.FacingDirection.Down;
                    Game.Player.playHeldObjectAnimation(true);
                    heldObject.transform.localPosition = new Vector3(0, 0, heldObject.transform.localPosition.z);
                    heldObjectRenderer.enabled = true;
                }
            }
            else if (Mathf.Abs(offset.x) == Mathf.Abs(offset.y) && (offset.x != 0 && offset.y != 0))
            {
                if (offset.x < 0)
                { //left walking animation
                    animator.Play("CarryingLWalk");
                    Game.Player.facingDirection = Assets.Scripts.Enums.FacingDirection.Left;
                    Game.Player.playHeldObjectAnimation(true);
                    heldObject.transform.localPosition = new Vector3(0, 0, heldObject.transform.localPosition.z);
                    heldObjectRenderer.enabled = true;
                }
                else
                {
                    animator.Play("CarryingRWalk");
                    Game.Player.facingDirection = Assets.Scripts.Enums.FacingDirection.Right;
                    Game.Player.playHeldObjectAnimation(true);
                    heldObject.transform.localPosition = new Vector3(0, 0, heldObject.transform.localPosition.z);
                    heldObjectRenderer.enabled = true;
                }
            }

            else if (offset.x == 0 && offset.y == 0)
            {
                if (Game.Player.facingDirection == Assets.Scripts.Enums.FacingDirection.Down)
                {
                    animator.Play("CarryingFIdle");
                    Game.Player.playHeldObjectAnimation(false);
                    heldObject.transform.localPosition = new Vector3(0, 0, heldObject.transform.localPosition.z);
                    heldObjectRenderer.enabled = true;
                }
                else if (Game.Player.facingDirection == Assets.Scripts.Enums.FacingDirection.Left)
                {
                    animator.Play("CarryingLIdle");
                    Game.Player.playHeldObjectAnimation(false);
                    heldObject.transform.localPosition = new Vector3(0, 0, heldObject.transform.localPosition.z);
                    heldObjectRenderer.enabled = true;
                }
                else if (Game.Player.facingDirection == Assets.Scripts.Enums.FacingDirection.Right)
                {
                    animator.Play("CarryingRIdle");
                    Game.Player.playHeldObjectAnimation(false);
                    heldObject.transform.localPosition = new Vector3(0, 0, heldObject.transform.localPosition.z);
                    heldObjectRenderer.enabled = true;
                }
                else if (Game.Player.facingDirection == Assets.Scripts.Enums.FacingDirection.Up)
                {
                    animator.Play("CarryingBIdle");
                    Game.Player.playHeldObjectAnimation(false);
                    heldObject.transform.localPosition = new Vector3(0, 0, heldObject.transform.localPosition.z);
                    heldObjectRenderer.enabled = false;
                }
            }
        }
    }

    /// <summary>
    /// ???
    /// </summary>
    public void NextStep()
    {
        currentStep++;
        Debug.Log("Next step: " + currentStep);
        arrow.GetComponent<progress>().SetStep(currentStep);
    }
    
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle" && !hidden && (Assets.Scripts.GameInput.InputControls.BPressed || Input.GetKeyDown(KeyCode.F)))
        {
            hidden = true;
            defaultSpeed = 0;
        }
        else if (other.gameObject.tag == "Obstacle" && hidden && (Assets.Scripts.GameInput.InputControls.BPressed || Input.GetKeyDown(KeyCode.F)))
        {
            hidden = false;
            defaultSpeed = 1f;
        }
    }
}
