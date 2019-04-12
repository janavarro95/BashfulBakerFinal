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
    [Serializable]
    public class Dish:Item
    {
        public List<Item> ingredients;

        public Enums.DishState currentDishState;


        public Texture2D ingredientsSprite;
        public Texture2D mixedSprite;
        public Texture2D preppedSprite;
        public Texture2D bakedSprite;
        public Texture2D packagedSprite;
        public Texture2D burntSprite;

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

        public bool HasBeenBurned
        {
            get
            {
                return currentDishState == Enums.DishState.Burnt;
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

            this.ingredientsSprite = Game.ContentManager.loadTexture2DFromResources(Path.Combine(sheetTrays, "BaseTrayF"));

            Debug.Log(this.ingredientsSprite != null ? "yay ingredients" : "boo nothing");

            //this.mixedSprite = Game.ContentManager.loadTexture2DFromResources(Path.Combine(doughBowls, "CCDoughBowl"));
            this.mixedSprite = getAppropriateMixedSprite();


            this.preppedSprite = getAppropriatePreppedSprite();
            this.bakedSprite = getAppropriateBakedSprite();
            this.packagedSprite = Game.ContentManager.loadTexture2DFromResources(Path.Combine(pastryBox, "PastryBox"));

            this._sprite = this.ingredientsSprite;
        }

        private Texture2D getAppropriateMixedSprite()
        {
            string combinedFolders = Path.Combine("Graphics", "Objects");

            string sheetTrays = Path.Combine(combinedFolders, "Sheet Trays");
            string doughBowls = Path.Combine(combinedFolders, "Dough Bowls");


            if (this.itemName == "Chocolate Chip Cookies" || this.itemName == "Chocolate Chip Cookie")
            {
                Debug.Log("GET THAT CHOCOLATE CHIP BOWL!");
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
                return Game.ContentManager.loadTexture2DFromResources(Path.Combine(sheetTrays, "carry_CCTray_raw"));
            }

            else if (this.itemName == "Mint Chip Cookies" || this.itemName == "Mint Chip Cookie")
            {
                sheetTrays = Path.Combine(sheetTrays, "Mint Chip");
                return Game.ContentManager.loadTexture2DFromResources(Path.Combine(sheetTrays, "Carry_Tray_RawMC"));
            }
            else if (this.itemName == "Oatmeal Raisin Cookies" || this.itemName=="Oatmeal Raisin Cookie")
            {
                sheetTrays = Path.Combine(sheetTrays, "Oatmeal Raisin");
                return Game.ContentManager.loadTexture2DFromResources(Path.Combine(sheetTrays, "Carry_Tray_RawOR"));
            }

            else if (this.itemName == "Pecan Crescent Cookies")
            {
                sheetTrays = Path.Combine(sheetTrays, "Pecan Crescent");
                return Game.ContentManager.loadTexture2DFromResources(Path.Combine(sheetTrays, "Carry_Tray_RawPC"));
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
                return Game.ContentManager.loadTexture2DFromResources(Path.Combine(sheetTrays, "carry_CCTray_cook"));
            }

            else if (this.itemName == "Mint Chip Cookies" || this.itemName == "Mint Chip Cookie")
            {
                sheetTrays = Path.Combine(sheetTrays, "Mint Chip");
                return Game.ContentManager.loadTexture2DFromResources(Path.Combine(sheetTrays, "Carry_Tray_BakedMC"));
            }

            else if (this.itemName == "Oatmeal Raisin Cookies" || this.itemName=="Oatmeal Raisin Cookie")
            {
                sheetTrays = Path.Combine(sheetTrays, "Oatmeal Raisin");
                return Game.ContentManager.loadTexture2DFromResources(Path.Combine(sheetTrays, "Carry_Tray_BakedOR"));
            }

            else if (this.itemName == "Pecan Crescent Cookies")
            {
                sheetTrays = Path.Combine(sheetTrays, "Pecan Crescent");
                return Game.ContentManager.loadTexture2DFromResources(Path.Combine(sheetTrays, "Carry_Tray_BakedPC"));
            }

            return null;
        }


    }
}
