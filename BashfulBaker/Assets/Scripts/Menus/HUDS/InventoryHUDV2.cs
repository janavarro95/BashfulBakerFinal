using Assets.Scripts.GameInformation;
using Assets.Scripts.Items;
using Assets.Scripts.Utilities.Timers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus.HUDS
{
    /// <summary>
    /// Deals with managing all UI with the inventory!
    /// </summary>
    public class InventoryHUDV2:HUD
    {
        /// <summary>
        /// The canvas!
        /// </summary>
        GameObject canvas;

        /// <summary>
        /// The UI element representing the dishes.
        /// </summary>
        public GameObject dishes;
        /// <summary>
        /// The UI element representing the special ingredients.
        /// </summary>
        public GameObject specialIngredients;

        public GameObject specialIngredientsIcon;

        /// <summary>
        /// The first special ingredient.
        /// </summary>
        private SpecialIngredient firstSpecialIngredient;
        /// <summary>
        /// The second special ingredient.
        /// </summary>
        private SpecialIngredient secondSpecialIngredient;
        /// <summary>
        /// The 3rd special ingredient.
        /// </summary>
        private SpecialIngredient thirdSpecialIngredient;
        /// <summary>
        /// The 4th special ingredient.
        /// </summary>
        private SpecialIngredient fourthSpecialIngredient;
        /// <summary>
        /// The 5th special ingredient.
        /// </summary>
        private SpecialIngredient fifthSpecialIngredient;

        /// <summary>
        /// The 6th special ingredient object.
        /// </summary>
        private SpecialIngredient sixthSpecialIngredient;

        //Images
        /// <summary>
        /// The image representing the 1st dish.
        /// </summary>
        public Image firstDishImage;
        /// <summary>
        /// The image representing the 2nd dish.
        /// </summary>
        public Image secondDishImage;
        /// <summary>
        /// The image representing the 3rd dish.
        /// </summary>
        public Image thirdDishImage;
        /// <summary>
        /// The image representing the 4th dish.
        /// </summary>
        public Image fourthDishImage;

        /// <summary>
        /// The image representing the 1st special ingredient.
        /// </summary>
        public Image firstSpecialIngredientImage;
        /// <summary>
        /// The image representing the 2nd special ingredient.
        /// </summary>
        public Image secondSpecialIngredientImage;
        /// <summary>
        /// The image representing the 3rd special ingredient.
        /// </summary>
        public Image thirdSpecialIngredientImage;
        /// <summary>
        /// The image representing the 4th special ingredient.
        /// </summary>
        public Image fourthSpecialIngredientImage;
        /// <summary>
        /// The image representing the 5th special ingredient.
        /// </summary>
        public Image fifthSpecialIngredientImage;
        /// <summary>
        /// The image representing the 6th special ingredient.
        /// </summary>
        public Image sixthSpecialIngredientImage;


        public Text firstSpecialIngredientText;
        public Text secondSpecialIngredientText;
        public Text thirdSpecialIngredientText;
        public Text fourthSpecialIngredientText;
        public Text fifthSpecialIngredientText;
        public Text sixthSpecialIngredientText;


        /// <summary>
        /// Used to delay visual updates for the inventory.
        /// </summary>
        public DeltaTimer updateTimer;

        /// <summary>
        /// The index for where we are in the inventory.
        /// </summary>
        public int currentDishIndex;

        /// <summary>
        /// Initialize information.
        /// </summary>
        public override void Start()
        {
            canvas = this.gameObject.transform.Find("Canvas").gameObject;

            dishes = canvas.transform.Find("DishesView").gameObject;
            specialIngredients = canvas.transform.Find("SpecialIngredientsView").gameObject;
            specialIngredientsIcon = canvas.transform.Find("SpecialIngredientsViewIcon").gameObject;
            setUpComponents();
            currentDishIndex = 0;
        }

        public void setUpComponents()
        {
            firstSpecialIngredientImage = specialIngredients.transform.Find("SpecialIngredientImage1").gameObject.GetComponent<Image>();
            secondSpecialIngredientImage = specialIngredients.transform.Find("SpecialIngredientImage2").gameObject.GetComponent<Image>();
            thirdSpecialIngredientImage = specialIngredients.transform.Find("SpecialIngredientImage3").gameObject.GetComponent<Image>();
            fourthSpecialIngredientImage = specialIngredients.transform.Find("SpecialIngredientImage4").gameObject.GetComponent<Image>();
            fifthSpecialIngredientImage = specialIngredients.transform.Find("SpecialIngredientImage5").gameObject.GetComponent<Image>();
            sixthSpecialIngredientImage = specialIngredients.transform.Find("SpecialIngredientImage6").gameObject.GetComponent<Image>();


            firstSpecialIngredientText = firstSpecialIngredientImage.gameObject.transform.Find("Amount").GetComponent<Text>();
            secondSpecialIngredientText = secondSpecialIngredientImage.gameObject.transform.Find("Amount").GetComponent<Text>();
            thirdSpecialIngredientText = thirdSpecialIngredientImage.gameObject.transform.Find("Amount").GetComponent<Text>();
            fourthSpecialIngredientText = fourthSpecialIngredientImage.gameObject.transform.Find("Amount").GetComponent<Text>();
            fifthSpecialIngredientText = fifthSpecialIngredientImage.gameObject.transform.Find("Amount").GetComponent<Text>();
            sixthSpecialIngredientText = sixthSpecialIngredientImage.gameObject.transform.Find("Amount").GetComponent<Text>();

            firstDishImage =dishes.transform.Find("DishImage1").gameObject.GetComponent<Image>();
            secondDishImage = dishes.transform.Find("DishImage2").gameObject.GetComponent<Image>();
            thirdDishImage = dishes.transform.Find("DishImage3").gameObject.GetComponent<Image>();
            fourthDishImage = dishes.transform.Find("DishImage4").gameObject.GetComponent<Image>();


            List<Dish> dishesList = Game.Player.dishesInventory.getAllDishes();
            List<SpecialIngredient> specialIngredientsList = Game.Player.specialIngredientsInventory.getAllSpecialIngredients();

            firstSpecialIngredientImage.color = new Color(1, 1, 1, 0);
            secondSpecialIngredientImage.color = new Color(1, 1, 1, 0);
            thirdSpecialIngredientImage.color = new Color(1, 1, 1, 0);
            fourthSpecialIngredientImage.color = new Color(1, 1, 1, 0);
            fifthSpecialIngredientImage.color = new Color(1, 1, 1, 0);
            sixthSpecialIngredientImage.color = new Color(1, 1, 1, 0);

            firstDishImage.color = new Color(1, 1, 1, 0);
            secondDishImage.color = new Color(1, 1, 1, 0);
            thirdDishImage.color = new Color(1, 1, 1, 0);
            fourthDishImage.color = new Color(1, 1, 1, 0);

            this.dishes.SetActive(true);
            this.specialIngredients.SetActive(true);

            //left ingredient sprite
            if (dishesList.Count > 0)
            {
                Texture2D texture = dishesList[0].currentSprite;
                firstDishImage.sprite = Content.ContentManager.Instance.loadSprite(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16);
                firstDishImage.color = Color.white;
                firstDishImage.gameObject.SetActive(true);
                dishesList[0].loadSprite();
            }

            //right ingredient sprite
            if (dishesList.Count > 1)
            {
                Texture2D texture = dishesList[1].currentSprite;
                secondDishImage.sprite = Content.ContentManager.Instance.loadSprite(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16);
                secondDishImage.color = Color.white;
                secondDishImage.gameObject.SetActive(true);
                dishesList[1].loadSprite();
            }

            //Top ingredient sprite
            if (dishesList.Count > 2)
            {
                Texture2D texture = dishesList[2].currentSprite;
                thirdDishImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16);
                thirdDishImage.color = Color.white;
                thirdDishImage.gameObject.SetActive(true);
                dishesList[2].loadSprite();
            }

            //Bottom ingredient sprite
            if (dishesList.Count > (3))
            {
                Texture2D texture = dishesList[3].currentSprite;
                fourthDishImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16);
                fourthDishImage.color = Color.white;
                fourthDishImage.gameObject.SetActive(true);
                dishesList[3].loadSprite();
            }


            //left ingredient sprite
            if (specialIngredientsList.Count > 0)
            {
                firstSpecialIngredient = specialIngredientsList.Find(ing => ing.ingredientType == Enums.SpecialIngredients.ChocolateChips);
                firstSpecialIngredientText.text = "x" + firstSpecialIngredient.stack.ToString();
                Texture2D texture = firstSpecialIngredient.Sprite;
                firstSpecialIngredientImage.sprite= Content.ContentManager.Instance.loadSprite(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16);
                firstSpecialIngredientImage.color = Color.white;
                
            }

            //right ingredient sprite
            if (specialIngredientsList.Count > 1)
            {
                secondSpecialIngredient = specialIngredientsList.Find(ing => ing.ingredientType == Enums.SpecialIngredients.MintChips);
                secondSpecialIngredientText.text = "x" + secondSpecialIngredient.stack.ToString();
                Texture2D texture = secondSpecialIngredient.Sprite;
                secondSpecialIngredientImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16);
                secondSpecialIngredientImage.color = Color.white;             
            }

            //Top ingredient sprite
            if (specialIngredientsList.Count > 2)
            {
                thirdSpecialIngredient = specialIngredientsList.Find(ing => ing.ingredientType == Enums.SpecialIngredients.Pecans);
                thirdSpecialIngredientText.text = "x" + thirdSpecialIngredient.stack.ToString();
                Texture2D texture = thirdSpecialIngredient.Sprite;
                thirdSpecialIngredientImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16);
                thirdSpecialIngredientImage.color = Color.white;
                
            }

            //Bottom ingredient sprite
            if (specialIngredientsList.Count > 3)
            {
                fourthSpecialIngredient = specialIngredientsList.Find(ing => ing.ingredientType == Enums.SpecialIngredients.Raisins);
                fourthSpecialIngredientText.text = "x" + fourthSpecialIngredient.stack.ToString();
                Texture2D texture = fourthSpecialIngredient.Sprite;
                fourthSpecialIngredientImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16);
                fourthSpecialIngredientImage.color = Color.white;
                
            }

            if (specialIngredientsList.Count > 4)
            {
                fifthSpecialIngredient = specialIngredientsList.Find(ing => ing.ingredientType == Enums.SpecialIngredients.Carrots);
                fifthSpecialIngredientText.text = "x" + fifthSpecialIngredient.stack.ToString();
                Texture2D texture = fifthSpecialIngredient.Sprite;
                fifthSpecialIngredientImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16);
                fifthSpecialIngredientImage.color = Color.white;
               
            }

            if (specialIngredientsList.Count > 5)
            {
                sixthSpecialIngredient = specialIngredientsList.Find(ing => ing.ingredientType == Enums.SpecialIngredients.Strawberries);
                sixthSpecialIngredientText.text = "x" + sixthSpecialIngredient.stack.ToString();
                Texture2D texture = sixthSpecialIngredient.Sprite;
                sixthSpecialIngredientImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16);
                sixthSpecialIngredientImage.color = Color.white;             
            }

            foreach(Dish d in Game.Player.dishesInventory)
            {
                d.updateSprite();
            }

        }

        /// <summary>
        /// Called roughly 60~ times a second.
        /// </summary>
        public override void Update()
        {
            if (updateTimer == null || updateTimer.state == Enums.TimerState.Initialized)
            {
                updateTimer = new DeltaTimer(1, Enums.TimerType.CountDown, true, updateDishes);
                updateTimer.start();
            }
            else
            {
                if (Game.HUD.showHUD && Game.HUD.showInventory)
                {
                    updateTimer.Update();
                }

                if (GameInput.InputControls.LeftTriggerPressed && Game.HUD.showInventory)
                {
                    updateCurrentDishIndex(-1);
                }
                else if (GameInput.InputControls.RightTriggerPressed && Game.HUD.showInventory)
                {
                    updateCurrentDishIndex(1);
                }

                if (GameInput.InputControls.LeftBumperPressed && this.specialIngredients.activeInHierarchy==false)
                {
                    this.specialIngredients.SetActive(true);
                    return;
                }
                else if(GameInput.InputControls.LeftBumperPressed && this.specialIngredients.activeInHierarchy == true)
                {
                    this.specialIngredients.SetActive(false);
                    return;
                }
                updateCurrentDishIndex(0);
            }

        }

        /// <summary>
        /// Update the current index for what dish the player is holding.
        /// </summary>
        /// <param name="amount"></param>
        private void updateCurrentDishIndex(int amount)
        {
            currentDishIndex += amount;
            if (currentDishIndex <= -1)
            {
                currentDishIndex = 3;
            }
            if (currentDishIndex >= 4)
            {
                currentDishIndex = 0;
            }

            if (currentDishIndex == 0)
            {
                try
                {
                    Game.Player.activeItem = Game.Player.dishesInventory.actualItems.ElementAt(0);
                    showOvenMitIcon();
                }
                catch(Exception err)
                {
                    Game.Player.activeItem = null;
                    hidAllOvenMits();
                }
            }
            else if (currentDishIndex == 1)
            {
                try
                {
                    Game.Player.activeItem = Game.Player.dishesInventory.actualItems.ElementAt(1);
                    showOvenMitIcon();
                }
                catch(Exception err)
                {
                    Game.Player.activeItem = null;
                    hidAllOvenMits();
                }
            }
            else if (currentDishIndex == 2)
            {
                try
                {
                    Game.Player.activeItem = Game.Player.dishesInventory.actualItems.ElementAt(2);
                    showOvenMitIcon();
                }
                catch(Exception err)
                {
                    Game.Player.activeItem = null;
                    hidAllOvenMits();
                }
            }
            else if (currentDishIndex == 3)
            {
                try
                {
                    Game.Player.activeItem = Game.Player.dishesInventory.actualItems.ElementAt(3);
                    showOvenMitIcon();
                }
                catch(Exception err)
                {
                    Game.Player.activeItem = null;
                    hidAllOvenMits();
                }
            }
            Game.Player.updateHeldItemSprite();
            updateDishes();
        }

        public void resetActiveDish()
        {
            updateCurrentDishIndex(0);
        }

        /// <summary>
        /// Update the visual components for dishes.
        /// </summary>
        public void updateDishes()
        {
            ///Debug.Log("Update dishes!");
            setUpComponents();
        }

        /// <summary>
        /// Sets the visibility of the HUD menu.
        /// </summary>
        /// <param name="visibility"></param>
        public override void setVisibility(Enums.Visibility visibility)
        {
            if (visibility == Enums.Visibility.Invisible) canvas.SetActive(false);
            if (visibility == Enums.Visibility.Visible) canvas.SetActive(true);
        }

        public void showEverything()
        {
            this.canvas.SetActive(true);
            showAllComponents();
        }

        public void showAllComponents()
        {
            foreach(Transform t in this.canvas.transform)
            {
                t.gameObject.SetActive(true);
            }
        }

        public void showOnlySpecialIngredients()
        {
            Game.HUD.showHUD = true;
            Game.HUD.showSpecialIngredients = true;
            this.dishes.SetActive(false);
            this.specialIngredients.SetActive(true);
            this.specialIngredientsIcon.SetActive(true);
        }


        private void showOvenMitIcon()
        {
            if (currentDishIndex == 0)
            {
                firstDishImage.gameObject.transform.Find("OvenMit").gameObject.SetActive(true);
                secondDishImage.gameObject.transform.Find("OvenMit").gameObject.SetActive(false);
                thirdDishImage.gameObject.transform.Find("OvenMit").gameObject.SetActive(false);
                fourthDishImage.gameObject.transform.Find("OvenMit").gameObject.SetActive(false);
            }
            if (currentDishIndex == 1)
            {
                firstDishImage.gameObject.transform.Find("OvenMit").gameObject.SetActive(false);
                secondDishImage.gameObject.transform.Find("OvenMit").gameObject.SetActive(true);
                thirdDishImage.gameObject.transform.Find("OvenMit").gameObject.SetActive(false);
                fourthDishImage.gameObject.transform.Find("OvenMit").gameObject.SetActive(false);
            }
            if (currentDishIndex == 2)
            {
                firstDishImage.gameObject.transform.Find("OvenMit").gameObject.SetActive(false);
                secondDishImage.gameObject.transform.Find("OvenMit").gameObject.SetActive(false);
                thirdDishImage.gameObject.transform.Find("OvenMit").gameObject.SetActive(true);
                fourthDishImage.gameObject.transform.Find("OvenMit").gameObject.SetActive(false);
            }
            if (currentDishIndex == 3)
            {
                firstDishImage.gameObject.transform.Find("OvenMit").gameObject.SetActive(false);
                secondDishImage.gameObject.transform.Find("OvenMit").gameObject.SetActive(false);
                thirdDishImage.gameObject.transform.Find("OvenMit").gameObject.SetActive(false);
                fourthDishImage.gameObject.transform.Find("OvenMit").gameObject.SetActive(true);
            }
        }
        private void hidAllOvenMits()
        {
            firstDishImage.gameObject.transform.Find("OvenMit").gameObject.SetActive(false);
            secondDishImage.gameObject.transform.Find("OvenMit").gameObject.SetActive(false);
            thirdDishImage.gameObject.transform.Find("OvenMit").gameObject.SetActive(false);
            fourthDishImage.gameObject.transform.Find("OvenMit").gameObject.SetActive(false);
        }
    }
}
