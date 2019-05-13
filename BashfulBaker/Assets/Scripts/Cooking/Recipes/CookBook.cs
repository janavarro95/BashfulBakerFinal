using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Cooking.Recipes
{
    /// <summary>
    /// A class which handles all of the recipes for the game.
    /// </summary>
    public class CookBook
    {
        public static CookBook CookingRecipes;

        /// <summary>
        /// A list of all of the recipes in the game.
        /// </summary>
        public Dictionary<Enums.CookingStation, Dictionary<string, Recipe>> Recipes;

        public CookBook()
        {
            return;
            //Initialize();
        }

        /// <summary>
        /// Initializes all of the dictionaries necessary to hold cooking recipes.
        /// </summary>
        public void Initialize()
        {
            if (Recipes != null) return; //Prevent it from being called twice.
            Recipes = new Dictionary<Enums.CookingStation, Dictionary<string, Recipe>>();
            foreach (Enums.CookingStation station in Enums.GetValues<Enums.CookingStation>())
            {
                Recipes.Add(station, new Dictionary<string, Recipe>());
            }
            LoadRecipesFromJSONFiles();

           //CreateInitialJsonFiles();
            

            //Debug.Log("Create the cookbook!");
        }


        private void CreateInitialJsonFiles()
        {
            if (Recipes[Enums.CookingStation.Oven].ContainsKey("Cookie")) return;
            Recipes[Enums.CookingStation.Oven].Add("Cookie", new Recipe("Cookie",new List<string>()
            {
                "Eggs",
                "Sugar",
                "Flour"
            }));
            SerializeRecipes();

        }

        /// <summary>
        /// Loads all of the recipes from .json files.
        /// </summary>
        public void LoadRecipesFromJSONFiles()
        {
            DeserializeRecipes();
        }

        /// <summary>
        /// Adds a recipe to a .json file.
        /// </summary>
        /// <param name="CookingStation">The enum representing what cooking station the player is interacting with.</param>
        /// <param name="Recipe">The recipe to add into the cook book.</param>
        private void AddRecipe(Enums.CookingStation CookingStation, Recipe Recipe)
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
        public bool CanCookThis(Enums.CookingStation CookingStation,string RecipeName,List<Item> Ingredients)
        {
            return Recipes[CookingStation][RecipeName].canCook(Ingredients);
        }

        /// <summary>
        /// Cooks the recipe by getting a copy of what the output item is going to be.
        /// </summary>
        /// <param name="CookingStation">The enum representing what cooking station the player is interacting with.</param>
        /// <param name="RecipeName">The name of the recipe to cook.</param>
        /// <returns></returns>
        public Item Cook(Enums.CookingStation CookingStation,string RecipeName)
        {
            //Just get a copy of the output for now. We can figure out inventory management later.
            return Recipes[CookingStation][RecipeName].cook();
        }

        public void SerializeRecipes()
        {

            string recipesPath = Path.Combine(Path.Combine(Application.streamingAssetsPath, "JSON"), "Recipes");
            foreach(KeyValuePair<Enums.CookingStation,Dictionary<string,Recipe>> pair in Recipes)
            {
                string stationPath = Path.Combine(recipesPath, pair.Key.ToString());
                Directory.CreateDirectory(stationPath);
                foreach(KeyValuePair<string,Recipe> recipe in Recipes[pair.Key])
                {
                    GameInformation.Game.Serializer.Serialize(Path.Combine(stationPath, recipe.Key + ".json"), recipe.Value);
                }
            }

        }

        /// <summary>
        /// Loads all JSON files from the asssets/json folder for recipes.
        /// </summary>
        public void DeserializeRecipes()
        {
            string recipesPath = Path.Combine(Path.Combine(Application.streamingAssetsPath, "JSON"), "Recipes");
            string[] folders = Directory.GetDirectories(recipesPath);
            foreach(string cookingStation in folders)
            {
                string[] files = Directory.GetFiles(cookingStation,"*.json");
                foreach(string recipe in files)
                {
                    if (recipe.Contains(".meta")) continue;
                    Recipe deserialized=GameInformation.Game.Serializer.Deserialize<Recipe>(recipe);
                    DirectoryInfo info = new DirectoryInfo(cookingStation);
                    Enums.CookingStation station=(Enums.CookingStation)Enum.Parse(typeof(Enums.CookingStation), info.Name, true);
                    //Debug.Log("Added :" + deserialized.name + " From: " + recipe);
                    Recipes[station].Add(deserialized.name, deserialized);
                }
            }

            foreach (KeyValuePair<Enums.CookingStation, Dictionary<string, Recipe>> pair in Recipes)
            {
                string stationPath = Path.Combine(recipesPath, pair.Key.ToString());
                Directory.CreateDirectory(stationPath);
                foreach (KeyValuePair<string, Recipe> recipe in Recipes[pair.Key])
                {
                    GameInformation.Game.Serializer.Serialize(Path.Combine(stationPath, recipe.Key + ".json"), recipe.Value);
                }
            }
        }

        /// <summary>
        /// Gets a list of all of the recipes in the game.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Recipe> getAllRecipes() { 
        
            Dictionary<string, Recipe> listOfRecipes = new Dictionary<string, Recipe>();
            foreach (KeyValuePair<Enums.CookingStation, Dictionary<string, Recipe>> book in this.Recipes)
            {
                foreach(KeyValuePair<string,Recipe> recipe in book.Value)
                {
                    listOfRecipes.Add(recipe.Key,recipe.Value);
                }
            }
            return listOfRecipes;
        }

    }
}
