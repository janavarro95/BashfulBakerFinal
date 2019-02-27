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

        public Dish(string DishName): base(DishName)
        {
            ingredients = new List<Item>();
            this.currentDishState = Enums.DishState.Ingredients;
        }

        public Dish(string DishName, List<Item> Ingredients):base(DishName)
        {
            this.ingredients = Ingredients;
            this.currentDishState = Enums.DishState.Ingredients;
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
            string combinedFolders = Path.Combine("Graphics", "Items");
            combinedFolders = Path.Combine(combinedFolders, "Dishes");


            this.ingredientsSprite = Game.ContentManager.loadTexture2D(Path.Combine(combinedFolders, this.itemName + "_ingredients.png"));
            this.mixedSprite = Game.ContentManager.loadTexture2D(Path.Combine(combinedFolders, this.itemName + "_mixed.png"));
            this.preppedSprite = Game.ContentManager.loadTexture2D(Path.Combine(combinedFolders, this.itemName + "_prepped.png"));
            this.bakedSprite = Game.ContentManager.loadTexture2D(Path.Combine(combinedFolders, this.itemName + "_baked.png"));
            this.packagedSprite = Game.ContentManager.loadTexture2D(Path.Combine(combinedFolders, this.itemName + "_packaged.png"));

            this._sprite = this.ingredientsSprite;
        }


    }
}
