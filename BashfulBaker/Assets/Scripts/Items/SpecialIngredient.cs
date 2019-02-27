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

        public SpecialIngredient() : base()
        {

        }

        public SpecialIngredient(string Name) : base(Name)
        {

        }

        public SpecialIngredient(string Name, int StackSize) : base(Name, StackSize)
        {

        }

        public override Item clone()
        {
            return base.clone();
        }

        protected override void loadSpriteFromDisk()
        {
            string combinedFolders = Path.Combine("Graphics", "Items");
            combinedFolders = Path.Combine(combinedFolders, "SpecialIngredients");

            this._sprite = Game.ContentManager.loadTexture2D(Path.Combine(combinedFolders, this.itemName));
        }
    }
}
