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
    public string Name;
    public int max;
    private Game.IngSource mySource;
    private bool exists;
    public Sprite emptySprite;

    void Start()
    {
        exists = false;
        for (int x = 0; x < Game.Sources.Count; x++)
        {
            if(Game.Sources[x].name == Name)
            {
                mySource = Game.Sources[x];
                exists = true;
            }
        }
        
        specItemInv = Game.Player.specialIngredientsInventory;
        if (!exists)
        {
            mySource = new Game.IngSource(Name, max);
            Game.Sources.Add(mySource);
        }

        if (mySource.current >= max)
        {
            GetComponent<SpriteRenderer>().sprite = emptySprite;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && (InputControls.APressed || Input.GetKeyDown(KeyCode.E)))
        {
            if (mySource.current < max)
            {
                Game.Player.addSpecialIngredientForPlayer(item);
                mySource.current++;
            }
            if (mySource.current >= max)
            {
                GetComponent<SpriteRenderer>().sprite = emptySprite;
            }

            Game.HUD.showHUD = true;
            Game.HUD.showInventory = true;
            Game.HUD.updateInventoryHUD();
        }
    }
}
