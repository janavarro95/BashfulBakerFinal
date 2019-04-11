using Assets.Scripts.Menus;
using Assets.Scripts.Menus.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// The menu to deal with day selection.
/// </summary>
public class DaySelectMenu : Menu
{
    /// <summary>
    /// All the components for day selection buttons.
    /// </summary>
    public Dictionary<string, MenuComponent> daySelectionComponents;

    public override void Start()
    {
        Assets.Scripts.GameInformation.Game.Menu = this;
        daySelectionComponents = new Dictionary<string, MenuComponent>();
        setUpForSnapping();

    }

    /// <summary>
    /// Constantly checks for updates.
    /// </summary>
    public override void Update()
    {
        checkForInput();
    }

    private void checkForInput()
    {
        ///Checks for day selection buttons.
        foreach (KeyValuePair<string, MenuComponent> component in daySelectionComponents)
        {
            if (Assets.Scripts.GameInput.GameCursorMenu.SimulateMousePress(component.Value))
            {
                try
                {
                    SceneManager.LoadScene(component.Key);
                }
                catch(Exception err)
                {
                    //Debug.Log("Said scene doesn't exist yet!");
                }
            }
        }

        ///Checks for closing the menu.
        if (Assets.Scripts.GameInput.InputControls.StartPressed)
        {
            exitMenu();
            SceneManager.LoadScene("MainMenu");
        }
    }

    /// <summary>
    /// Close the menu.
    /// </summary>
    public override void exitMenu()
    {
        base.exitMenu();
        Assets.Scripts.GameInformation.Game.Menu = null;
    }

    /// <summary>
    /// Closes the game menu.
    /// </summary>
    /// <param name="playCloseSound"></param>
    public override void exitMenu(bool playCloseSound = true)
    {
        base.exitMenu(playCloseSound);
        Assets.Scripts.GameInformation.Game.Menu = null;
    }

    /// <summary>
    /// Sets up all of the components for snapping.
    /// </summary>
    public override void setUpForSnapping()
    {
        GameObject canvas = this.gameObject.transform.Find("Canvas").gameObject;
        GameObject background = canvas.transform.Find("Background").gameObject;
        daySelectionComponents.Add("Kitchen", new MenuComponent(background.transform.Find("Day1").Find("Image").GetComponent<Image>()));
        daySelectionComponents.Add("KitchenDay2", new MenuComponent(background.transform.Find("Day2").Find("Image").GetComponent<Image>()));
        daySelectionComponents.Add("KitchenDay3", new MenuComponent(background.transform.Find("Day3").Find("Image").GetComponent<Image>()));

        this.menuCursor = canvas.transform.Find("MenuMouseCursor").gameObject.GetComponent<Assets.Scripts.GameInput.GameCursorMenu>();
        this.selectedComponent = daySelectionComponents["Kitchen"];
        this.menuCursor.snapToCurrentComponent();

        daySelectionComponents["Kitchen"].setNeighbors(null,daySelectionComponents["KitchenDay3"],null,daySelectionComponents["KitchenDay2"]);
        daySelectionComponents["KitchenDay2"].setNeighbors(null, null, daySelectionComponents["Kitchen"], null);
        daySelectionComponents["KitchenDay3"].setNeighbors(daySelectionComponents["Kitchen"], null, null, null);
    }

    /// <summary>
    /// Checks if the menu is compatible with controller snapping.
    /// </summary>
    /// <returns></returns>
    public override bool snapCompatible()
    {
        return true;
    }
}
