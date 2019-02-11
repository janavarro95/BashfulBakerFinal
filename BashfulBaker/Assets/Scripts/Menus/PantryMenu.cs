﻿using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;
using Assets.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    public class PantryMenu:Menu
    {
        public int menuPage;
        private int maxPages;

        Image leftImage;
        Image rightImage;
        Image topImage;
        Image bottomImage;
        Image centralImage;

        Text leftText;
        Text rightText;
        Text topText;
        Text bottomText;

        Dish leftDish;
        Dish rightDish;
        Dish topDish;
        Dish bottomDish;

        Ingredient selectedIngredient;

        Dish selectedDish;

        GameObject categorySelection;
        GameObject leftCategory;
        GameObject rightCategory;
        GameObject topCategory;
        GameObject bottomCategory;
        GameObject dishCategory;


        private enum PantryMenuState
        {
            CategoryMenu,
            LeftCategory,
            TopCategory,
            RightCategory,
            BottomCategory,
            DishCategory
        }

        private PantryMenuState currentState;

        /// <summary>
        /// Instantiate all menu logic here.
        /// </summary>
        public override void Start()
        {

            menuPage = 0;

            //initial set up
            GameObject canvas = this.transform.Find("Canvas").gameObject;
            dishCategory = canvas.gameObject.transform.Find("DishCategory").gameObject;
            leftCategory = canvas.gameObject.transform.Find("LeftCategory").gameObject;
            rightCategory = canvas.gameObject.transform.Find("RightCategory").gameObject;
            topCategory = canvas.gameObject.transform.Find("TopCategory").gameObject;
            bottomCategory = canvas.gameObject.transform.Find("BottomCategory").gameObject;
            categorySelection = canvas.gameObject.transform.Find("CategorySelection").gameObject;


            currentState = PantryMenuState.CategoryMenu;
            setUpMenu();

            //Game.Player.inventory.Add(Ingredient.LoadIngredientFromPrefab("Cherries",1));
            //menuCursor = canvas.transform.Find("MenuMouseCursor").GetComponent<GameCursorMenu>();
            Game.Menu = this;
        }

        private void setUpMenu()
        {

            if (currentState == PantryMenuState.LeftCategory)
            {

                dishCategory.SetActive(false);
                leftCategory.SetActive(true);
                rightCategory.SetActive(false);
                topCategory.SetActive(false);
                bottomCategory.SetActive(false);
                categorySelection.SetActive(false);


                getMenuComponents();

                leftText.text = Game.Pantry.inventory.Contains("Cinnamon") ? Game.Player.inventory.getItem("Cinnamon").stack.ToString() : "0";
                topText.text = Game.Pantry.inventory.Contains("Cocoa") ? Game.Player.inventory.getItem("Cocoa").stack.ToString() : "0";
                rightText.text = Game.Pantry.inventory.Contains("Matcha") ? Game.Player.inventory.getItem("Matcha").stack.ToString() : "0";
                bottomText.text = Game.Pantry.inventory.Contains("Ginger") ? Game.Player.inventory.getItem("Ginger").stack.ToString() : "0";
                //Cocoa
                //Ginger
                //Matcha

            }
            else if (currentState == PantryMenuState.RightCategory)
            {


                dishCategory.SetActive(false);
                leftCategory.SetActive(false);
                rightCategory.SetActive(true);
                topCategory.SetActive(false);
                bottomCategory.SetActive(false);
                categorySelection.SetActive(false);

                getMenuComponents();
            }
            else if (currentState == PantryMenuState.TopCategory)
            {

                dishCategory.SetActive(false);
                leftCategory.SetActive(false);
                rightCategory.SetActive(false);
                topCategory.SetActive(true);
                bottomCategory.SetActive(false);
                categorySelection.SetActive(false);

                getMenuComponents();

                leftText.text = Game.Pantry.inventory.Contains("Dark Chocolate Chip") ? Game.Player.inventory.getItem("Dark Chocolate Chip").stack.ToString() : "0";
                topText.text = Game.Pantry.inventory.Contains("Milk Chocolate Chip") ? Game.Player.inventory.getItem("Milk Chocolate Chip").stack.ToString() : "0";
                rightText.text = Game.Pantry.inventory.Contains("White Chocolate Chip") ? Game.Player.inventory.getItem("White Chocolate Chip").stack.ToString() : "0";
                bottomText.text = Game.Pantry.inventory.Contains("Mint Chocolate Chip") ? Game.Player.inventory.getItem("Mint Chocolate Chip").stack.ToString() : "0";
            }
            else if (currentState == PantryMenuState.BottomCategory)
            {

                dishCategory.SetActive(false);
                leftCategory.SetActive(false);
                rightCategory.SetActive(false);
                topCategory.SetActive(false);
                bottomCategory.SetActive(true);
                categorySelection.SetActive(false);

                getMenuComponents();
            }
            else if (currentState == PantryMenuState.CategoryMenu)
            {
                //do nothing???
                dishCategory.SetActive(false);
                leftCategory.SetActive(false);
                rightCategory.SetActive(false);
                topCategory.SetActive(false);
                bottomCategory.SetActive(false);
                categorySelection.SetActive(true);
                return;
            }
            else if(currentState == PantryMenuState.DishCategory)
            {
                dishCategory.SetActive(false);
                leftCategory.SetActive(false);
                rightCategory.SetActive(false);
                topCategory.SetActive(false);
                bottomCategory.SetActive(false);
                categorySelection.SetActive(false);
            }
        }

        private void getMenuComponents()
        {
            GameObject menuStuff = null;

            if (currentState == PantryMenuState.BottomCategory)
            {
                menuStuff = bottomCategory;
            }
            else if (currentState == PantryMenuState.CategoryMenu)
            {
                //menuStuff = categorySelection;
            }
            else if (currentState == PantryMenuState.LeftCategory)
            {
                menuStuff = leftCategory;
            }
            else if (currentState == PantryMenuState.RightCategory)
            {
                menuStuff = rightCategory;
            }
            else if (currentState == PantryMenuState.TopCategory)
            {
                menuStuff = topCategory;
            }
            else
            {
                menuStuff = dishCategory;
            }


            GameObject leftIngredient = menuStuff.gameObject.transform.Find("LeftIngredient").gameObject;
            GameObject rightIngredient = menuStuff.gameObject.transform.Find("RightIngredient").gameObject;
            GameObject topIngredient = menuStuff.gameObject.transform.Find("TopIngredient").gameObject;
            GameObject bottomIngredient = menuStuff.gameObject.transform.Find("BottomIngredient").gameObject;
            GameObject centralIngredient = menuStuff.gameObject.transform.Find("CentralIngredient").gameObject;

            leftImage = leftIngredient.GetComponent<Image>();
            rightImage = rightIngredient.GetComponent<Image>();
            topImage = topIngredient.GetComponent<Image>();
            bottomImage = bottomIngredient.GetComponent<Image>();
            centralImage = centralIngredient.GetComponent<Image>();

            leftText = leftIngredient.transform.Find("LeftText").GetComponent<Text>();
            rightText = rightIngredient.transform.Find("RightText").GetComponent<Text>();
            topText = topIngredient.transform.Find("TopText").GetComponent<Text>();
            bottomText = bottomIngredient.transform.Find("BottomText").GetComponent<Text>();
        }

        /*
        private void setDish()
        {
            centralImage.color = new Color(1, 1, 1, 0);

            topDish = null;
            bottomDish = null;
            leftDish = null;
            rightDish = null;

            leftText.text = "";
            rightText.text = "";
            topText.text = "";
            bottomText.text = "";

            List<Dish> items = Game.Player.inventory.getAllDishes();

            maxPages = (items.Count / 4) + 1;

            leftImage.color = new Color(1, 1, 1, 0);
            rightImage.color = new Color(1, 1, 1, 0);
            topImage.color = new Color(1, 1, 1, 0);
            bottomImage.color = new Color(1, 1, 1, 0);


            //left ingredient sprite
            if (items.Count > (0 + menuPage * 4))
            {
                leftImage.sprite = items[0 + (menuPage * 4)].sprite;
                leftImage.color = Color.white;
                leftDish = (Dish)items[0 + (menuPage * 4)];
                leftText.text = leftDish.stack.ToString();
            }

            //right ingredient sprite
            if (items.Count > (1 + menuPage * 4))
            {
                leftImage.sprite = items[1 + (menuPage * 4)].sprite;
                leftImage.color = Color.white;
                rightDish = (Dish)items[1 + (menuPage * 4)];
                rightText.text = rightDish.stack.ToString();
            }

            //Top ingredient sprite
            if (items.Count > (2 + menuPage * 4))
            {
                leftImage.sprite = items[2 + (menuPage * 4)].sprite;
                leftImage.color = Color.white;
                topDish = (Dish)items[2 + (menuPage * 4)];
                topText.text = topDish.stack.ToString();
            }

            //Bottom ingredient sprite
            if (items.Count > (3 + menuPage * 4))
            {
                leftImage.sprite = items[3 + (menuPage * 4)].sprite;
                leftImage.color = Color.white;
                bottomDish = (Dish)items[3 + (menuPage * 4)];
                bottomText.text = bottomDish.stack.ToString();
            }
        }
        */

        /// <summary>
        /// Runs ~60 times a second.
        /// </summary>
        public override void Update()
        {
            checkForInput();
        }

        /// <summary>
        /// Checks to see what buttons have been pressed.
        /// </summary>
        private void checkForInput()
        {
            if (currentState == PantryMenuState.CategoryMenu)
            {
                if (InputControls.APressed)
                {
                    //bottom state
                    currentState = PantryMenuState.BottomCategory;
                    setUpMenu();
                }
                if (InputControls.BPressed)
                {
                    //right state
                    currentState = PantryMenuState.RightCategory;
                    setUpMenu();
                }
                if (InputControls.XPressed)
                {
                    //left
                    currentState = PantryMenuState.LeftCategory;
                    setUpMenu();
                }
                if (InputControls.YPressed)
                {
                    //top
                    currentState = PantryMenuState.TopCategory;
                    setUpMenu();
                }
                if (InputControls.StartPressed)
                {
                    exitMenu();
                }
            }

            if (currentState == PantryMenuState.TopCategory)
            {
                if (GameInput.InputControls.RightBumperPressed)
                {
                    //if in category go back.
                    //if in category close menu?
                    currentState = PantryMenuState.CategoryMenu;
                    setUpMenu();
                }
                if (InputControls.APressed)
                {
                    //bottom state
                }
                if (InputControls.BPressed)
                {
                    //right state
                }
                if (InputControls.XPressed)
                {
                    //left
                }
                if (InputControls.YPressed)
                {
                    //top
                }
            }
            if (currentState == PantryMenuState.BottomCategory)
            {
                if (GameInput.InputControls.RightBumperPressed)
                {
                    //if in category go back.
                    //if in category close menu?
                    currentState = PantryMenuState.CategoryMenu;
                    setUpMenu();
                }
                if (InputControls.APressed)
                {
                    //bottom state
                }
                if (InputControls.BPressed)
                {
                    //right state
                }
                if (InputControls.XPressed)
                {
                    //left
                }
                if (InputControls.YPressed)
                {
                    //top
                }
            }
            if (currentState == PantryMenuState.LeftCategory)
            {
                if (GameInput.InputControls.RightBumperPressed)
                {
                    //if in category go back.
                    //if in category close menu?
                    currentState = PantryMenuState.CategoryMenu;
                    setUpMenu();
                }
                if (InputControls.APressed)
                {
                    //bottom state
                }
                if (InputControls.BPressed)
                {
                    //right state
                }
                if (InputControls.XPressed)
                {
                    //left
                }
                if (InputControls.YPressed)
                {
                    //top
                }
            }
            if (currentState == PantryMenuState.RightCategory)
            {
                if (GameInput.InputControls.RightBumperPressed)
                {
                    //if in category go back.
                    //if in category close menu?
                    currentState = PantryMenuState.CategoryMenu;
                    setUpMenu();
                }
                if (InputControls.APressed)
                {
                    //bottom state
                }
                if (InputControls.BPressed)
                {
                    //right state
                }
                if (InputControls.XPressed)
                {
                    //left
                }
                if (InputControls.YPressed)
                {
                    //top
                }
            }
            if(currentState == PantryMenuState.DishCategory)
            {
                return;
            }
        }

        /// <summary>
        /// Checks for and returns a selected ingredient and then closes the menu.
        /// </summary>
        /// <returns></returns>
        private System.Collections.IEnumerable selectIngredientCloseMenu()
        {
            if (selectedIngredient != null)
            {
                yield return selectedIngredient;
                selectedIngredient.removeFromStack(1);
                exitMenu();
            }
        }

        /// <summary>
        /// Wrapper to check which ingredient was selected and close the menu at the same time.
        /// </summary>
        /// <returns></returns>
        public Ingredient getSelectedIngredient()
        {
            return (Ingredient)selectIngredientCloseMenu();
        }

        /// <summary>
        /// Close the active menu.
        /// </summary>
        public override void exitMenu()
        {
            Destroy(this.gameObject);
            Game.Menu = null;
        }



    }
}
