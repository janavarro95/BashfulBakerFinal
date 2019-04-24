using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;
using Assets.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

namespace Assets.Scripts.Menus
{
    /// <summary>
    /// TODO: 
    ///       Add in sound for selecting an ingredient.
    ///       Add in sound for menu open/close
    /// </summary>
    public class InventoryMenu :Menu
    {

        Image leftImage;
        Image rightImage;
        Image topImage;
        Image bottomImage;
        Image centralImage;

        GameObject inventory;

        Item leftItem;
        Item rightItem;
        Item topItem;
        Item bottomItem;
        Item selectedDish;

        Item leftIngredient;
        Item rightIngredient;
        Item topIngredient;
        Item bottomIngredient;
        Item selectedIngredient;

        public Enums.InventoryViewMode currentViewMode;

        /// <summary>
        /// Instantiate all menu logic here.
        /// </summary>
        public override void Start()
        {

            //initial set up
            GameObject canvas = this.transform.Find("Canvas").gameObject;

            inventory = canvas.gameObject.transform.Find("Inventory").gameObject;
            

            //Game.Player.inventory.Add(Ingredient.LoadIngredientFromPrefab("Cherries",1));
            //menuCursor = canvas.transform.Find("MenuMouseCursor").GetComponent<GameCursorMenu>();
            Game.Menu = this;

            Game.HUD.showInventory = false;

            //this.currentViewMode = Game.HUD.InventoryHUD.currentMode;

            setUpMenu();

        }

        public void swapMode()
        {
            if (this.currentViewMode == Enums.InventoryViewMode.DishView)
            {
                this.currentViewMode = Enums.InventoryViewMode.SpecialIngredientView;
                setUpMenu();
                return;
            }
            else if (this.currentViewMode == Enums.InventoryViewMode.SpecialIngredientView)
            {
                this.currentViewMode = Enums.InventoryViewMode.DishView;
                setUpMenu();
                return;
            }
        }

        private void setUpMenu()
        {
            getMenuComponents();
            if (currentViewMode == Enums.InventoryViewMode.DishView)
            {
                setDish();
            }
            else
            {
                setSpecialIngredients();
            }
        }

        private void getMenuComponents()
        {

            GameObject leftIngredient = inventory.gameObject.transform.Find("LeftIngredient").gameObject;
            GameObject rightIngredient = inventory.gameObject.transform.Find("RightIngredient").gameObject;
            GameObject topIngredient = inventory.gameObject.transform.Find("TopIngredient").gameObject;
            GameObject bottomIngredient = inventory.gameObject.transform.Find("BottomIngredient").gameObject;
            GameObject centralIngredient = inventory.gameObject.transform.Find("CentralIngredient").gameObject;

            leftImage = leftIngredient.GetComponent<Image>();
            rightImage = rightIngredient.GetComponent<Image>();
            topImage = topIngredient.GetComponent<Image>();
            bottomImage = bottomIngredient.GetComponent<Image>();
            centralImage = centralIngredient.GetComponent<Image>();

        }


        private void setDish()
        {
            

            topItem = null;
            bottomItem = null;
            leftItem = null;
            rightItem = null;



            List<Dish> items = Game.Player.dishesInventory.getAllDishes();

          

            
            leftImage.color = new Color(1, 1, 1, 0);
            rightImage.color = new Color(1, 1, 1, 0);
            topImage.color = new Color(1, 1, 1, 0);
            bottomImage.color = new Color(1, 1, 1, 0);
            centralImage.color = new Color(1, 1, 1, 0);
            

            //left ingredient sprite
            if (items.Count > 0)
            {
                leftImage.sprite = Content.ContentManager.Instance.loadSprite(items[0].Sprite,new Rect(0,0,32,32),new Vector2(0.5f,0.5f),16);
                leftImage.color = Color.white;
                leftItem =items[0];
            }

            //right ingredient sprite
            if (items.Count > 1)
            {
                rightImage.sprite = Sprite.Create(items[1].Sprite, new Rect(0, 0, 32, 32), rightImage.sprite.pivot);
                rightImage.color = Color.white;
                rightItem = items[1];
            }

            //Top ingredient sprite
            if (items.Count > 2)
            {
                topImage.sprite = Sprite.Create(items[2].Sprite, new Rect(0, 0, 32, 32), topImage.sprite.pivot);
                topImage.color = Color.white;
                topItem = items[2];
            }

            //Bottom ingredient sprite
            if (items.Count > 3)
            {
                
                bottomImage.sprite = Sprite.Create(items[3].Sprite, new Rect(0, 0, 32, 32), bottomImage.sprite.pivot);
                bottomImage.color = Color.white;
                bottomItem = items[3];
            }

            if (Game.Player.activeItem != null)
            {
                this.selectedDish =(Dish) Game.Player.activeItem;
                centralImage.sprite = Content.ContentManager.Instance.loadSprite(selectedDish.Sprite, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f), 16);
                centralImage.color = new Color(1, 1, 1, 1);
            }
        }

