using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.GameInput;
using Assets.Scripts;
using Assets.Scripts.GameInformation;
using Assets.Scripts.Items;

public class trashCan : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
	{
		if (InputControls.APressed && collision.gameObject.tag == "Player")
		{
			Game.Player.dishesInventory.Remove(Game.Player.activeItem);
            Game.Player.removeActiveItem();
            Game.Player.updateHeldItemSprite();
		}
	}
}
