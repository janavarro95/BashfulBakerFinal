using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Items;
using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;
using Assets.Scripts;

public class PickUp : MonoBehaviour
{
    public Enums.SpecialIngredients item;
    Inventory specItemInv;
    
    void Start()
    {
        specItemInv = Game.Player.specialIngredientsInventory;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && (InputControls.APressed || Input.GetKeyDown(KeyCode.E)))
        {
            Game.Player.addSpecialIngredientForPlayer(item);


            Game.HUD.showHUD = true;
            Game.HUD.showInventory = true;
            Game.HUD.updateInventoryHUD();
        }
    }
}
