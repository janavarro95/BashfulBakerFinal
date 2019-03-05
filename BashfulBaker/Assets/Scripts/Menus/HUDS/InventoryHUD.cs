using Assets.Scripts.GameInformation;
using Assets.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus.HUDS
{
    public class InventoryHUD:HUD
    {

        GameObject canvas;

        public Enums.InventoryViewMode currentMode;
        public GameObject dishes;
        public GameObject specialIngredients;

        public Dish topDish;
        public Dish leftDish;
        public Dish rightDish;
        public Dish bottomDish;

        public SpecialIngredient topSpecialIngredient;
        public SpecialIngredient leftSpecialIngredient;
        public SpecialIngredient rightSpecialIngredient;
        public SpecialIngredient bottomSpecialIngredient;

        public Image topImage;
        public Image leftImage;
        public Image rightImage;
        public Image bottomImage;
        public Image centralImage;

        public override void Start()
        {
            canvas = this.gameObject.transform.Find("Canvas").gameObject;
            this.currentMode = Enums.InventoryViewMode.DishView;
            GameObject inventoryBackground = canvas.transform.Find("Inventory Background").gameObject;

            dishes = inventoryBackground.transform.Find("DishesView").gameObject;
            specialIngredients = inventoryBackground.transform.Find("SpecialIngredientsView").gameObject;
            setUpComponents();

        }

        public void setUpComponents()
        {
            GameObject selectedView = null;
            if(this.currentMode== Enums.InventoryViewMode.DishView)
            {
                selectedView = this.dishes;
            }
            else
            {
                selectedView = this.specialIngredients;
            }

            leftImage = selectedView.transform.Find("LeftImage").gameObject.GetComponent<Image>();
            rightImage = selectedView.transform.Find("RightImage").gameObject.GetComponent<Image>();
            topImage = selectedView.transform.Find("TopImage").gameObject.GetComponent<Image>();
            bottomImage = selectedView.transform.Find("BottomImage").gameObject.GetComponent<Image>();
            centralImage = selectedView.transform.Find("CentralImage").gameObject.GetComponent<Image>();

            List<Dish> dishes = Game.Player.dishesInventory.getAllDishes();
            List<SpecialIngredient> specialIngredients = Game.Player.specialIngredientsInventory.getAllSpecialIngredients();


            leftImage.color = new Color(1, 1, 1, 0);
            rightImage.color = new Color(1, 1, 1, 0);
            topImage.color = new Color(1, 1, 1, 0);
            bottomImage.color = new Color(1, 1, 1, 0);
            centralImage.color = new Color(1, 1, 1, 0);

            if (currentMode == Enums.InventoryViewMode.DishView)
            {
                this.dishes.SetActive(true);
                this.specialIngredients.SetActive(false);
                //left ingredient sprite
                if (dishes.Count > 0)
                {
                    Texture2D texture = dishes[0].Sprite;
                    leftImage.sprite = Content.ContentManager.Instance.loadSprite(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16);
                    leftImage.color = Color.white;
                    leftDish = dishes[0];
                }

                //right ingredient sprite
                if (dishes.Count > 1)
                {
                    Texture2D texture = dishes[1].Sprite;
                    rightImage.sprite = Content.ContentManager.Instance.loadSprite(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16);
                    rightImage.color = Color.white;
                    rightDish = dishes[1];
                }

                //Top ingredient sprite
                if (dishes.Count > 2)
                {
                    Texture2D texture = dishes[2].Sprite;
                    topImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16);
                    topImage.color = Color.white;
                    topDish = dishes[2];
                }

                //Bottom ingredient sprite
                if (dishes.Count > (3))
                {
                    Texture2D texture = dishes[3].Sprite;
                    bottomImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16);
                    bottomImage.color = Color.white;
                    bottomDish = dishes[3];
                }
            }
            else
            {
                this.dishes.SetActive(false);
                this.specialIngredients.SetActive(true);
                //left ingredient sprite
                if (specialIngredients.Count > 0)
                {
                    Texture2D texture = specialIngredients[0].Sprite;
                    leftImage.sprite = Content.ContentManager.Instance.loadSprite(specialIngredients[0].Sprite, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16);
                    leftImage.color = Color.white;
                    leftSpecialIngredient = specialIngredients[0];
                }

                //right ingredient sprite
                if (specialIngredients.Count > 1)
                {
                    Texture2D texture = specialIngredients[1].Sprite;
                    rightImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16);
                    rightImage.color = Color.white;
                    rightSpecialIngredient = specialIngredients[1];
                }

                //Top ingredient sprite
                if (specialIngredients.Count > 2)
                {
                    Texture2D texture = specialIngredients[2].Sprite;
                    topImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16);
                    topImage.color = Color.white;
                    topSpecialIngredient = specialIngredients[2];
                }

                //Bottom ingredient sprite
                if (specialIngredients.Count > (3))
                {
                    Texture2D texture = specialIngredients[3].Sprite;
                    bottomImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16);
                    bottomImage.color = Color.white;
                    bottomSpecialIngredient = specialIngredients[3];
                }
            }

        }

        public void swapMode()
        {
            if(this.currentMode== Enums.InventoryViewMode.DishView)
            {
                this.currentMode = Enums.InventoryViewMode.SpecialIngredientView;
                setUpComponents();
                return;
            }
            else if(this.currentMode== Enums.InventoryViewMode.SpecialIngredientView)
            {
                this.currentMode = Enums.InventoryViewMode.DishView;
                setUpComponents();
                return;
            }
        }

        public override void Update()
        {
            if (Game.HUD.showInventory == true)
            {
                if (GameInput.InputControls.RightBumperPressed)
                {
                    swapMode();
                }
            }
        }

        public override void setVisibility(Enums.Visibility visibility)
        {
            if (visibility == Enums.Visibility.Invisible) canvas.SetActive(false);
            if (visibility == Enums.Visibility.Visible) canvas.SetActive(true);
        }

    }
}
