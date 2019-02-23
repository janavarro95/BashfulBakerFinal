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

        public Dish() : base()
        {
            ingredients = new List<Item>();
        }

        public Dish(string DishName): base(DishName)
        {
            ingredients = new List<Item>();
        }

        public Dish(string DishName, List<Item> Ingredients):base(DishName)
        {
            this.ingredients = Ingredients;
        }

        public override Item clone()
        {
            return new Dish(this.Name, this.ingredients);
        }

        protected override void loadSpriteFromDisk()
        {
            string combinedFolders = Path.Combine("Graphics", "Items");
            combinedFolders = Path.Combine(combinedFolders, "Dishes");
            this._sprite = Game.ContentManager.loadTexture2D(Path.Combine(combinedFolders, this.itemName+ ".png"));
        }


    }
}
