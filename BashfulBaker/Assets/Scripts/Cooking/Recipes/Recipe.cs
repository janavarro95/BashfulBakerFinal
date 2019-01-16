using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Cooking.Recipes
{
    public class Recipe
    {
        public string name;
        public List<Item> itemsNeeded;
        public Item output;

        public bool canCook(List<Item> ingredients)
        {
            foreach(Item item in ingredients)
            {
                //var i where i.property equals someValue
                Item valid=itemsNeeded.Find(ingredient => ingredient.Name == item.Name);
                if (valid == null) return false;
            }
            return true;
        }

        public Item cook()
        {
            //Get a clone of the game object Item.
            return output.clone();
        }

    }
}
