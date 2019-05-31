﻿using Assets.Scripts.GameInformation;
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
    public class InventoryHUDV2 : HUD
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

        public GameObject specialIngredientsGameObject;

        public Image specialIngredientsIcon;

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


        public Text firstSpecialIngredientText;
        public Text secondSpecialIngredientText;
        public Text thirdSpecialIngredientText;
        public Text fourthSpecialIngredientText;

        [System.Serializable]
        private class SpecialIngredientRotation{

            /// <summary>
            /// The max rotation of the image.
            /// </summary>
            [SerializeField]
            private int maxRotation;
            /// <summary>
            /// How fast to rotate.
            /// </summary>
            [SerializeField]
            private float roationAmount;

            /// <summary>
            /// If the ingredient should update.
            /// </summary>
            public bool shouldUpdate;
            /// <summary>
            /// If the ingredient has done a full rotation wiggle.
            /// </summary>
            private bool hasDoneCycle;
            /// <summary>
            /// The rotational direction/
            /// </summary>
            private enum Direction
            {
                Left,
                Right
            }
            /// <summary>
            /// The direction to move.
            /// </summary>
            [SerializeField]
            private Direction dir;


            private Image image;
            /// <summary>
            /// Constructor.
            /// </summary>
            public SpecialIngredientRotation(Image img)
            {
                maxRotation = 20;
                roationAmount = 1;
                shouldUpdate = false;
                hasDoneCycle = false;
                this.image = img;
            }

            

            /// <summary>
            /// Constuctor.
            /// </summary>
            /// <param name="speed">How fast the rotation should happen.</param>
            public SpecialIngredientRotation(float speed)
            {
                maxRotation = 20;
                roationAmount = speed;
                shouldUpdate = false;
                hasDoneCycle = false;
            }
            /// <summary>
            /// Causes the special ingredient icon to do a wiggle motion.
            /// </summary>
            public void doCycle()
            {
                Quaternion quad = this.image.rectTransform.localRotation;
                Vector3 euler = quad.eulerAngles;
                euler.z = 0;
                quad.eulerAngles = euler;
                this.image.rectTransform.localRotation = quad;

                shouldUpdate = true;
                hasDoneCycle = false;
            }

            /// <summary>
            /// Update the rotation of the ingredient.
            /// </summary>
            /// <param name="img"></param>
            public void update()
            {
                if (shouldUpdate == false) return;
                rotate(this.image);
            }

            /// <summary>
            /// Rotate the icon.
            /// </summary>
            /// <param name="img"></param>
            private void rotate(Image img)
            {
                Quaternion quad = img.rectTransform.rotation;
                Vector3 angles = img.rectTransform.rotation.eulerAngles;
                quad.eulerAngles = new Vector3(angles.x, angles.y, (dir == Direction.Left) ? angles.z+ (-roationAmount) : angles.z+(roationAmount));

                if (quad.eulerAngles.z < 360 - maxRotation && quad.eulerAngles.z > 180)
                {
                    dir = Direction.Right;
                    Debug.Log("GO RIGHT!");
                }
                else if (quad.eulerAngles.z > maxRotation && quad.eulerAngles.z < 180)
                {
                    dir = Direction.Left;
                    Debug.Log("Go LEFT");
                    hasDoneCycle = true;
                }

                if(hasDoneCycle==true && quad.eulerAngles.z <= 0)
                {
                    hasDoneCycle = false;
                    shouldUpdate = false;
                    quad.eulerAngles = new Vector3(angles.x, angles.y, 0);
                }

                //Debug.Log(quad.eulerAngles);

                img.rectTransform.rotation = quad;
            }

        }


        /// <summary>
        /// Used to delay visual updates for the inventory.
        /// </summary>
        public DeltaTimer updateTimer;

        /// <summary>
        /// The index for where we are in the inventory.
        /// </summary>
        public int currentDishIndex;

        public TMPro.TextMeshProUGUI endText;


        [SerializeField]
        SpecialIngredientRotation sp1Rot;
        [SerializeField]
        SpecialIngredientRotation sp2Rot;
        [SerializeField]
        SpecialIngredientRotation sp3Rot;
        [SerializeField]
        SpecialIngredientRotation sp4Rot;

        [SerializeField]
        SpecialIngredientRotation basketRot;

        /// <summary>
        /// Initialize information.
        /// </summary>
        public override void Start()
        {
            canvas = this.gameObject.transform.Find("Canvas").gameObject;

            dishes = canvas.transform.Find("DishesView").gameObject;
            specialIngredients = canvas.transform.Find("SpecialIngredientsView").gameObject;
            specialIngredientsGameObject = canvas.transform.Find("SpecialIngredientsViewIcon").gameObject;
            specialIngredientsIcon = specialIngredientsGameObject.GetComponent<Image>();

            firstSpecialIngredientImage = specialIngredients.transform.Find("SpecialIngredientImage1").gameObject.GetComponent<Image>();
            secondSpecialIngredientImage = specialIngredients.transform.Find("SpecialIngredientImage2").gameObject.GetComponent<Image>();
            thirdSpecialIngredientImage = specialIngredients.transform.Find("SpecialIngredientImage3").gameObject.GetComponent<Image>();
            fourthSpecialIngredientImage = specialIngredients.transform.Find("SpecialIngredientImage4").gameObject.GetComponent<Image>();
            

            firstSpecialIngredientText = specialIngredients.gameObject.transform.Find("Amount1").GetComponent<Text>();
            secondSpecialIngredientText = specialIngredients.gameObject.transform.Find("Amount2").GetComponent<Text>();
            thirdSpecialIngredientText = specialIngredients.gameObject.transform.Find("Amount3").GetComponent<Text>();
            fourthSpecialIngredientText = specialIngredients.gameObject.transform.Find("Amount4").GetComponent<Text>();

            firstDishImage = dishes.transform.Find("DishImage1").gameObject.GetComponent<Image>();
            secondDishImage = dishes.transform.Find("DishImage2").gameObject.GetComponent<Image>();
            thirdDishImage = dishes.transform.Find("DishImage3").gameObject.GetComponent<Image>();
            fourthDishImage = dishes.transform.Find("DishImage4").gameObject.GetComponent<Image>();
            sp1Rot = new SpecialIngredientRotation(firstSpecialIngredientImage);
            sp2Rot = new SpecialIngredientRotation(secondSpecialIngredientImage);
            sp3Rot = new SpecialIngredientRotation(thirdSpecialIngredientImage);
            sp4Rot = new SpecialIngredientRotation(fourthSpecialIngredientImage);
            basketRot = new SpecialIngredientRotation(specialIngredientsIcon);

            endText = dishes.transform.Find("EndDay").GetComponent<TMPro.TextMeshProUGUI>();
            endText.gameObject.SetActive(false);

            firstDishImage.gameObject.SetActive(true);
            secondDishImage.gameObject.SetActive(true);
            thirdDishImage.gameObject.SetActive(true);
            fourthDishImage.gameObject.SetActive(true);

            setUpComponents();
            currentDishIndex = 0;

            /*
            sp1Rot.doCycle();
            sp2Rot.doCycle();
            sp3Rot.doCycle();
            sp4Rot.doCycle();
            sp5Rot.doCycle();
            sp6Rot.doCycle();
            basketRot.doCycle();
            */
        }

        public void setUpComponents()
        {
            List<Dish> dishesList = Game.Player.dishesInventory.getAllDishes();
            List<SpecialIngredient> specialIngredientsList = Game.Player.specialIngredientsInventory.getAllSpecialIngredients();

            firstSpecialIngredientImage.color = new Color(1, 1, 1, 0);
            secondSpecialIngredientImage.color = new Color(1, 1, 1, 0);
            thirdSpecialIngredientImage.color = new Color(1, 1, 1, 0);
            fourthSpecialIngredientImage.color = new Color(1, 1, 1, 0);
            firstDishImage.color = new Color(1, 1, 1, 0);
            secondDishImage.color = new Color(1, 1, 1, 0);
            thirdDishImage.color = new Color(1, 1, 1, 0);
            fourthDishImage.color = new Color(1, 1, 1, 0);

            this.dishes.SetActive(true);
            //this.specialIngredients.SetActive(true);

            //left ingredient sprite
            if (dishesList.Count > 0)
            {
                if (dishesList[0].currentDishState == Enums.DishState.Packaged)
                {
                    firstDishImage.rectTransform.sizeDelta = new Vector2(39, 38);
                }
                else
                {
                    firstDishImage.rectTransform.sizeDelta = new Vector2(64, 128);
                }

                Texture2D texture = dishesList[0].currentSprite;
                firstDishImage.sprite = Content.ContentManager.Instance.loadSprite(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16);
                firstDishImage.color = Color.white;
                firstDishImage.gameObject.SetActive(true);
                dishesList[0].loadSprite();
            }

            //right ingredient sprite
            if (dishesList.Count > 1)
            {
                if (dishesList[1].currentDishState == Enums.DishState.Packaged)
                {
                    secondDishImage.rectTransform.sizeDelta = new Vector2(39, 38);
                }
                else
                {
                    secondDishImage.rectTransform.sizeDelta = new Vector2(64, 128);
                }


                Texture2D texture = dishesList[1].currentSprite;
                secondDishImage.sprite = Content.ContentManager.Instance.loadSprite(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16);
                secondDishImage.color = Color.white;
                secondDishImage.gameObject.SetActive(true);
                dishesList[1].loadSprite();
            }

            //Top ingredient sprite
            if (dishesList.Count > 2)
            {
                if (dishesList[2].currentDishState == Enums.DishState.Packaged)
                {
                    thirdDishImage.rectTransform.sizeDelta = new Vector2(39, 38);
                }
                else
                {
                    thirdDishImage.rectTransform.sizeDelta = new Vector2(64, 128);
                }

                Texture2D texture = dishesList[2].currentSprite;
                thirdDishImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16);
                thirdDishImage.color = Color.white;
                thirdDishImage.gameObject.SetActive(true);
                dishesList[2].loadSprite();
            }

            //Bottom ingredient sprite
            if (dishesList.Count > (3))
            {
                if (dishesList[3].currentDishState == Enums.DishState.Packaged)
                {
                    fourthDishImage.rectTransform.sizeDelta = new Vector2(39, 38);
                }
                else
                {
                    fourthDishImage.rectTransform.sizeDelta = new Vector2(64, 128);
                }

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
                //Texture2D texture = firstSpecialIngredient.Sprite;
                //firstSpecialIngredientImage.sprite= Content.ContentManager.Instance.loadSprite(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16);
                firstSpecialIngredientImage.color = Color.white;
                
            }

            //right ingredient sprite
            if (specialIngredientsList.Count > 1)
            {
                secondSpecialIngredient = specialIngredientsList.Find(ing => ing.ingredientType == Enums.SpecialIngredients.MintChips);
                secondSpecialIngredientText.text = "x" + secondSpecialIngredient.stack.ToString();
                //Texture2D texture = secondSpecialIngredient.Sprite;
                //secondSpecialIngredientImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16);
                secondSpecialIngredientImage.color = Color.white;             
            }

            //Top ingredient sprite
            if (specialIngredientsList.Count > 2)
            {
                thirdSpecialIngredient = specialIngredientsList.Find(ing => ing.ingredientType == Enums.SpecialIngredients.Pecans);
                thirdSpecialIngredientText.text = "x" + thirdSpecialIngredient.stack.ToString();
                //Texture2D texture = thirdSpecialIngredient.Sprite;
                //thirdSpecialIngredientImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16);
                thirdSpecialIngredientImage.color = Color.white;
                
            }

            //Bottom ingredient sprite
            if (specialIngredientsList.Count > 3)
            {
                fourthSpecialIngredient = specialIngredientsList.Find(ing => ing.ingredientType == Enums.SpecialIngredients.Raisins);
                fourthSpecialIngredientText.text = "x" + fourthSpecialIngredient.stack.ToString();
                //Texture2D texture = fourthSpecialIngredient.Sprite;
                //fourthSpecialIngredientImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16);
                fourthSpecialIngredientImage.color = Color.white;
                
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

                    sp1Rot.update();
                    sp2Rot.update();
                    sp3Rot.update();
                    sp4Rot.update();
                    basketRot.update();

                    if (Game.QuestManager.completedAllQuests())
                    {
                        endText.gameObject.SetActive(true);
                    }
                    else
                    {
                        endText.gameObject.SetActive(false);
                    }
                }

                if (GameInput.InputControls.LeftTriggerPressed && Game.HUD.showInventory && Game.Player.dishesInventory.IsEmpty==false)
                {
                    updateCurrentDishIndex(-1);
                }
                else if (GameInput.InputControls.RightTriggerPressed && Game.HUD.showInventory && Game.Player.dishesInventory.IsEmpty == false)
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

                if (GameInput.InputControls.SelectPressed && Game.QuestManager.completedAllQuests())
                {
                    //DO NEW MENU!
                    Game.PhaseTimer.finish();
                }

            }

        }

        /// <summary>
        /// Update the current index for what dish the player is holding.
        /// </summary>
        /// <param name="amount"></param>
        public void updateCurrentDishIndex(int amount)
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
                    showOvenMitIcon();
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
                    showOvenMitIcon();
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
                    showOvenMitIcon();
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
                    showOvenMitIcon();
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

        /// <summary>
        /// Show the correct oven mit icons.
        /// </summary>
        private void showOvenMitIcon()
        {
            if (Game.Player.dishesInventory.Count == 0)
            {
                hideAllOvenMits();
                return;
            }
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

        /// <summary>
        /// Hides all of the oven mit sprites
        /// </summary>
        private void hideAllOvenMits()
        {
            firstDishImage.gameObject.transform.Find("OvenMit").gameObject.SetActive(false);
            secondDishImage.gameObject.transform.Find("OvenMit").gameObject.SetActive(false);
            thirdDishImage.gameObject.transform.Find("OvenMit").gameObject.SetActive(false);
            fourthDishImage.gameObject.transform.Find("OvenMit").gameObject.SetActive(false);
        }


        /// <summary>
        /// Rotate a special ingredient icon in the HUD.
        /// </summary>
        /// <param name="ingredient"></param>
        public void rotateSpecialIngredient(Enums.SpecialIngredients ingredient)
        {
            if (ingredient == 0)
            {
                sp1Rot.doCycle();
                basketRot.doCycle();
            }
            else if ((int)ingredient == 1)
            {
                sp2Rot.doCycle();
                basketRot.doCycle();
            }
            else if ((int)ingredient == 2)
            {
                sp3Rot.doCycle();
                basketRot.doCycle();
            }
            else if ((int)ingredient == 3)
            {
                sp4Rot.doCycle();
                basketRot.doCycle();
            }
        }
    }
}
