using Assets.Scripts.GameInformation;
using Assets.Scripts.Menus;
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
	}
	
	// Update is called once per frame
	void Update () {

        if (Game.IsMenuUp == false)
        {
            if (Assets.Scripts.GameInput.InputControls.StartPressed)
            {
                Menu.Instantiate<Menu>();
            }

            Vector3 offset = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * MovementSpeed;

            this.gameObject.transform.position += offset;
            playCharacterMovementAnimation(offset);

        }
        else if(Game.IsMenuUp==true)
        {
            if (Assets.Scripts.GameInput.InputControls.StartPressed)
            {
                Game.Menu.exitMenu();
            }
        }
	}

    private void playCharacterMovementAnimation(Vector3 offset)
    {

        if (Mathf.Abs(offset.x) > Mathf.Abs(offset.y))
        {
            if (offset.x < 0)
            { //left walking animation
                animator.Play("LeftWalkAnimation");
                Game.Player.facingDirection = Assets.Scripts.Enums.FacingDirection.Left;
            }
            else
            {
                animator.Play("RightWalkAnimation");
                Game.Player.facingDirection = Assets.Scripts.Enums.FacingDirection.Right;
            }
        }
        else if (Mathf.Abs(offset.x) < Mathf.Abs(offset.y))
        {
            if (offset.y > 0)
            {
                animator.Play("UpWalkAnimation");
                Game.Player.facingDirection = Assets.Scripts.Enums.FacingDirection.Up;
            }
            else
            {
                animator.Play("DownWalkAnimation");
                Game.Player.facingDirection = Assets.Scripts.Enums.FacingDirection.Down;
            }
        }

        else if (offset.x == 0 && offset.y == 0)
        {
            if (Game.Player.facingDirection == Assets.Scripts.Enums.FacingDirection.Down) animator.Play("DownIdle");
            else if (Game.Player.facingDirection == Assets.Scripts.Enums.FacingDirection.Left) animator.Play("LeftIdle");
            else if (Game.Player.facingDirection == Assets.Scripts.Enums.FacingDirection.Right) animator.Play("RightIdle");
            else if (Game.Player.facingDirection == Assets.Scripts.Enums.FacingDirection.Up) animator.Play("UpIdle");
        }
    }

}
