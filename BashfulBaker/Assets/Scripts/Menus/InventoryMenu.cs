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

namespace Assets.Scripts.Menus
{
    /// <summary>
    /// TODO: Add button functionality for selecting ingredients.
    ///       Add in left bumper button.
    /// </summary>
    public class InventoryMenu :Menu
    {
        public int menuPage;
        private int maxPages;

        Image leftImage;
        Image rightImage;
        Image topImage;
        Image bottomImage;

        Ingredient leftIngredient;
        Ingredient rightIngredient;
        Ingredient topIngredient;
        Ingredient bottomIngredient;

        /// <summary>
        /// Instantiate all menu logic here.
        /// </summary>
        public override void Start()
        {

            menuPage = 0;

            GameObject canvas = this.transform.Find("Canvas").gameObject;
            GameObject menuStuff = canvas.gameObject.transform.Find("IngredientCategoryBackground").gameObject; 

            GameObject leftIngredient = menuStuff.gameObject.transform.Find("LeftIngredient").gameObject;
            GameObject rightIngredient = menuStuff.gameObject.transform.Find("RightIngredient").gameObject;
            GameObject topIngredient = menuStuff.gameObject.transform.Find("TopIngredient").gameObject;
            GameObject bottomIngredient = menuStuff.gameObject.transform.Find("BottomIngredient").gameObject;

            leftImage = leftIngredient.GetComponent<Image>();
            rightImage = rightIngredient.GetComponent<Image>();
            topImage = topIngredient.GetComponent<Image>();
            bottomImage = bottomIngredient.GetComponent<Image>();

            setIngredients();
            //menuCursor = canvas.transform.Find("MenuMouseCursor").GetComponent<GameCursorMenu>();
            Game.Menu = this;
        }


        private void setIngredients()
        {
            topIngredient = null;
            bottomIngredient = null;
            leftIngredient = null;
            rightIngredient = null;


            List<Item> items = Game.Player.inventory.items.FindAll(i => i.GetType() == typeof(Assets.Scripts.Items.Ingredient) || i.GetType() == typeof(Assets.Scripts.Items.ComplexIngredient));

            maxPages = (items.Count / 4)+1;
            //left ingredient sprite
            if (items.Count > (0 + menuPage * 4))
            {
                leftImage.sprite = items[0 + (menuPage * 4)].sprite;
                leftIngredient =(Ingredient)items[0 + (menuPage * 4)];
            }

            //right ingredient sprite
            if (items.Count > (1 + menuPage * 4))
            {
                leftImage.sprite = items[1 + (menuPage * 4)].sprite;
                rightIngredient = (Ingredient)items[1 + (menuPage * 4)];
            }

            //Top ingredient sprite
            if (items.Count > (2 + menuPage * 4))
            {
                leftImage.sprite = items[2 + (menuPage * 4)].sprite;
                topIngredient = (Ingredient)items[2+ (menuPage * 4)];
            }

            //Bottom ingredient sprite
            if (items.Count > (3 + menuPage * 4))
            {
                leftImage.sprite = items[3 + (menuPage * 4)].sprite;
                bottomIngredient = (Ingredient)items[3 + (menuPage * 4)];
            }
        }

        /// <summary>
        /// Runs ~60 times a second.
        /// </summary>
        public override void Update()
        {
            if (GameInput.InputControls.RightBumperPressed)
            {
                if (maxPages - menuPage > 1)
                {

                    menuPage++;
                    setIngredients();
                }
            }
            if (GameInput.InputControls.LeftBumperPressed)
            {
                if (menuPage == 0) return;
                menuPage--;
                setIngredients();
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
