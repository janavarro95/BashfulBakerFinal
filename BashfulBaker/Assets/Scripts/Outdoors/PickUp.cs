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
    

    // Start is called before the first frame update
    void Start()
    {
        specItemInv = Game.Player.specialIngredientsInventory;
        item_ = new SpecialIngredient(item);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("hit a box");
        if ((InputControls.APressed || Input.GetKeyDown(KeyCode.E)))
        {
            Debug.Log("Pressed E");
            if (specItemInv.Count < specItemInv.maxCapaxity)
            {
                Debug.Log("adding item");
                specItemInv.Add(item_);
                Debug.Log("items: " + specItemInv.Count);
            }
            else
            {
                Debug.Log("full");
            }
        }
    }
}
