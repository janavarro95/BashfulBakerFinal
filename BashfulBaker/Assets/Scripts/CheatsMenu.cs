using Assets.Scripts;
using Assets.Scripts.GameInformation;
using Assets.Scripts.Items;
using Assets.Scripts.Menus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatsMenu : Menu
{

    InputField speedInput;
    Dropdown cookieMode;
    public override void Start()
    {
        speedInput = this.gameObject.transform.Find("Canvas").Find("Background").Find("InputField").gameObject.GetComponent<InputField>();
        cookieMode = this.gameObject.transform.Find("Canvas").Find("Background").Find("Dropdown").gameObject.GetComponent<Dropdown>();
        speedInput.text = Game.Player.PlayerMovement.defaultSpeed.ToString();
    }

    public override void exitMenu()
    {
        base.exitMenu();
        Assets.Scripts.GameInformation.Game.Menu = null;
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitMenu();
        }
    }

    public override bool snapCompatible()
    {
        return false;
    }

    public override void setUpForSnapping()
    {
        
    }



    public void giveChocolateChip()
    {
        Game.Player.addSpecialIngredientForPlayer(Assets.Scripts.Enums.SpecialIngredients.ChocolateChips);
    }
    public void giveMintChip()
    {
        Game.Player.addSpecialIngredientForPlayer(Assets.Scripts.Enums.SpecialIngredients.MintChips);
    }
    public void givePecan()
    {
        Game.Player.addSpecialIngredientForPlayer(Assets.Scripts.Enums.SpecialIngredients.Pecans);
    }
    public void giveRaisin()
    {
        Game.Player.addSpecialIngredientForPlayer(Assets.Scripts.Enums.SpecialIngredients.Raisins);
    }

    public void giveChocolateChipCookies()
    {
        Dish d = new Dish(Enums.Dishes.ChocolateChipCookies,getState());
        Game.Player.dishesInventory.Add(d);
    }

    public void giveMintChipCookies()
    {
        Dish d = new Dish(Enums.Dishes.MintChipCookies, getState());
        Game.Player.dishesInventory.Add(d);
    }

    public void givePecanCookies()
    {
        Dish d = new Dish(Enums.Dishes.PecanCookies, getState());
        Game.Player.dishesInventory.Add(d);
    }

    public void giveORCookies()
    {
        Dish d = new Dish(Enums.Dishes.OatmealRaisinCookies, getState());
        Game.Player.dishesInventory.Add(d);
    }

    public void changePlayersSpeed()
    {
        Game.Player.PlayerMovement.defaultSpeed = (float)Convert.ToDouble(speedInput.text);
    }

    public Enums.DishState getState()
    {
        if (cookieMode.options[cookieMode.value].text == "Ingredients")
        {
            Debug.Log("ing");
            return Enums.DishState.Ingredients;
        }
        if (cookieMode.options[cookieMode.value].text == "Mixed")
        {
            return Enums.DishState.Mixed;
        }
        if (cookieMode.options[cookieMode.value].text == "Prepped")
        {
            return Enums.DishState.Prepped;
        }
        if (cookieMode.options[cookieMode.value].text == "Baked")
        {
            return Enums.DishState.Baked;
        }
        if (cookieMode.options[cookieMode.value].text == "Packaged")
        {
            return Enums.DishState.Packaged;
        }
        if (cookieMode.options[cookieMode.value].text == "Burnt")
        {
            return Enums.DishState.Burnt;
        }
        Debug.Log("fuuuu");
        return Enums.DishState.Ingredients;
    }
}
