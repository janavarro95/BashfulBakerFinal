﻿using Assets.Scripts.Cooking.Recipes;
using Assets.Scripts.GameInformation;
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

        Text leftPantryText;
        Text rightPantryText;
        Text topPantryText;
        Text bottomPantryText;

        GameObject categorySelectionPantry;
        GameObject leftCategoryPantry;
        GameObject rightCategoryPantry;
        GameObject topCategoryPantry;
        GameObject bottomCategoryPantry;


        Text leftPlayerText;
        Text rightPlayerText;
        Text topPlayerText;
        Text bottomPlayerText;

        GameObject categorySelectionPlayer;
        GameObject leftCategoryPlayer;
        GameObject rightCategoryPlayer;
        GameObject topCategoryPlayer;
        GameObject bottomCategoryPlayer;


        [SerializeField]
        Sprite leftArrowSprite;

        [SerializeField]
        Sprite rightArrowSprite;


        Image arrow;

        [SerializeField]
        Sprite tutorialCategorySprite;

        [SerializeField]
        Sprite nonTutorialCategorySprite;

        public bool isTutorial;

        private enum PantryItemMode
        {
            Take,
            Store
        }


        private enum PantryMenuState
        {
            Initialized,
            CategoryMenu,
            LeftCategory,
            TopCategory,
            RightCategory,
            BottomCategory,
            DishCategory
        }

        [SerializeField]
        private PantryMenuState currentState;
        [SerializeField]
        private PantryItemMode currentMode;

        /// <summary>
        /// Instantiate all menu logic here.
        /// </summary>
        public override void Start()
        {

            //initial set up
            GameObject canvas = this.transform.Find("Canvas").gameObject;

            GameObject pantrySide = canvas.transform.Find("PantrySide").gameObject;
            GameObject playerSide = canvas.transform.Find("PlayerSide").gameObject;
            arrow = canvas.transform.Find("ArrowMode").gameObject.GetComponent<Image>();


            leftCategoryPantry = pantrySide.gameObject.transform.Find("LeftCategory").gameObject;
            rightCategoryPantry = pantrySide.gameObject.transform.Find("RightCategory").gameObject;
            topCategoryPantry = pantrySide.gameObject.transform.Find("TopCategory").gameObject;
            bottomCategoryPantry = pantrySide.gameObject.transform.Find("BottomCategory").gameObject;
            categorySelectionPantry = pantrySide.gameObject.transform.Find("CategorySelect").gameObject;



            leftCategoryPlayer = playerSide.gameObject.transform.Find("LeftCategory").gameObject;
            rightCategoryPlayer = playerSide.gameObject.transform.Find("RightCategory").gameObject;
            topCategoryPlayer = playerSide.gameObject.transform.Find("TopCategory").gameObject;
            bottomCategoryPlayer = playerSide.gameObject.transform.Find("BottomCategory").gameObject;
            categorySelectionPlayer = playerSide.gameObject.transform.Find("CategorySelect").gameObject;


            currentState = PantryMenuState.Initialized;
            currentMode = PantryItemMode.Take;
            setUpMenu();

            //Game.Player.inventory.Add(Ingredient.LoadIngredientFromPrefab("Cherries",1));
            //menuCursor = canvas.transform.Find("MenuMouseCursor").GetComponent<GameCursorMenu>();
            Game.Menu = this;
        }

        private void setUpMenu()
        {
            if (isTutorial)
            {
                if (currentState == PantryMenuState.CategoryMenu)
                {
                    leftCategoryPantry.SetActive(false);
                    rightCategoryPantry.SetActive(false);
                    topCategoryPantry.SetActive(false);
                    bottomCategoryPantry.SetActive(false);
                    categorySelectionPantry.SetActive(true);

                    leftCategoryPlayer.SetActive(false);
                    rightCategoryPlayer.SetActive(false);
                    topCategoryPlayer.SetActive(false);
                    bottomCategoryPlayer.SetActive(false);
                    categorySelectionPlayer.SetActive(true);

                    arrow.enabled = false;

                    GameObject canvas = this.transform.Find("Canvas").gameObject;
                    GameObject pantrySide = canvas.transform.Find("PantrySide").gameObject;
                    GameObject categorySelect = pantrySide.transform.Find("CategorySelect").gameObject;
                    Image img = categorySelect.transform.Find("PantryBackgroundImage").gameObject.GetComponent<Image>();
                    img.sprite = tutorialCategorySprite;


                    GameObject playerSide = canvas.transform.Find("PlayerSide").gameObject;
                    categorySelect = playerSide.transform.Find("CategorySelect").gameObject;
                    img = categorySelect.transform.Find("PantryBackgroundImage").gameObject.GetComponent<Image>();
                    img.sprite = tutorialCategorySprite;

                    //Debug.Log("NANI???");

                    return;
                }
                else if (currentState == PantryMenuState.RightCategory)
                {
                    leftCategoryPantry.SetActive(false);
                    rightCategoryPantry.SetActive(true);
                    topCategoryPantry.SetActive(false);
                    bottomCategoryPantry.SetActive(false);
                    categorySelectionPantry.SetActive(false);


                    leftCategoryPlayer.SetActive(false);
                    rightCategoryPlayer.SetActive(true);
                    topCategoryPlayer.SetActive(false);
                    bottomCategoryPlayer.SetActive(false);
                    categorySelectionPlayer.SetActive(false);

                    arrow.enabled = true;

                    getMenuComponents();

                    rightPantryText.text = "";//Game.Pantry.getIngredientsCountForRecipes("Chocolate Chip Cookie").ToString();
                    rightPlayerText.text = "";//Game.Player.getIngredientsCountForRecipes("Chocolate Chip Cookie").ToString();

                    bottomPantryText.text = "";
                    bottomPlayerText.text = "";
                    topPantryText.text = "";
                    topPlayerText.text = "";
                    leftPlayerText.text = "";
                    leftPantryText.text = "";

                }
                if (currentMode == PantryItemMode.Store)
                {
                    arrow.sprite = leftArrowSprite;
                }
                else if (currentMode == PantryItemMode.Take)
                {
                    arrow.sprite = rightArrowSprite;
                }

                return;
            }

            if (currentState == PantryMenuState.LeftCategory)
            {
                leftCategoryPantry.SetActive(true);
                rightCategoryPantry.SetActive(false);
                topCategoryPantry.SetActive(false);
                bottomCategoryPantry.SetActive(false);
                categorySelectionPantry.SetActive(false);

                leftCategoryPlayer.SetActive(true);
                rightCategoryPlayer.SetActive(false);
                topCategoryPlayer.SetActive(false);
                bottomCategoryPlayer.SetActive(false);
                categorySelectionPlayer.SetActive(false);

                arrow.enabled = true;

                getMenuComponents();

                leftPantryText.text = "";// Game.Pantry.inventory.Contains("Dark Chocolate Chip") ? Game.Pantry.inventory.getItem("Dark Chocolate Chip").stack.ToString() : "0";
                topPantryText.text = "";// Game.Pantry.inventory.Contains("Milk Chocolate Chip") ? Game.Pantry.inventory.getItem("Milk Chocolate Chip").stack.ToString() : "0";
                rightPantryText.text = "";// Game.Pantry.inventory.Contains("White Chocolate Chip") ? Game.Pantry.inventory.getItem("White Chocolate Chip").stack.ToString() : "0";
                bottomPantryText.text = "";// Game.Pantry.inventory.Contains("Mint Chocolate Chip") ? Game.Pantry.inventory.getItem("Mint Chocolate Chip").stack.ToString() : "0";

                leftPlayerText.text = "";// Game.Player.dishesInventory.Contains("Dark Chocolate Chip") ? Game.Player.dishesInventory.getItem("Dark Chocolate Chip").stack.ToString() : "0";
                topPlayerText.text = "";// Game.Player.dishesInventory.Contains("Milk Chocolate Chip") ? Game.Player.dishesInventory.getItem("Milk Chocolate Chip").stack.ToString() : "0";
                rightPlayerText.text = "";//Game.Player.dishesInventory.Contains("White Chocolate Chip") ? Game.Player.dishesInventory.getItem("White Chocolate Chip").stack.ToString() : "0";
                bottomPlayerText.text = "";//Game.Player.dishesInventory.Contains("Mint Chocolate Chip") ? Game.Player.dishesInventory.getItem("Mint Chocolate Chip").stack.ToString() : "0";

                //Cocoa
                //Ginger
                //Matcha

            }
            else if (currentState == PantryMenuState.RightCategory)
            {
                leftCategoryPantry.SetActive(false);
                rightCategoryPantry.SetActive(true);
                topCategoryPantry.SetActive(false);
                bottomCategoryPantry.SetActive(false);
                categorySelectionPantry.SetActive(false);


                leftCategoryPlayer.SetActive(false);
                rightCategoryPlayer.SetActive(true);
                topCategoryPlayer.SetActive(false);
                bottomCategoryPlayer.SetActive(false);
                categorySelectionPlayer.SetActive(false);

                arrow.enabled = true;

                getMenuComponents();

                topPantryText.text = "";//Game.Pantry.inventory.Contains("Chocolate Chip Cookie Ingredients") ? Game.Pantry.inventory.getItem("Chocolate Chip Cookie Ingredients").stack.ToString() : "0";
                topPlayerText.text = "";//Game.Player.dishesInventory.Contains("Chocolate Chip Cookie Ingredients") ? Game.Player.dishesInventory.getItem("Chocolate Chip Cookie Ingredients").stack.ToString() : "0";
            }
            else if (currentState == PantryMenuState.TopCategory)
            {
                leftCategoryPantry.SetActive(false);
                rightCategoryPantry.SetActive(false);
                topCategoryPantry.SetActive(true);
                bottomCategoryPantry.SetActive(false);
                categorySelectionPantry.SetActive(false);

                leftCategoryPlayer.SetActive(false);
                rightCategoryPlayer.SetActive(false);
                topCategoryPlayer.SetActive(true);
                bottomCategoryPlayer.SetActive(false);
                categorySelectionPlayer.SetActive(false);

                arrow.enabled = true;

                getMenuComponents();

                leftPantryText.text = "";// Game.Pantry.inventory.Contains("Cinnamon") ? Game.Pantry.inventory.getItem("Cinnamon").stack.ToString() : "0";
                topPantryText.text = "";// Game.Pantry.inventory.Contains("Cocoa") ? Game.Pantry.inventory.getItem("Cocoa").stack.ToString() : "0";
                rightPantryText.text = "";//Game.Pantry.inventory.Contains("Matcha") ? Game.Pantry.inventory.getItem("Matcha").stack.ToString() : "0";
                bottomPantryText.text = "";// Game.Pantry.inventory.Contains("Ginger") ? Game.Pantry.inventory.getItem("Ginger").stack.ToString() : "0";

                leftPlayerText.text = "";//Game.Player.dishesInventory.Contains("Cinnamon") ? Game.Player.dishesInventory.getItem("Cinnamon").stack.ToString() : "0";
                topPlayerText.text = "";//Game.Player.dishesInventory.Contains("Cocoa") ? Game.Player.dishesInventory.getItem("Cocoa").stack.ToString() : "0";
                rightPlayerText.text = "";// Game.Player.dishesInventory.Contains("Matcha") ? Game.Player.dishesInventory.getItem("Matcha").stack.ToString() : "0";
                bottomPlayerText.text = "";// Game.Player.dishesInventory.Contains("Ginger") ? Game.Player.dishesInventory.getItem("Ginger").stack.ToString() : "0";

            }
            else if (currentState == PantryMenuState.BottomCategory)
            {

                leftCategoryPantry.SetActive(false);
                rightCategoryPantry.SetActive(false);
                topCategoryPantry.SetActive(false);
                bottomCategoryPantry.SetActive(true);
                categorySelectionPantry.SetActive(false);

                leftCategoryPlayer.SetActive(false);
                rightCategoryPlayer.SetActive(false);
                topCategoryPlayer.SetActive(false);
                bottomCategoryPlayer.SetActive(true);
                categorySelectionPlayer.SetActive(false);

                arrow.enabled = true;

                getMenuComponents();
            }
            else if (currentState == PantryMenuState.CategoryMenu)
            {
                leftCategoryPantry.SetActive(false);
                rightCategoryPantry.SetActive(false);
                topCategoryPantry.SetActive(false);
                bottomCategoryPantry.SetActive(false);
                categorySelectionPantry.SetActive(true);

                leftCategoryPlayer.SetActive(false);
                rightCategoryPlayer.SetActive(false);
                topCategoryPlayer.SetActive(false);
                bottomCategoryPlayer.SetActive(false);
                categorySelectionPlayer.SetActive(true);

                arrow.enabled = false;

                return;
            }
            else if(currentState == PantryMenuState.DishCategory)
            {
                leftCategoryPantry.SetActive(false);
                rightCategoryPantry.SetActive(false);
                topCategoryPantry.SetActive(false);
                bottomCategoryPantry.SetActive(false);
                categorySelectionPantry.SetActive(false);

                leftCategoryPlayer.SetActive(false);
                rightCategoryPlayer.SetActive(false);
                topCategoryPlayer.SetActive(false);
                bottomCategoryPlayer.SetActive(false);
                categorySelectionPlayer.SetActive(false);

                arrow.enabled = false;
            }

            if(currentMode== PantryItemMode.Store)
            {
                arrow.sprite = leftArrowSprite;
            }
            else if(currentMode == PantryItemMode.Take)
            {
                arrow.sprite = rightArrowSprite;
            }

        }

        private void getMenuComponents()
        {

            if (currentState == PantryMenuState.BottomCategory)
            {
                leftPantryText = bottomCategoryPantry.transform.Find("LeftText").GetComponent<Text>();
                rightPantryText = bottomCategoryPantry.transform.Find("RightText").GetComponent<Text>();
                topPantryText = bottomCategoryPantry.transform.Find("TopText").GetComponent<Text>();
                bottomPantryText = bottomCategoryPantry.transform.Find("BottomText").GetComponent<Text>();

                leftPlayerText = bottomCategoryPlayer.transform.Find("LeftText").GetComponent<Text>();
                rightPlayerText = bottomCategoryPlayer.transform.Find("RightText").GetComponent<Text>();
                topPlayerText = bottomCategoryPlayer.transform.Find("TopText").GetComponent<Text>();
                bottomPlayerText = bottomCategoryPlayer.transform.Find("BottomText").GetComponent<Text>();
            }
            else if (currentState == PantryMenuState.CategoryMenu)
            {
                //menuStuff = categorySelection;

            }
            else if (currentState == PantryMenuState.LeftCategory)
            {
                leftPantryText = leftCategoryPantry.transform.Find("LeftText").GetComponent<Text>();
                rightPantryText = leftCategoryPantry.transform.Find("RightText").GetComponent<Text>();
                topPantryText = leftCategoryPantry.transform.Find("TopText").GetComponent<Text>();
                bottomPantryText = leftCategoryPantry.transform.Find("BottomText").GetComponent<Text>();

                leftPlayerText = leftCategoryPlayer.transform.Find("LeftText").GetComponent<Text>();
                rightPlayerText = leftCategoryPlayer.transform.Find("RightText").GetComponent<Text>();
                topPlayerText = leftCategoryPlayer.transform.Find("TopText").GetComponent<Text>();
                bottomPlayerText = leftCategoryPlayer.transform.Find("BottomText").GetComponent<Text>();
            }
            else if (currentState == PantryMenuState.RightCategory)
            {
                leftPantryText = rightCategoryPantry.transform.Find("LeftText").GetComponent<Text>();
                rightPantryText = rightCategoryPantry.transform.Find("RightText").GetComponent<Text>();
                topPantryText = rightCategoryPantry.transform.Find("TopText").GetComponent<Text>();
                bottomPantryText = rightCategoryPantry.transform.Find("BottomText").GetComponent<Text>();

                leftPlayerText = rightCategoryPlayer.transform.Find("LeftText").GetComponent<Text>();
                rightPlayerText = rightCategoryPlayer.transform.Find("RightText").GetComponent<Text>();
                topPlayerText = rightCategoryPlayer.transform.Find("TopText").GetComponent<Text>();
                bottomPlayerText = rightCategoryPlayer.transform.Find("BottomText").GetComponent<Text>();
            }
            else if (currentState == PantryMenuState.TopCategory)
            {
                leftPantryText = topCategoryPantry.transform.Find("LeftText").GetComponent<Text>();
                rightPantryText = topCategoryPantry.transform.Find("RightText").GetComponent<Text>();
                topPantryText = topCategoryPantry.transform.Find("TopText").GetComponent<Text>();
                bottomPantryText = topCategoryPantry.transform.Find("BottomText").GetComponent<Text>();

                leftPlayerText = topCategoryPlayer.transform.Find("LeftText").GetComponent<Text>();
                rightPlayerText = topCategoryPlayer.transform.Find("RightText").GetComponent<Text>();
                topPlayerText = topCategoryPlayer.transform.Find("TopText").GetComponent<Text>();
                bottomPlayerText = topCategoryPlayer.transform.Find("BottomText").GetComponent<Text>();
            }
            else
            {
                return;
            }

            

           
        }


        /// <summary>
        /// Runs ~60 times a second.
        /// </summary>
        public override void Update()
        {
            checkForInput();
            if( currentState== PantryMenuState.Initialized)
            {
                currentState = PantryMenuState.CategoryMenu;
                setUpMenu();
            }
        }

        /// <summary>
        /// Checks to see what buttons have been pressed.
        /// </summary>
        private void checkForInput()
        {

            if (isTutorial)
            {
                if(currentState== PantryMenuState.CategoryMenu)
                {

                    if (InputControls.StartPressed)
                    {
                        exitMenu();
                    }
                    if (InputControls.BPressed)
                    {
                        //right state
                        currentState = PantryMenuState.RightCategory;
                        setUpMenu();
                    }
                    return;
                }
                if(currentState== PantryMenuState.RightCategory)
                {
                    if (InputControls.BPressed)
                    {
                        //top
                        if (currentMode == PantryItemMode.Take)
                        {
                            
                        }
                        if (currentMode == PantryItemMode.Store)
                        {
                            
                        }
                        setUpMenu();
                    }
                }

                if (GameInput.InputControls.LeftTrigger > 0)
                {
                    currentMode = PantryItemMode.Store;
                    setUpMenu();
                }
                if (GameInput.InputControls.RightTrigger > 0)
                {
                    currentMode = PantryItemMode.Take;
                    setUpMenu();
                }

                return;
            }


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
                return;
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

                    if (currentMode == PantryItemMode.Take)
                    {
                        
                    }
                    if (currentMode == PantryItemMode.Store)
                    {
                        
                    }

                }
                if (InputControls.BPressed)
                {
                    if (currentMode == PantryItemMode.Take)
                    {
                        
                    }
                    if (currentMode == PantryItemMode.Store)
                    {
                        
                    }
                }
                if (InputControls.XPressed)
                {
                    //left
                    //left
                    if (currentMode == PantryItemMode.Take)
                    {
                        
                    }
                    if (currentMode == PantryItemMode.Store)
                    {
                        
                    }
                }
                if (InputControls.YPressed)
                {
                    //top
                    if (currentMode == PantryItemMode.Take)
                    {
                       
                    }
                    if (currentMode == PantryItemMode.Store)
                    {
                        
                    }
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
                    if (currentMode == PantryItemMode.Take)
                    {

                    }
                    if (currentMode == PantryItemMode.Store)
                    {

                    }
                }
                if (InputControls.BPressed)
                {
                    //right state
                    if (currentMode == PantryItemMode.Take)
                    {

                    }
                    if (currentMode == PantryItemMode.Store)
                    {

                    }
                }
                if (InputControls.XPressed)
                {
                    //left
                    if (currentMode == PantryItemMode.Take)
                    {

                    }
                    if (currentMode == PantryItemMode.Store)
                    {

                    }
                }
                if (InputControls.YPressed)
                {
                    //top
                    if (currentMode == PantryItemMode.Take)
                    {

                    }
                    if (currentMode == PantryItemMode.Store)
                    {

                    }
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
                    //left
                    if (currentMode == PantryItemMode.Take)
                    {
                        
                    }
                    if (currentMode == PantryItemMode.Store)
                    {
                        
                    }
                    //setUpMenu();
                }
                if (InputControls.BPressed)
                {
                    //left
                    if (currentMode == PantryItemMode.Take)
                    {
                        
                    }
                    if (currentMode == PantryItemMode.Store)
                    {
                        
                    }
                }
                if (InputControls.XPressed)
                {
                    //left
                    if (currentMode == PantryItemMode.Take)
                    {
                        
                    }
                    if (currentMode == PantryItemMode.Store)
                    {
                        
                    }
                    setUpMenu();
                }
                if (InputControls.YPressed)
                {
                    //left
                    if (currentMode == PantryItemMode.Take)
                    {
                        
                    }
                    if (currentMode == PantryItemMode.Store)
                    {
                        
                    }
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
                    if (currentMode == PantryItemMode.Take)
                    {

                    }
                    if (currentMode == PantryItemMode.Store)
                    {

                    }
                }
                if (InputControls.BPressed)
                {
                    //right state
                    if (currentMode == PantryItemMode.Take)
                    {

                    }
                    if (currentMode == PantryItemMode.Store)
                    {

                    }
                }
                if (InputControls.XPressed)
                {
                    //left
                    if (currentMode == PantryItemMode.Take)
                    {

                    }
                    if (currentMode == PantryItemMode.Store)
                    {

                    }
                }
                if (InputControls.YPressed)
                {
                    //top
                    if (currentMode == PantryItemMode.Take)
                    {
                       
                    }
                    if (currentMode == PantryItemMode.Store)
                    {
                        
                    }
                    setUpMenu();

                }
            }
            if(currentState == PantryMenuState.DishCategory)
            {
                return;
            }

            if (GameInput.InputControls.LeftTrigger>0)
            {
                currentMode = PantryItemMode.Store;
                setUpMenu();
            }
            if (GameInput.InputControls.RightTrigger>0)
            {
                currentMode = PantryItemMode.Take;
                setUpMenu();
            }
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
