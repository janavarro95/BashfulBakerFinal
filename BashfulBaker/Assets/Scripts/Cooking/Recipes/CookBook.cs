using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Cooking.Recipes
{
    /// <summary>
    /// A class which handles all of the recipes for the game.
    /// </summary>
    public class CookBook
    {
        /// <summary>
        /// A list of all of the recipes in the game.
        /// </summary>
        public static Dictionary<Enums.CookingStation, Dictionary<string, Recipe>> Recipes;

        /// <summary>
        /// Initializes all of the dictionaries necessary to hold cooking recipes.
        /// </summary>
        public static void Initialize()
        {
            foreach (Enums.CookingStation station in Enums.GetValues<Enums.CookingStation>())
            {
                Recipes.Add(station, new Dictionary<string, Recipe>());
            }
            LoadRecipesFromJSONFiles();
        }

        /// <summary>
        /// Loads all of the recipes from .json files.
        /// </summary>
        public static void LoadRecipesFromJSONFiles()
        {
            //Implement this.
        }

        /// <summary>
        /// Adds a recipe to a .json file.
        /// </summary>
        /// <param name="CookingStation">The enum representing what cooking station the player is interacting with.</param>
        /// <param name="Recipe">The recipe to add into the cook book.</param>
        private static void AddRecipe(Enums.CookingStation CookingStation, Recipe Recipe)
        {
            if (Recipes.ContainsKey(CookingStation))
            {
                if (Recipes[CookingStation].ContainsKey(Recipe.name)) return; //Return so we don't get an error adding a duplicate key to the dictionary.
                Recipes[CookingStation].Add(Recipe.name, Recipe);
            }
        }

        /// <summary>
        /// Checks if a recipe can be cooked.
        /// </summary>
        /// <param name="CookingStation">The enum representing what cooking station the player is interacting with.</param>
        /// <param name="RecipeName">The name of the recipe to cook.</param>
        /// <param name="Ingredients">The list of ingredients held by the player.</param>
        /// <returns></returns>
        public static bool CanCookThis(Enums.CookingStation CookingStation,string RecipeName,List<Item> Ingredients)
        {
            return Recipes[CookingStation][RecipeName].canCook(Ingredients);
        }

        /// <summary>
        /// Cooks the recipe by getting a copy of what the output item is going to be.
        /// </summary>
        /// <param name="CookingStation">The enum representing what cooking station the player is interacting with.</param>
        /// <param name="RecipeName">The name of the recipe to cook.</param>
        /// <returns></returns>
        public static Item Cook(Enums.CookingStation CookingStation,string RecipeName)
        {
            //Just get a copy of the output for now. We can figure out inventory management later.
            return Recipes[CookingStation][RecipeName].cook();
        }

    }
}
