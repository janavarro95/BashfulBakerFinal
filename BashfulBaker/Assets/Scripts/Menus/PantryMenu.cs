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

        private PantryMenuState currentState;
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

                leftPantryText.text = Game.Pantry.inventory.Contains("Dark Chocolate Chip") ? Game.Pantry.inventory.getItem("Dark Chocolate Chip").stack.ToString() : "0";
                topPantryText.text = Game.Pantry.inventory.Contains("Milk Chocolate Chip") ? Game.Pantry.inventory.getItem("Milk Chocolate Chip").stack.ToString() : "0";
                rightPantryText.text = Game.Pantry.inventory.Contains("White Chocolate Chip") ? Game.Pantry.inventory.getItem("White Chocolate Chip").stack.ToString() : "0";
                bottomPantryText.text = Game.Pantry.inventory.Contains("Mint Chocolate Chip") ? Game.Pantry.inventory.getItem("Mint Chocolate Chip").stack.ToString() : "0";

                leftPlayerText.text = Game.Player.inventory.Contains("Dark Chocolate Chip") ? Game.Player.inventory.getItem("Dark Chocolate Chip").stack.ToString() : "0";
                topPlayerText.text = Game.Player.inventory.Contains("Milk Chocolate Chip") ? Game.Player.inventory.getItem("Milk Chocolate Chip").stack.ToString() : "0";
                rightPlayerText.text = Game.Player.inventory.Contains("White Chocolate Chip") ? Game.Player.inventory.getItem("White Chocolate Chip").stack.ToString() : "0";
                bottomPlayerText.text = Game.Player.inventory.Contains("Mint Chocolate Chip") ? Game.Player.inventory.getItem("Mint Chocolate Chip").stack.ToString() : "0";

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

                leftPantryText.text = Game.Pantry.inventory.Contains("Cinnamon") ? Game.Pantry.inventory.getItem("Cinnamon").stack.ToString() : "0";
                topPantryText.text = Game.Pantry.inventory.Contains("Cocoa") ? Game.Pantry.inventory.getItem("Cocoa").stack.ToString() : "0";
                rightPantryText.text = Game.Pantry.inventory.Contains("Matcha") ? Game.Pantry.inventory.getItem("Matcha").stack.ToString() : "0";
                bottomPantryText.text = Game.Pantry.inventory.Contains("Ginger") ? Game.Pantry.inventory.getItem("Ginger").stack.ToString() : "0";

                leftPlayerText.text = Game.Player.inventory.Contains("Cinnamon") ? Game.Player.inventory.getItem("Cinnamon").stack.ToString() : "0";
                topPlayerText.text = Game.Player.inventory.Contains("Cocoa") ? Game.Player.inventory.getItem("Cocoa").stack.ToString() : "0";
                rightPlayerText.text = Game.Player.inventory.Contains("Matcha") ? Game.Player.inventory.getItem("Matcha").stack.ToString() : "0";
                bottomPlayerText.text = Game.Player.inventory.Contains("Ginger") ? Game.Player.inventory.getItem("Ginger").stack.ToString() : "0";

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
            if( currentState== PantryMenuState.Initialized)
            {
                currentState = PantryMenuState.CategoryMenu;
            }
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
                        Game.Pantry.takeOne("Ginger");
                    }
                    if (currentMode == PantryItemMode.Store)
                    {
                        Game.Pantry.storeOne("Ginger");
                    }

                }
                if (InputControls.BPressed)
                {
                    if (currentMode == PantryItemMode.Take)
                    {
                        Game.Pantry.takeOne("Matcha");
                    }
                    if (currentMode == PantryItemMode.Store)
                    {
                        Game.Pantry.storeOne("Matcha");
                    }
                }
                if (InputControls.XPressed)
                {
                    //left
                    //left
                    if (currentMode == PantryItemMode.Take)
                    {
                        Game.Pantry.takeOne("Cinnamon");
                    }
                    if (currentMode == PantryItemMode.Store)
                    {
                        Game.Pantry.storeOne("Cinnamon");
                    }
                }
                if (InputControls.YPressed)
                {
                    //top
                    if (currentMode == PantryItemMode.Take)
                    {
                        Game.Pantry.takeOne("Cocoa");
                    }
                    if (currentMode == PantryItemMode.Store)
                    {
                        Game.Pantry.storeOne("Cocoa");
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
                        Game.Pantry.takeOne("Mint Chocolate Chip");
                    }
                    if (currentMode == PantryItemMode.Store)
                    {
                        Game.Pantry.storeOne("Mint Chocolate Chip");
                    }
                    //setUpMenu();
                }
                if (InputControls.BPressed)
                {
                    //left
                    if (currentMode == PantryItemMode.Take)
                    {
                        Game.Pantry.takeOne("White Chocolate Chip");
                    }
                    if (currentMode == PantryItemMode.Store)
                    {
                        Game.Pantry.storeOne("Whitex Chocolate Chip");
                    }
                }
                if (InputControls.XPressed)
                {
                    //left
                    if (currentMode == PantryItemMode.Take)
                    {
                        Game.Pantry.takeOne("Dark Chocolate Chip");
                    }
                    if (currentMode == PantryItemMode.Store)
                    {
                        Game.Pantry.storeOne("Dark Chocolate Chip");
                    }
                    setUpMenu();
                }
                if (InputControls.YPressed)
                {
                    //left
                    if (currentMode == PantryItemMode.Take)
                    {
                        Game.Pantry.takeOne("Milk Chocolate Chip");
                    }
                    if (currentMode == PantryItemMode.Store)
                    {
                        Game.Pantry.storeOne("Milk Chocolate Chip");
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
