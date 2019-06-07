using Assets.Scripts.GameInformation;
using Assets.Scripts.Items;
using Assets.Scripts.Utilities;
using Assets.Scripts.GameInput;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class minimap : MonoBehaviour
{
    public GameObject map, player;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(InputControls.APressed && collision.gameObject.tag == "Player")
		{
			map.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -3);
			map.GetComponent<SpriteRenderer>().enabled = !map.GetComponent<SpriteRenderer>().enabled;
            if (map.GetComponent<SpriteRenderer>().enabled)
            {
                Game.HUD.showHUD = false;
            }
            else
            {
                Game.HUD.showHUD = true;
            }

			player.GetComponent<PlayerMovement>().CanPlayerMove = !player.GetComponent<PlayerMovement>().CanPlayerMove;
		}
    }
}
