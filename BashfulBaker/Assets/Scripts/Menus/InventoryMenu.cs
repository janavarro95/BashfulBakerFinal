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
using System.Collections.Generic;
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

        GameObject inventory;

        Item leftItem;
        Item rightItem;
        Item topItem;
        Item bottomItem;

        Item selectedItem;

        /// <summary>
        /// Instantiate all menu logic here.
        /// </summary>
        public override void Start()
        {

            menuPage = 0;

            //initial set up
            GameObject canvas = this.transform.Find("Canvas").gameObject;

            inventory = canvas.gameObject.transform.Find("Inventory").gameObject;
            setUpMenu();

            //Game.Player.inventory.Add(Ingredient.LoadIngredientFromPrefab("Cherries",1));
            //menuCursor = canvas.transform.Find("MenuMouseCursor").GetComponent<GameCursorMenu>();
            Game.Menu = this;

            Game.HUD.showInventory = false;

        }

        private void setUpMenu()
        {
            getMenuComponents();
            setDish();
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

            leftText = leftIngredient.transform.Find("LeftText").GetComponent<Text>();
            rightText = rightIngredient.transform.Find("RightText").GetComponent<Text>();
            topText = topIngredient.transform.Find("TopText").GetComponent<Text>();
            bottomText = bottomIngredient.transform.Find("BottomText").GetComponent<Text>();
        }


        private void setDish()
        {
            

            topItem = null;
            bottomItem = null;
            leftItem = null;
            rightItem = null;

            leftText.text = "";
            rightText.text = "";
            topText.text = "";
            bottomText.text = "";

            List<Item> items = Game.Player.inventory.getAllItems();

            maxPages = (items.Count / 4)+1;

            
            leftImage.color = new Color(1, 1, 1, 0);
            rightImage.color = new Color(1, 1, 1, 0);
            topImage.color = new Color(1, 1, 1, 0);
            bottomImage.color = new Color(1, 1, 1, 0);
            centralImage.color = new Color(1, 1, 1, 0);
            

            //left ingredient sprite
            if (items.Count > (0 + menuPage * 4))
            {
                leftImage.sprite = Content.ContentManager.Instance.loadSprite(items[0 + (menuPage * 4)].Sprite,new Rect(0,0,32,32),new Vector2(0.5f,0.5f),16);
                Debug.Log("AHHH");
                leftImage.color = Color.white;
                leftItem =items[0 + (menuPage * 4)];
                leftText.text = leftItem.stack.ToString();
            }

            //right ingredient sprite
            if (items.Count > (1 + menuPage * 4))
            {
                rightImage.sprite = Sprite.Create(items[1 + (menuPage * 4)].Sprite, rightImage.rectTransform.rect, rightImage.sprite.pivot);
                rightImage.color = Color.white;
                rightItem = items[1 + (menuPage * 4)];
                rightText.text = rightItem.stack.ToString();
            }

            //Top ingredient sprite
            if (items.Count > (2 + menuPage * 4))
            {
                topImage.sprite = Sprite.Create(items[2 + (menuPage * 4)].Sprite, topImage.rectTransform.rect, topImage.sprite.pivot);
                topImage.color = Color.white;
                topItem = items[2+ (menuPage * 4)];
                topText.text = topItem.stack.ToString();
            }

            //Bottom ingredient sprite
            if (items.Count > (3 + menuPage * 4))
            {
                
                bottomImage.sprite = Sprite.Create(items[3 + (menuPage * 4)].Sprite, bottomImage.rectTransform.rect, bottomImage.sprite.pivot);
                bottomImage.color = Color.white;
                bottomItem = items[3 + (menuPage * 4)];
                bottomText.text = bottomItem.stack.ToString();
            }

            if (Game.Player.activeItem != null)
            {
                this.selectedItem = Game.Player.activeItem;
                centralImage.sprite = Content.ContentManager.Instance.loadSprite(selectedItem.Sprite, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f), 16);
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

            if (InputControls.APressed)
            {
                if (bottomItem == null)
                {
                    centralImage.color = new Color(1, 1, 1, 0);
                    selectedItem = null;
                    return;
                }
                //bottom state
                selectedItem = bottomItem;
                centralImage.sprite = Content.ContentManager.Instance.loadSprite(selectedItem.Sprite, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f), 16);
                centralImage.color = new Color(1, 1, 1, 1);
            }
            if (InputControls.BPressed)
            {
                if (rightItem == null)
                {
                    centralImage.color = new Color(1, 1, 1, 0);
                    selectedItem = null;
                    return;
                }
                //right state
                selectedItem = rightItem;
                centralImage.sprite = Content.ContentManager.Instance.loadSprite(selectedItem.Sprite, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f), 16);
                centralImage.color = new Color(1, 1, 1, 1);
            }
            if (InputControls.XPressed)
            {
                if (leftItem == null)
                {
                    centralImage.color = new Color(1, 1, 1, 0);
                    selectedItem = null;
                    return;
                }
                //left
                selectedItem = leftItem;
                centralImage.sprite = Content.ContentManager.Instance.loadSprite(selectedItem.Sprite, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f), 16);
                centralImage.color = new Color(1, 1, 1, 1);
            }
            if (InputControls.YPressed)
            {
                if (topItem == null)
                {
                    centralImage.color = new Color(1, 1, 1, 0);
                    selectedItem = null;
                    return;
                }
                //top
                selectedItem = topItem;
                centralImage.sprite = Content.ContentManager.Instance.loadSprite(selectedItem.Sprite, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f), 16);
                centralImage.color = new Color(1, 1, 1, 1);
            }

            if (InputControls.StartPressed)
            {
                exitMenu();
                Game.HUD.showInventory = true;
            }
        }

        /// <summary>
        /// Checks for and returns a selected ingredient and then closes the menu.
        /// </summary>
        /// <returns></returns>
        private System.Collections.IEnumerable selectIngredientCloseMenu()
        {
            if (selectedItem != null)
            {
                yield return selectedItem;
                selectedItem.removeFromStack(1);
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
            Game.Player.activeItem = this.selectedItem;
            Game.HUD.showInventory = true;
            Destroy(this.gameObject);
            Game.Menu = null;
        }

    }
}
