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
    /// <summary>
    /// Deals with handling a cooked dish for the game and tracking all of it's states.
    /// </summary>
    [Serializable]
    public class Dish:Item
    {
        /// <summary>
        /// The ingredients used in this dish. Not really used anymore.
        /// </summary>
        public List<Item> ingredients;

        /// <summary>
        /// The state for the dish.
        /// </summary>
        public Enums.DishState currentDishState;


        /// <summary>
        /// The texture for the ingredients visual for the dish.
        /// </summary>
        public Texture2D ingredientsSprite;
        /// <summary>
        /// The texture for the mixed visual for the dish.
        /// </summary>
        public Texture2D mixedSprite;
        /// <summary>
        /// The texture for the prepped visual for the dish.
        /// </summary>
        public Texture2D preppedSprite;

        /// <summary>
        /// The texture for the baked visual for the dish.
        /// </summary>
        public Texture2D bakedSprite;
        /// <summary>
        /// The texture for the packaged visual for the dish.
        /// </summary>
        public Texture2D packagedSprite;
        /// <summary>
        /// The texture for the burnt visual for the dish.
        /// </summary>
        public Texture2D burntSprite;

        /// <summary>
        /// Gets the sprite for the dish.
        /// </summary>
        public Texture2D currentSprite
        {
            get
            {
                return this.Sprite;
            }
            set
            {
                this.Sprite = value;
            }
        }

        /// <summary>
        /// Checks if the dish is just ingredients.
        /// </summary>
        public bool IsIngredients
        {
            get
            {
                return currentDishState == Enums.DishState.Ingredients;
            }
        }
        /// <summary>
        /// Checks if the dish has been mixed.
        /// </summary>
        public bool HasBeenMixingBowled
        {
            get
            {
                return currentDishState == Enums.DishState.Mixed;
            }
        }
        /// <summary>
        /// Checks if the dish has been prepped and needs to be baked.
        /// </summary>
        public bool HasBeenPreppedToBake
        {
            get
            {
                return currentDishState == Enums.DishState.Prepped;
            }
        }
        /// <summary>
        /// Checks if the dish has been baked.
        /// </summary>
        public bool HasBeenBaked
        {
            get
            {
                return currentDishState == Enums.DishState.Baked;
            }
        }

        /// <summary>
        /// Checks if the dish has been packaged.
        /// </summary>
        public bool HasBeenPackaged
        {
            get
            {
                return currentDishState == Enums.DishState.Packaged;
            }
        }

        /// <summary>
        /// Checks if the dish has been burnt.
        /// </summary>
        public bool HasBeenBurned
        {
            get
            {
                return currentDishState == Enums.DishState.Burnt;
            }
        }

        /// <summary>
        /// Checks if the dish has been completed.
        /// </summary>
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

        /// <summary>
        /// Creates a new dish.
        /// </summary>
        /// <param name="DishName"></param>
        /// <param name="State"></param>
        public Dish(string DishName,Enums.DishState State= Enums.DishState.Ingredients): base(DishName)
        {
            ingredients = new List<Item>();
            this.currentDishState = State;
        }

        /// <summary>
        /// Constructor that refers to a list of enums of the different kinds of dishes we can make.
        /// </summary>
        /// <param name="Dishes"></param>
        public Dish(Enums.Dishes Dishes)
        {
            if(Dishes== Enums.Dishes.ChocolateChipCookies)
            {
                this.Name = "Chocolate Chip Cookies";
            }
            if(Dishes== Enums.Dishes.MintChipCookies)
            {
                this.Name = "Mint Chip Cookies";
            }
            if(Dishes== Enums.Dishes.OatmealRaisinCookies)
            {
                this.Name = "Oatmeal Raisin Cookies";
            }
            if (Dishes == Enums.Dishes.PecanCookies)
            {
                this.Name = "Pecan Crescent Cookies";
            }

            this.currentDishState = Enums.DishState.Ingredients;
        }

        public Dish(string DishName, List<Item> Ingredients, Enums.DishState State = Enums.DishState.Ingredients) :base(DishName)
        {
            this.ingredients = Ingredients; 
            this.currentDishState = State;
        }

        /// <summary>
        /// Updates the dish.
        /// </summary>
        public void Update()
        {
            checkForTextureChange();
        }

        /// <summary>
        /// Checks to see if the dish's state has changed and loads the appropriate texture for it.
        /// </summary>
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

        /// <summary>
        /// Gets a clone of this dish. Unused.
        /// </summary>
        /// <returns></returns>
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

            this.ingredientsSprite = Game.ContentManager.loadTexture2DFromResources(Path.Combine(sheetTrays, "BaseTrayF"));

            //Debug.Log(this.ingredientsSprite != null ? "yay ingredients" : "boo nothing");

            //this.mixedSprite = Game.ContentManager.loadTexture2DFromResources(Path.Combine(doughBowls, "CCDoughBowl"));
            this.mixedSprite = getAppropriateMixedSprite();


            this.preppedSprite = getAppropriatePreppedSprite();
            this.bakedSprite = getAppropriateBakedSprite();
            this.packagedSprite = Game.ContentManager.loadTexture2DFromResources(Path.Combine(pastryBox, "PastryBox"));

            this._sprite = this.ingredientsSprite;
        }

        /// <summary>
        /// Loads the appropriate mixed sprite for the dish.
        /// </summary>
        /// <returns></returns>
        private Texture2D getAppropriateMixedSprite()
        {
            string combinedFolders = Path.Combine("Graphics", "Objects");

            string sheetTrays = Path.Combine(combinedFolders, "Sheet Trays");
            string doughBowls = Path.Combine(combinedFolders, "Dough Bowls");


            if (this.itemName == "Chocolate Chip Cookies" || this.itemName == "Chocolate Chip Cookie")
            {
                doughBowls = Path.Combine(doughBowls, "Choc Chip");
                return Game.ContentManager.loadTexture2DFromResources(Path.Combine(doughBowls, "CCDoughBowl"));
            }
            else if(this.itemName=="Mint Chip Cookies" || this.itemName=="Mint Chip Cookie")
            {
                return Game.ContentManager.loadTexture2DFromResources(Path.Combine(Path.Combine(doughBowls,"Mint Chip"), "Bowl_MC"));
            }
            else if(this.itemName=="Oatmeal Raisin Cookies" || this.itemName=="Oatmeal Raisin Cookie")
            {
                return Game.ContentManager.loadTexture2DFromResources(Path.Combine(Path.Combine(doughBowls, "Oatmeal Raisin"), "Bowl_OR"));
            }
            else if (this.itemName == "Pecan Crescent Cookies")
            {
                return Game.ContentManager.loadTexture2DFromResources(Path.Combine(Path.Combine(doughBowls, "Pecan Crescent"), "Bowl_PC"));
            }
            return null;
        }

        /// <summary>
        /// Used to get the appropriate prepped sprites for the dish.
        /// </summary>
        /// <returns></returns>
        private Texture2D getAppropriatePreppedSprite()
        {
            string combinedFolders = Path.Combine("Graphics", "Objects");

            string sheetTrays = Path.Combine(combinedFolders, "Sheet Trays");
            string doughBowls = Path.Combine(combinedFolders, "Dough Bowls");


            if (this.itemName == "Chocolate Chip Cookies" || this.itemName== "Chocolate Chip Cookie")
            {
                sheetTrays = Path.Combine(sheetTrays, "Choc Chip");
                return Game.ContentManager.loadTexture2DFromResources(Path.Combine(sheetTrays, "carry_CCTray_raw_sprite"));
            }

            else if (this.itemName == "Mint Chip Cookies" || this.itemName == "Mint Chip Cookie")
            {
                sheetTrays = Path.Combine(sheetTrays, "Mint Chip");
                return Game.ContentManager.loadTexture2DFromResources(Path.Combine(sheetTrays, "Carry_Tray_RawMC_sprite"));
            }
            else if (this.itemName == "Oatmeal Raisin Cookies" || this.itemName=="Oatmeal Raisin Cookie")
            {
                sheetTrays = Path.Combine(sheetTrays, "Oatmeal Raisin");
                return Game.ContentManager.loadTexture2DFromResources(Path.Combine(sheetTrays, "Carry_Tray_RawOR_sprite"));
            }

            else if (this.itemName == "Pecan Crescent Cookies")
            {
                sheetTrays = Path.Combine(sheetTrays, "Pecan Crescent");
                return Game.ContentManager.loadTexture2DFromResources(Path.Combine(sheetTrays, "Carry_Tray_RawPC_sprite"));
            }
            return null;
        }

        /// <summary>
        /// Used to get the appropriate baked sprite for the dish
        /// </summary>
        /// <returns></returns>
        private Texture2D getAppropriateBakedSprite()
        {
            string combinedFolders = Path.Combine("Graphics", "Objects");

            string sheetTrays = Path.Combine(combinedFolders, "Sheet Trays");
            string doughBowls = Path.Combine(combinedFolders, "Dough Bowls");


            if (this.itemName == "Chocolate Chip Cookies" || this.itemName == "Chocolate Chip Cookie")
            {
                sheetTrays = Path.Combine(sheetTrays, "Choc Chip");
                return Game.ContentManager.loadTexture2DFromResources(Path.Combine(sheetTrays, "carry_CCTray_cook_sprite"));
            }

            else if (this.itemName == "Mint Chip Cookies" || this.itemName == "Mint Chip Cookie")
            {
                sheetTrays = Path.Combine(sheetTrays, "Mint Chip");
                return Game.ContentManager.loadTexture2DFromResources(Path.Combine(sheetTrays, "Carry_Tray_BakedMC_sprite"));
            }

            else if (this.itemName == "Oatmeal Raisin Cookies" || this.itemName=="Oatmeal Raisin Cookie")
            {
                sheetTrays = Path.Combine(sheetTrays, "Oatmeal Raisin");
                return Game.ContentManager.loadTexture2DFromResources(Path.Combine(sheetTrays, "Carry_Tray_BakedOR_sprite"));
            }

            else if (this.itemName == "Pecan Crescent Cookies")
            {
                sheetTrays = Path.Combine(sheetTrays, "Pecan Crescent");
                return Game.ContentManager.loadTexture2DFromResources(Path.Combine(sheetTrays, "Carry_Tray_BakedPC_sprite"));
            }

            return null;
        }

        public void updateSprite()
        {
            if (this.currentDishState == Enums.DishState.Ingredients) this.Sprite = this.ingredientsSprite;
            if (this.currentDishState == Enums.DishState.Mixed) this.Sprite = this.mixedSprite;
            if (this.currentDishState == Enums.DishState.Prepped) this.Sprite = this.preppedSprite;
            if (this.currentDishState == Enums.DishState.Baked) this.Sprite = this.bakedSprite;
            if (this.currentDishState == Enums.DishState.Packaged) this.Sprite = this.packagedSprite;
        }

    }
}
