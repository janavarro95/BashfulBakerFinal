using Assets.Scripts.GameInformation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class Ingredient:Item
    {
        public Ingredient()
        {

        }

        public Ingredient(string ItemName) : base(ItemName)
        {

        }

        public override Item clone()
        {
            return base.clone();
        }

        protected override void loadSpriteFromDisk()
        {
            string combinedFolders = Path.Combine("Graphics", "Items");
            combinedFolders = Path.Combine(combinedFolders, "Ingredients");

            Debug.Log(Path.Combine(combinedFolders, this.itemName + ".png"));

            this._sprite = Game.ContentManager.loadTexture2D(Path.Combine(combinedFolders, this.itemName+".png"));
            
        }
    }
}
