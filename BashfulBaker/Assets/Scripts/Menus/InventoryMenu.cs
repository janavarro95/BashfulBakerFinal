﻿using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;
using Assets.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

namespace Assets.Scripts.Menus
{
    /// <summary>
    /// TODO: 
    ///       Add in left bumper button.
    ///       Add in sound for selecting an ingredient.
    ///       Add in sound for menu open/close
    ///       Add in numbers on ingredients
    /// </summary>
    public class InventoryMenu :Menu
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


        private enum InventoryMenuState
        {
            CategoryMenu,
            LeftCategory,
            TopCategory,
            RightCategory,
            BottomCategory,
            DishCategory
        }

        private InventoryMenuState currentState;

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


            currentState = InventoryMenuState.CategoryMenu;
            setUpMenu();

            //Game.Player.inventory.Add(Ingredient.LoadIngredientFromPrefab("Cherries",1));
            //menuCursor = canvas.transform.Find("MenuMouseCursor").GetComponent<GameCursorMenu>();
            Game.Menu = this;
        }

        private void setUpMenu()
        {

            if (currentState== InventoryMenuState.DishCategory)
            {
                      

                dishCategory.SetActive(true);
                leftCategory.SetActive(false);
                rightCategory.SetActive(false);
                topCategory.SetActive(false);
                bottomCategory.SetActive(false);
                categorySelection.SetActive(false);

                getMenuComponents();

            }
            else if (currentState== InventoryMenuState.LeftCategory)
            {

                dishCategory.SetActive(false);
                leftCategory.SetActive(true);
                rightCategory.SetActive(false);
                topCategory.SetActive(false);
                bottomCategory.SetActive(false);
                categorySelection.SetActive(false);


                getMenuComponents();

                leftText.text = Game.Player.inventory.Contains("Dark Chocolate Chip") ? Game.Player.inventory.getItem("Dark Chocolate Chip").stack.ToString() : "0";
                topText.text = Game.Player.inventory.Contains("Milk Chocolate Chip") ? Game.Player.inventory.getItem("Milk Chocolate Chip").stack.ToString() : "0";
                rightText.text = Game.Player.inventory.Contains("White Chocolate Chip") ? Game.Player.inventory.getItem("White Chocolate Chip").stack.ToString() : "0";
                bottomText.text = Game.Player.inventory.Contains("Mint Chocolate Chip") ? Game.Player.inventory.getItem("Mint Chocolate Chip").stack.ToString() : "0";
                //Cocoa
                //Ginger
                //Matcha

            }
            else if(currentState == InventoryMenuState.RightCategory)
            {
                

                dishCategory.SetActive(false);
                leftCategory.SetActive(false);
                rightCategory.SetActive(true);
                topCategory.SetActive(false);
                bottomCategory.SetActive(false);
                categorySelection.SetActive(false);

                getMenuComponents();
            }
            else if(currentState == InventoryMenuState.TopCategory)
            {
                
                dishCategory.SetActive(false);
                leftCategory.SetActive(false);
                rightCategory.SetActive(false);
                topCategory.SetActive(true);
                bottomCategory.SetActive(false);
                categorySelection.SetActive(false);

                getMenuComponents();

                leftText.text = Game.Player.inventory.Contains("Cinnamon") ? Game.Player.inventory.getItem("Cinnamon").stack.ToString() : "0";
                topText.text = Game.Player.inventory.Contains("Cocoa") ? Game.Player.inventory.getItem("Cocoa").stack.ToString() : "0";
                rightText.text = Game.Player.inventory.Contains("Matcha") ? Game.Player.inventory.getItem("Matcha").stack.ToString() : "0";
                bottomText.text = Game.Player.inventory.Contains("Ginger") ? Game.Player.inventory.getItem("Ginger").stack.ToString() : "0";

            }
            else if(currentState == InventoryMenuState.BottomCategory)
            {
                
                dishCategory.SetActive(false);
                leftCategory.SetActive(false);
                rightCategory.SetActive(false);
                topCategory.SetActive(false);
                bottomCategory.SetActive(true);
                categorySelection.SetActive(false);

                getMenuComponents();
            }
            else if(currentState== InventoryMenuState.CategoryMenu)
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

        }

        private void getMenuComponents()
        {
            GameObject menuStuff = null;

            if (currentState == InventoryMenuState.BottomCategory)
            {
                menuStuff = bottomCategory;
            }
            else if (currentState == InventoryMenuState.CategoryMenu)
            {
                //menuStuff = categorySelection;
            }
            else if (currentState == InventoryMenuState.DishCategory)
            {
                menuStuff = dishCategory;
            }
            else if (currentState == InventoryMenuState.LeftCategory)
            {
                menuStuff = leftCategory;
            }
            else if (currentState == InventoryMenuState.RightCategory)
            {
                menuStuff = rightCategory;
            }
            else if (currentState == InventoryMenuState.TopCategory)
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

            maxPages = (items.Count / 4)+1;

            leftImage.color = new Color(1, 1, 1, 0);
            rightImage.color = new Color(1, 1, 1, 0);
            topImage.color = new Color(1, 1, 1, 0);
            bottomImage.color = new Color(1, 1, 1, 0);
            

            //left ingredient sprite
            if (items.Count > (0 + menuPage * 4))
            {
                leftImage.sprite = Sprite.Create(items[0 + (menuPage * 4)].sprite,leftImage.rectTransform.rect,leftImage.sprite.pivot);
                leftImage.color = Color.white;
                leftDish =(Dish)items[0 + (menuPage * 4)];
                leftText.text = leftDish.stack.ToString();
            }

            //right ingredient sprite
            if (items.Count > (1 + menuPage * 4))
            {
                rightImage.sprite = Sprite.Create(items[1 + (menuPage * 4)].sprite, rightImage.rectTransform.rect, rightImage.sprite.pivot);
                rightImage.color = Color.white;
                rightDish = (Dish)items[1 + (menuPage * 4)];
                rightText.text = rightDish.stack.ToString();
            }

            //Top ingredient sprite
            if (items.Count > (2 + menuPage * 4))
            {
                topImage.sprite = Sprite.Create(items[2 + (menuPage * 4)].sprite, topImage.rectTransform.rect, topImage.sprite.pivot);
                topImage.color = Color.white;
                topDish = (Dish)items[2+ (menuPage * 4)];
                topText.text = topDish.stack.ToString();
            }

            //Bottom ingredient sprite
            if (items.Count > (3 + menuPage * 4))
            {
                bottomImage.sprite = Sprite.Create(items[3 + (menuPage * 4)].sprite, bottomImage.rectTransform.rect, bottomImage.sprite.pivot);
                bottomImage.color = Color.white;
                bottomDish = (Dish)items[3 + (menuPage * 4)];
                bottomText.text = bottomDish.stack.ToString();
            }
        }

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
            if(currentState == InventoryMenuState.CategoryMenu)
            {
                if (GameInput.InputControls.RightBumperPressed)
                {
                    //go to dish menu
                    currentState = InventoryMenuState.DishCategory;
                    setUpMenu();
                    setDish();
                }
                if (InputControls.APressed)
                {
                    //bottom state
                    currentState = InventoryMenuState.BottomCategory;
                    setUpMenu();
                }
                if (InputControls.BPressed)
                {
                    //right state
                    currentState = InventoryMenuState.RightCategory;
                    setUpMenu();
                }
                if (InputControls.XPressed)
                {
                    //left
                    currentState = InventoryMenuState.LeftCategory;
                    setUpMenu();
                }
                if (InputControls.YPressed)
                {
                    //top
                    currentState = InventoryMenuState.TopCategory;
                    setUpMenu();
                }
                if (InputControls.StartPressed)
                {
                    exitMenu();
                }
            }

            if (currentState == InventoryMenuState.DishCategory)
            {
                if (GameInput.InputControls.RightBumperPressed)
                {
                    //if in category go back.
                    //if in category close menu?
                    currentState = InventoryMenuState.CategoryMenu;
                    setUpMenu();
                }

                if (GameInput.InputControls.APressed)
                {
                    if (bottomDish != null)
                    {
                        selectedDish = bottomDish;
                    }
                }
                if (GameInput.InputControls.BPressed)
                {
                    if (rightDish != null)
                    {
                        selectedDish = rightDish;
                    }
                }
                if (GameInput.InputControls.XPressed)
                {
                    if (leftDish != null)
                    {
                        selectedDish = leftDish;
                    }
                }
                if (GameInput.InputControls.YPressed)
                {
                    if (topDish != null)
                    {
                        selectedDish = topDish;
                    }
                }
            }

            if(currentState== InventoryMenuState.TopCategory)
            {
                if (GameInput.InputControls.RightBumperPressed)
                {
                    //if in category go back.
                    //if in category close menu?
                    currentState = InventoryMenuState.CategoryMenu;
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
            if (currentState == InventoryMenuState.BottomCategory)
            {
                if (GameInput.InputControls.RightBumperPressed)
                {
                    //if in category go back.
                    //if in category close menu?
                    currentState = InventoryMenuState.CategoryMenu;
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
            if (currentState == InventoryMenuState.LeftCategory)
            {
                if (GameInput.InputControls.RightBumperPressed)
                {
                    //if in category go back.
                    //if in category close menu?
                    currentState = InventoryMenuState.CategoryMenu;
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
            if (currentState == InventoryMenuState.RightCategory)
            {
                if (GameInput.InputControls.RightBumperPressed)
                {
                    //if in category go back.
                    //if in category close menu?
                    currentState = InventoryMenuState.CategoryMenu;
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
