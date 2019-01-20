using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Cooking.Recipes
{
    public class Recipe
    {
        public string name;
        public List<string> itemsNeeded;
        public string output;


        public Recipe()
        {
            this.name = "";
            this.itemsNeeded = new List<string>();
            this.output = null;
        }

        public Recipe(string Name, List<string> Ingredients)
        {
            this.name = Name;
            this.itemsNeeded = Ingredients;
            this.output = Name;
        }

        public bool canCook(List<Item> ingredients)
        {
            foreach(Item item in ingredients)
            {
                //var i where i.property equals someValue
                if (itemsNeeded.Contains(item.Name)) continue;
                else return false;
            }
            return true;
        }

        public Item cook()
        {
            //Get a clone of the game object Item.
            throw new NotImplementedException("Need to implement cooking a recipe!");
        }

    }
}
