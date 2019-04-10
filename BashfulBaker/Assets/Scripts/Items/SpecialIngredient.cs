using Assets.Scripts.GameInformation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Items
{
    public class SpecialIngredient:Item
    {

        public Enums.SpecialIngredients ingredientType;

        public SpecialIngredient() : base()
        {
        }

        public SpecialIngredient(string Name) : base(Name)
        {
        }

        public SpecialIngredient(string Name, int StackSize) : base(Name, StackSize)
        {
        }

        public SpecialIngredient(Enums.SpecialIngredients ingredient, int StackSize=0)
        {
            if(ingredient== Enums.SpecialIngredients.Carrots)
            {
                this.Name = "Carrots";
            }
            else if(ingredient== Enums.SpecialIngredients.ChocolateChips)
            {
                this.Name = "Chocolate Chip";
            }
            else if(ingredient== Enums.SpecialIngredients.MintChips)
            {
                this.Name = "Mint Chip";
            }
            else if(ingredient== Enums.SpecialIngredients.Pecans)
            {
                this.Name = "Pecans";
            }
            else if(ingredient== Enums.SpecialIngredients.Rasins)
            {
                this.Name = "Rasins";
            }
            else if(ingredient== Enums.SpecialIngredients.Strawberries)
            {
                this.Name = "Strawberries";
            }

            this.ingredientType = ingredient;
            this.stack = StackSize;
        }

        public override Item clone()
        {
            return base.clone();
        }

        protected override void loadSpriteFromDisk()
        {
            string combinedFolders = Path.Combine("Graphics", "Items");
            combinedFolders = Path.Combine(combinedFolders, "SpecialIngredients");

            this._sprite = Game.ContentManager.loadTexture2DFromStreamingAssets(Path.Combine(combinedFolders, this.itemName+ ".png"));
        }
    }
}