        private void setSpecialIngredients()
        {
            topIngredient = null;
            bottomIngredient = null;
            leftIngredient = null;
            rightIngredient = null;


            List<SpecialIngredient> items = Game.Player.specialIngredientsInventory.getAllSpecialIngredients();

            leftImage.color = new Color(1, 1, 1, 0);
            rightImage.color = new Color(1, 1, 1, 0);
            topImage.color = new Color(1, 1, 1, 0);
            bottomImage.color = new Color(1, 1, 1, 0);
            centralImage.color = new Color(1, 1, 1, 0);


            //left ingredient sprite
            if (items.Count > 0)
            {
                leftImage.sprite = Content.ContentManager.Instance.loadSprite(items[0].Sprite, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f), 16);
                leftImage.color = Color.white;
                leftItem = items[0];
            }

            //right ingredient sprite
            if (items.Count > 1)
            {
                rightImage.sprite = Sprite.Create(items[1].Sprite, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f), 16);
                rightImage.color = Color.white;
                rightItem = items[1];
            }

            //Top ingredient sprite
            if (items.Count > 2)
            {
                topImage.sprite = Sprite.Create(items[2].Sprite, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f), 16);
                topImage.color = Color.white;
                topItem = items[2];
            }

            //Bottom ingredient sprite
            if (items.Count > 3)
            {

                bottomImage.sprite = Sprite.Create(items[3].Sprite, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f), 16);
                bottomImage.color = Color.white;
                bottomItem = items[3];
            }

            if (Game.Player.activeItem != null&&Game.Player.activeItem.GetType()==typeof(SpecialIngredient))
            {
                //Debug.Log("Reset active ingredient");
                this.selectedIngredient = (SpecialIngredient)Game.Player.activeItem;
                centralImage.sprite = Content.ContentManager.Instance.loadSprite(selectedIngredient.Sprite, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f), 16);
                centralImage.color = new Color(1, 1, 1, 1);
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
            if (this.currentViewMode == Enums.InventoryViewMode.DishView)
            {
                if (InputControls.APressed)
                {
                    if (bottomItem == null)
                    {
                        centralImage.color = new Color(1, 1, 1, 0);
                        selectedDish = null;
                        return;
                    }
                    //bottom state
                    selectedDish = bottomItem;
                    centralImage.sprite = Content.ContentManager.Instance.loadSprite(selectedDish.Sprite, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f), 16);
                    centralImage.color = new Color(1, 1, 1, 1);
                    Game.SoundEffects.playItemSelectSound();
                }
                if (InputControls.BPressed)
                {
                    if (rightItem == null)
                    {
                        centralImage.color = new Color(1, 1, 1, 0);
                        selectedDish = null;
                        return;
                    }
                    //right state
                    selectedDish = rightItem;
                    centralImage.sprite = Content.ContentManager.Instance.loadSprite(selectedDish.Sprite, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f), 16);
                    centralImage.color = new Color(1, 1, 1, 1);
                    Game.SoundEffects.playItemSelectSound();
                }
                if (InputControls.XPressed)
                {
                    if (leftItem == null)
                    {
                        centralImage.color = new Color(1, 1, 1, 0);
                        selectedDish = null;
                        return;
                    }
                    //left
                    selectedDish = leftItem;
                    centralImage.sprite = Content.ContentManager.Instance.loadSprite(selectedDish.Sprite, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f), 16);
                    centralImage.color = new Color(1, 1, 1, 1);
                    Game.SoundEffects.playItemSelectSound();
                }
                if (InputControls.YPressed)
                {
                    if (topItem == null)
                    {
                        centralImage.color = new Color(1, 1, 1, 0);
                        selectedDish = null;
                        return;
                    }
                    //top
                    selectedDish = topItem;
                    centralImage.sprite = Content.ContentManager.Instance.loadSprite(selectedDish.Sprite, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f), 16);
                    centralImage.color = new Color(1, 1, 1, 1);
                    Game.SoundEffects.playItemSelectSound();
                }
            }
            else if(this.currentViewMode== Enums.InventoryViewMode.SpecialIngredientView)
            {
                if (InputControls.APressed)
                {
                    if (bottomIngredient == null)
                    {
                        centralImage.color = new Color(1, 1, 1, 0);
                        selectedIngredient = null;
                        return;
                    }
                    //bottom state
                    selectedIngredient = bottomIngredient;
                    centralImage.sprite = Content.ContentManager.Instance.loadSprite(selectedIngredient.Sprite, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f), 16);
                    centralImage.color = new Color(1, 1, 1, 1);
                    Game.SoundEffects.playItemSelectSound();
                }
                if (InputControls.BPressed)
                {
                    if (rightIngredient == null)
                    {
                        centralImage.color = new Color(1, 1, 1, 0);
                        selectedIngredient = null;
                        return;
                    }
                    //right state
                    selectedIngredient = rightIngredient;
                    centralImage.sprite = Content.ContentManager.Instance.loadSprite(selectedIngredient.Sprite, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f), 16);
                    centralImage.color = new Color(1, 1, 1, 1);
                    Game.SoundEffects.playItemSelectSound();
                }
                if (InputControls.XPressed)
                {
                    if (leftIngredient == null)
                    {
                        centralImage.color = new Color(1, 1, 1, 0);
                        selectedIngredient = null;
                        return;
                    }
                    //left
                    selectedIngredient = leftIngredient;
                    centralImage.sprite = Content.ContentManager.Instance.loadSprite(selectedIngredient.Sprite, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f), 16);
                    centralImage.color = new Color(1, 1, 1, 1);
                    Game.SoundEffects.playItemSelectSound();
                }
                if (InputControls.YPressed)
                {
                    if (topIngredient == null)
                    {
                        centralImage.color = new Color(1, 1, 1, 0);
                        selectedIngredient = null;
                        return;
                    }
                    //top
                    selectedIngredient = topIngredient;
                    centralImage.sprite = Content.ContentManager.Instance.loadSprite(selectedIngredient.Sprite, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f), 16);
                    centralImage.color = new Color(1, 1, 1, 1);
                    Game.SoundEffects.playItemSelectSound();
                }
            }

            if (InputControls.StartPressed)
            {
                exitMenu();
                Game.HUD.showInventory = true;
            }
            if (InputControls.RightBumperPressed)
            {
                swapMode();
                Game.SoundEffects.playMenuButtonMovementSnap();
            }
        }

        /*
        /// <summary>
        /// Checks for and returns a selected ingredient and then closes the menu.
        /// </summary>
        /// <returns></returns>
        private System.Collections.IEnumerable selectIngredientCloseMenu()
        {
            if (selectedIngredient != null)
            {
                yield return selectedIngredient;
                Game.Player.specialIngredientsInventory.Remove(selectedIngredient);
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
        */
        /// <summary>
        /// Close the active menu.
        /// </summary>
        public override void exitMenu()
        {
            if (currentViewMode == Enums.InventoryViewMode.DishView)
            {
                //Debug.Log("Close?");
                Game.Player.activeItem = this.selectedDish;
                Game.Player.updateHeldItemSprite();
                Game.HUD.showInventory = true;
                //Game.HUD.InventoryHUD.swapMode(currentViewMode);
                base.exitMenu();
                Game.Menu = null;
            }
            else
            {
                //Debug.Log("Close2?");
                Game.Player.activeItem = this.selectedIngredient;
                Game.Player.updateHeldItemSprite();
                Game.HUD.showInventory = true;
                //Game.HUD.InventoryHUD.swapMode(currentViewMode);
                base.exitMenu();
                Game.Menu = null;
            }
            
        }

    }
}
