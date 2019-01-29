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
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Assets.Scripts.GameInput.InputControls.L3Down) Debug.Log("L3!");
        if (Assets.Scripts.GameInput.InputControls.R3Down) Debug.Log("R3!");

        if (Game.IsMenuUp == false)
        {
            if (Assets.Scripts.GameInput.InputControls.StartPressed)
            {
                Menu.Instantiate<Menu>();
            }
            this.gameObject.transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * MovementSpeed;
        }
        else if(Game.IsMenuUp==true)
        {
            if (Assets.Scripts.GameInput.InputControls.StartPressed)
            {
                Game.Menu.exitMenu();
            }
        }
	}
}
