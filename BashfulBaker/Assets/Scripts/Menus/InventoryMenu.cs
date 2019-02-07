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
    /// TODO: Add button functionality for selecting ingredients.
    ///       Add in left bumper button.
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

        Ingredient leftIngredient;
        Ingredient rightIngredient;
        Ingredient topIngredient;
        Ingredient bottomIngredient;

        Ingredient selectedIngredient;
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
            GameObject centralIngredient = menuStuff.gameObject.transform.Find("CentralIngredient").gameObject;

            leftImage = leftIngredient.GetComponent<Image>();
            rightImage = rightIngredient.GetComponent<Image>();
            topImage = topIngredient.GetComponent<Image>();
            bottomImage = bottomIngredient.GetComponent<Image>();
            centralImage = centralIngredient.GetComponent<Image>();


            Game.Player.inventory.Add(Ingredient.LoadIngredientFromPrefab("Cherries"));
            setIngredients();
            //menuCursor = canvas.transform.Find("MenuMouseCursor").GetComponent<GameCursorMenu>();
            Game.Menu = this;
        }


        private void setIngredients()
        {
            centralImage.color = new Color(1, 1, 1, 0);

            topIngredient = null;
            bottomIngredient = null;
            leftIngredient = null;
            rightIngredient = null;
            

            List<Item> items = Game.Player.inventory.items.FindAll(i => i.GetType() == typeof(Assets.Scripts.Items.Ingredient) || i.GetType() == typeof(Assets.Scripts.Items.ComplexIngredient));

            maxPages = (items.Count / 4)+1;

            leftImage.color = new Color(1, 1, 1, 0);
            rightImage.color = new Color(1, 1, 1, 0);
            topImage.color = new Color(1, 1, 1, 0);
            bottomImage.color = new Color(1, 1, 1, 0);
            

            //left ingredient sprite
            if (items.Count > (0 + menuPage * 4))
            {
                leftImage.sprite = items[0 + (menuPage * 4)].sprite;
                leftImage.color = Color.white;
                leftIngredient =(Ingredient)items[0 + (menuPage * 4)];
            }

            //right ingredient sprite
            if (items.Count > (1 + menuPage * 4))
            {
                leftImage.sprite = items[1 + (menuPage * 4)].sprite;
                leftImage.color = Color.white;
                rightIngredient = (Ingredient)items[1 + (menuPage * 4)];
            }

            //Top ingredient sprite
            if (items.Count > (2 + menuPage * 4))
            {
                leftImage.sprite = items[2 + (menuPage * 4)].sprite;
                leftImage.color = Color.white;
                topIngredient = (Ingredient)items[2+ (menuPage * 4)];
            }

            //Bottom ingredient sprite
            if (items.Count > (3 + menuPage * 4))
            {
                leftImage.sprite = items[3 + (menuPage * 4)].sprite;
                leftImage.color = Color.white;
                bottomIngredient = (Ingredient)items[3 + (menuPage * 4)];
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

            if (GameInput.InputControls.APressed)
            {
                if (bottomIngredient != null)
                {
                    selectedIngredient = bottomIngredient;
                }
            }
            if (GameInput.InputControls.BPressed)
            {
                if (rightIngredient != null)
                {
                    selectedIngredient = rightIngredient;
                }
            }
            if (GameInput.InputControls.XPressed)
            {
                if (leftIngredient != null)
                {
                    selectedIngredient = leftIngredient;
                }
            }
            if (GameInput.InputControls.YPressed)
            {
                if (topIngredient != null)
                {
                    selectedIngredient = topIngredient;
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
