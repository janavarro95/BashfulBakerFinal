﻿using Assets.Scripts.Menus;
using Assets.Scripts.Menus.Components;
using Assets.Scripts.QuestSystem.Quests;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Assets.Scripts.Menus
{

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

            if (GameInformation.Game.TutorialCompleted)
            {
                GameInformation.Game.HUD.InventoryHUD.showOnlySpecialIngredients();
                Debug.Log("SHOW THE INGREDIENTS");
            }
            else
            {
                Debug.Log("Tutorial not completed???");
            }
        }

        /// <summary>
        /// Constantly checks for updates.
        /// </summary>
        public override void Update()
        {
            checkForInput();
        }

        public void initializeImages()
        {
            foreach(KeyValuePair<string, MenuComponent> component in daySelectionComponents)
            {
                if (component.Key == "Kitchen")
                {
                    if (GameInformation.Game.DaysUnlocked[1] == true)
                    {
                        (component.Value.unityObject as Image).color = new Color(1f, 1f, 1f, 1);
                        
                    }
                    else
                    {
                        (component.Value.unityObject as Image).color = new Color(0.5f, 0.5f, 0.5f, 1);
                    }
                }
                else if (component.Key == "KitchenDay2")
                {
                    if (GameInformation.Game.DaysUnlocked[2] == true)
                    {
                        (component.Value.unityObject as Image).color = new Color(1f, 1f, 1f, 1);
                    }
                    else
                    {
                        (component.Value.unityObject as Image).color = new Color(0.5f, 0.5f, 0.5f, 1);
                    }
                }
                else if (component.Key == "KitchenDay3")
                {
                    if (GameInformation.Game.DaysUnlocked[3] == true)
                    {
                        Debug.Log("INIT");
                        (component.Value.unityObject as Image).color = new Color(1f, 1f, 1f, 1);
                    }
                    else
                    {
                        Debug.Log("INIT AHH");
                        (component.Value.unityObject as Image).color = new Color(0.5f, 0.5f, 0.5f, 1);
                    }
                }
                else if (component.Key == "KitchenDay4")
                {
                    if (GameInformation.Game.DaysUnlocked[4] == true)
                    {
                        (component.Value.unityObject as Image).color = new Color(1f, 1f, 1f, 1);
                    }
                    else
                    {
                        (component.Value.unityObject as Image).color = new Color(0.5f, 0.5f, 0.5f, 1);
                    }
                }
            }
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
                        if (specialPreDaySetUp(component.Key) == false)
                        {
                            return;
                        }
                        GameInformation.Game.Player.setSpriteVisibility(Enums.Visibility.Visible);
                        GameInformation.Game.Player.position = new Vector3(-3.2f, -9.5f, 0);
                        GameInformation.Game.HUD.showHUD = false;
                        GameInformation.Game.HUD.InventoryHUD.showAllComponents();
                        SceneManager.LoadScene(component.Key);
                    }
                    catch (Exception err)
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

        private bool specialPreDaySetUp(string componentName)
        {

            if (componentName == "Kitchen")
            {
                if (GameInformation.Game.DaysUnlocked[1] == false) return false;
                GameInformation.Game.CurrentDayNumber = 1;
            }

            if (componentName == "KitchenDay2")
            {
                if (GameInformation.Game.DaysUnlocked[2] == false) return false;
                GameInformation.Game.TutorialCompleted = true;
                GameInformation.Game.CurrentDayNumber = 2;


                Debug.Log("REMOVE THIS LATER!!!!!!!");
                GameInformation.Game.Player.addSpecialIngredientForPlayer(Enums.SpecialIngredients.ChocolateChips);
                GameInformation.Game.Player.addSpecialIngredientForPlayer(Enums.SpecialIngredients.MintChips);
                GameInformation.Game.Player.addSpecialIngredientForPlayer(Enums.SpecialIngredients.Pecans);
                GameInformation.Game.Player.addSpecialIngredientForPlayer(Enums.SpecialIngredients.Raisins);

                GameInformation.Game.QuestManager.addQuest(new CookingQuest("Chocolate Chip Cookies", "Sylvia", new List<string>()));
            }

            if (componentName == "KitchenDay3")
            {
                if (GameInformation.Game.DaysUnlocked[3] == false) return false;
            }
            if (componentName == "KitchenDay4")
            {
                if (GameInformation.Game.DaysUnlocked[4] == false) return false;
            }
            return true;
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
            daySelectionComponents.Add("KitchenDay4", new MenuComponent(background.transform.Find("Day4").Find("Image").GetComponent<Image>()));

            this.menuCursor = canvas.transform.Find("MenuMouseCursor").gameObject.GetComponent<Assets.Scripts.GameInput.GameCursorMenu>();
            this.selectedComponent = daySelectionComponents["Kitchen"];
            this.menuCursor.snapToCurrentComponent();

            daySelectionComponents["Kitchen"].setNeighbors(null, daySelectionComponents["KitchenDay3"], null, daySelectionComponents["KitchenDay2"]);
            daySelectionComponents["KitchenDay2"].setNeighbors(null, daySelectionComponents["KitchenDay4"], daySelectionComponents["Kitchen"], null);
            daySelectionComponents["KitchenDay3"].setNeighbors(daySelectionComponents["Kitchen"], null, null, daySelectionComponents["KitchenDay4"]);
            daySelectionComponents["KitchenDay4"].setNeighbors(daySelectionComponents["KitchenDay2"], null, daySelectionComponents["KitchenDay3"], null);

            initializeImages();
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
}