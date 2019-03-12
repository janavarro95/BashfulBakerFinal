using Assets.Scripts.GameInformation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class Dish:Item
    {
        public List<Item> ingredients;

        public Enums.DishState currentDishState;


        public Texture2D ingredientsSprite;
        public Texture2D mixedSprite;
        public Texture2D preppedSprite;
        public Texture2D bakedSprite;
        public Texture2D packagedSprite;

        public Texture2D currentSprite
        {
            get
            {
                return this.currentSprite;
            }
            set
            {
                this.Sprite = value;
            }
        }

        public bool IsIngredients
        {
            get
            {
                return currentDishState == Enums.DishState.Ingredients;
            }
        }
        public bool HasBeenMixingBowled
        {
            get
            {
                return currentDishState == Enums.DishState.Mixed;
            }
        }
        public bool HasBeenPreppedToBake
        {
            get
            {
                return currentDishState == Enums.DishState.Prepped;
            }
        }
        public bool HasBeenBaked
        {
            get
            {
                return currentDishState == Enums.DishState.Baked;
            }
        }

        public bool HasBeenPackaged
        {
            get
            {
                return currentDishState == Enums.DishState.Packaged;
            }
        }

        public bool IsDishComplete
        {
            get
            {
                return HasBeenPackaged;
            }
        }


        public Dish() : base()
        {
            ingredients = new List<Item>();
            this.currentDishState = Enums.DishState.Ingredients;
        }

        public Dish(string DishName,Enums.DishState State= Enums.DishState.Ingredients): base(DishName)
        {
            ingredients = new List<Item>();
            this.currentDishState = State;
        }

        public Dish(string DishName, List<Item> Ingredients, Enums.DishState State = Enums.DishState.Ingredients) :base(DishName)
        {
            this.ingredients = Ingredients; 
            this.currentDishState = State;
        }

        public void Update()
        {
            checkForTextureChange();
        }

        private void checkForTextureChange()
        {
            if(this.currentDishState== Enums.DishState.Ingredients)
            {
                this.Sprite = this.ingredientsSprite;
            }
            else if(this.currentDishState== Enums.DishState.Mixed)
            {
                this.Sprite = mixedSprite;
            }
            else if(this.currentDishState== Enums.DishState.Prepped)
            {
                this.Sprite = preppedSprite;
            }
            else if(this.currentDishState== Enums.DishState.Baked)
            {
                this.Sprite = bakedSprite;
            }
            else if(this.currentDishState == Enums.DishState.Packaged)
            {
                this.Sprite = packagedSprite;
            }
        }

        public override Item clone()
        {
            return new Dish(this.Name, this.ingredients);
        }

        /// <summary>
        /// Loads the appropriate dish textures from disk.
        /// </summary>
        protected override void loadSpriteFromDisk()
        {
            string combinedFolders = Path.Combine("Graphics", "Objects");

            string sheetTrays = Path.Combine(combinedFolders, "Sheet Trays");
            string doughBowls = Path.Combine(combinedFolders, "Dough Bowls");
            string pastryBox = Path.Combine(combinedFolders, "Pastry Box");

            this.ingredientsSprite = Game.ContentManager.loadTexture2D(Path.Combine(sheetTrays, "BaseTrayF.png"));
            this.mixedSprite = Game.ContentManager.loadTexture2D(Path.Combine(doughBowls, "CCDoughBowl.png"));

            this.preppedSprite = getAppropriatePreppedSprite();
            this.bakedSprite = getAppropriateBakedSprite();
            this.packagedSprite = Game.ContentManager.loadTexture2D(Path.Combine(pastryBox, "PastryBox.png"));

            this._sprite = this.ingredientsSprite;
        }

        private Texture2D getAppropriatePreppedSprite()
        {
            string combinedFolders = Path.Combine("Graphics", "Objects");

            string sheetTrays = Path.Combine(combinedFolders, "Sheet Trays");
            string doughBowls = Path.Combine(combinedFolders, "Dough Bowls");


            if (this.itemName == "Chocolate Chip Cookies" || this.itemName== "Chocolate Chip Cookie")
            {
                sheetTrays = Path.Combine(sheetTrays, "Choc Chip");
                return Game.ContentManager.loadTexture2D(Path.Combine(sheetTrays, "carry_CCTray_raw.png"));
            }
            return null;
        }

        private Texture2D getAppropriateBakedSprite()
        {
            string combinedFolders = Path.Combine("Graphics", "Objects");

            string sheetTrays = Path.Combine(combinedFolders, "Sheet Trays");
            string doughBowls = Path.Combine(combinedFolders, "Dough Bowls");


            if (this.itemName == "Chocolate Chip Cookies" || this.itemName == "Chocolate Chip Cookie")
            {
                sheetTrays = Path.Combine(sheetTrays, "Choc Chip");
                return Game.ContentManager.loadTexture2D(Path.Combine(sheetTrays, "carry_CCTray_cook.png"));
            }
            return null;
        }


    }
}
