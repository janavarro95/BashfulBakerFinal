using Assets.Scripts.GameInformation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Items
{
    public class ComplexIngredient:Ingredient
    {
        public List<Ingredient> ingredients;


        public ComplexIngredient()
        {
            ingredients = new List<Ingredient>();
        }

        public ComplexIngredient(string ItemName) : base(ItemName)
        {
            ingredients = new List<Ingredient>();
        }

        public ComplexIngredient(string ItemName, List<Ingredient> Ingredients):base(ItemName)
        {
            this.ingredients = Ingredients;
        }

        public override Item clone()
        {
            return new ComplexIngredient(this.Name, this.ingredients);
        }

        protected override void loadSpriteFromDisk()
        {
            string combinedFolders = Path.Combine("Graphics", "Items");
            combinedFolders = Path.Combine(combinedFolders, "ComplexIngredients");

            

            this._sprite = Game.ContentManager.loadTexture2DFromStreamingAssets(Path.Combine(combinedFolders, this.itemName+".png"));
        }
    }
}
