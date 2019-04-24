using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Items;
using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;

public class PickUp : MonoBehaviour
{
    public string item;
    private SpecialIngredient item_;
    Inventory specItemInv;

    /* Valid Inputs:
        * ChocolateChips
        * MintChips
        * Pecans
        * Raisins
        * Carrots
        * Strawberries
     */

    // Start is called before the first frame update
    void Start()
    {
        specItemInv = Game.Player.specialIngredientsInventory;
        item_ = new SpecialIngredient(item);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && (InputControls.APressed || Input.GetKeyDown(KeyCode.E)))
        {
            if (specItemInv.Count < specItemInv.maxCapaxity)
            {
                specItemInv.Add(item_);
                //Debug.Log("items: " + specItemInv.Count);
            }
            else
            {
                //Debug.Log("full");
            }
        }
    }
}
