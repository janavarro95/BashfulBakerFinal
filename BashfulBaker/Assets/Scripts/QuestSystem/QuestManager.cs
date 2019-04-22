using Assets.Scripts.Characters.NPCS;
using Assets.Scripts.Cooking.Recipes;
using Assets.Scripts.GameInformation;
using Assets.Scripts.Items;
using Assets.Scripts.QuestSystem.Quests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.QuestSystem
{
    /// <summary>
    /// The quest manager which manages all of the quests.
    /// 
    /// TODO:
    ///     Generate dialogue for the most recently recieved quest?
    /// </summary>
    public class QuestManager
    {
        /// <summary>
        /// A reference to the quest manager for the game.
        /// </summary>
        public static QuestManager Quests;

        /// <summary>
        /// A list of quests held by the quest manager.
        /// </summary>
        public List<Quest> quests;

        public QuestManager()
        {
            this.quests = new List<Quest>();
            /*
            quests.Add(new CookingQuest("Example", "Mr.Example", new List<string>()
            {
                "Raspberries"
            }, null));
            */
            
            serializeCookingQuests();

            
        }

        public void addQuest(Quest q)
        {
            this.quests.Add(q);
        }

        /// <summary>
        /// Broken:
        ///     Issue: Needs to find a way to get a list of all npcs in the game.
        /// </summary>
        /// <returns></returns>
        public CookingQuest generateCookingQuest(NPC npc) {
            return npc.preferences.generateCookingQuest(npc.Name);
        }

        /// <summary>
        /// Generates a random cooking quest.
        /// </summary>
        /// <param name="ClientName">The person to deliver the dish to.</param>
        /// <returns></returns>
        public CookingQuest generateCookingQuest(string ClientName)
        {
            CookingQuest newQuest = generateCookingQuest(ClientName, null, null);
            return newQuest;
        }

        /// <summary>
        /// Generates a random cooking quest.
        /// </summary>
        /// <param name="ClientName">The person to deliver the dish to.</param>
        /// <param name="SpecialIngredientsWanted">A list containing the names of the special ingredients wanted I.E cherries.</param>
        /// <returns></returns>
        public CookingQuest generateCookingQuest(string ClientName,List<string> SpecialIngredientsWanted)
        {
            CookingQuest newQuest = generateCookingQuest(ClientName, SpecialIngredientsWanted, null);
            return newQuest;
        }

        /// <summary>
        /// Generates a random cooking quest.
        /// </summary>
        /// <param name="ClientName">The person to deliver the dish to.</param>
        /// <param name="SpecialIngredientsWanted">A list containing the names of the special ingredients wanted I.E cherries.</param>
        /// <param name="UnwantedIngredients">A list containing the list of unwanted ingredients for say maybe preferences or alergies.</param>
        /// <returns></returns>
        public CookingQuest generateCookingQuest(string ClientName, List<string> SpecialIngredientsWanted,List<string> UnwantedIngredients)
        {
            var recipes= Game.CookBook.getAllRecipes();
            List<string> keys=new List<string>();
            foreach(var recipe in recipes)
            {
                keys.Add(recipe.Key);
            }

            int index = UnityEngine.Random.Range(0, recipes.Count - 1);
            CookingQuest newQuest = new CookingQuest(keys[index], ClientName, SpecialIngredientsWanted,UnwantedIngredients);
            return newQuest;
        }

        /// <summary>
        /// Generates a cooking quest for a specific dish.
        /// </summary>
        /// <param name="RequestedDish">The name of the dish to make.</param>
        /// <param name="ClientName">The person to deliver the dish to.</param>
        /// <param name="SpecialIngredientsWanted">A list containing the names of the special ingredients wanted I.E cherries.</param>
        /// <param name="UnwantedIngredients">A list containing the list of unwanted ingredients for say maybe preferences or alergies.</param>
        public CookingQuest generateCookingQuest(string RequestedDish, string ClientName, List<string> SpecialIngredientsWanted=null, List<string> UnwantedIngredients=null)
        {
            Dictionary<string,Recipe> recipes = Game.CookBook.getAllRecipes();

            //Sanity checking to make sure the recipe is a valid one.
            string recipeName = "";
            foreach(KeyValuePair<string,Recipe> value in recipes)
            {
                if (value.Key.Equals(RequestedDish))
                {
                    recipeName = value.Key;
                }
            }
            if (String.IsNullOrEmpty(recipeName)) throw new Exception("Recipe not found! Not generating cooking quest!");

            CookingQuest newQuest = new CookingQuest(RequestedDish, ClientName, SpecialIngredientsWanted, UnwantedIngredients);
            return newQuest;
        }


        /// <summary>
        /// Loads a cooking quest from a .json file and adds it to the quest manager.
        /// </summary>
        /// <param name="fileName">The name of the file without the .json extension. I.E Cheescake, or Day1_Quest</param>
        /// <returns></returns>
        public CookingQuest loadCookingQuest(string fileName, bool addToQuestManager = true)
        {
            string cookingQuests = Path.Combine(Path.Combine(Path.Combine(Application.streamingAssetsPath,"JSON"),"Quests"),"CookingQuests");

            string[] files = Directory.GetFiles(cookingQuests, "*.json");
            foreach (string quest in files)
            {
                if (quest.Contains(".meta")) continue;
                if (fileName == Path.GetFileNameWithoutExtension(quest))
                {
                    CookingQuest deserialized = GameInformation.Game.Serializer.Deserialize<CookingQuest>(quest);
                    if (addToQuestManager) quests.Add(deserialized);
                    return deserialized;
                }
            }
            return null;
        }

        private void serializeCookingQuests()
        {
            string cookingQuests = Path.Combine(Path.Combine(Path.Combine(Application.streamingAssetsPath, "JSON"), "Quests"), "CookingQuests");
            Directory.CreateDirectory(cookingQuests);


            foreach (Quest q in this.quests)
            {
                Game.Serializer.Serialize(Path.Combine(cookingQuests, (q as CookingQuest).RequiredDish + ".json"),q);
            }
            return;
        }

        /// <summary>
        /// Checks a cooking quest for completion.
        /// </summary>
        /// <param name="dish"></param>
        /// <returns></returns>
        public Enums.QuestCompletionStatus checkCookingQuestCompletionStatus(Dish dish)
        {
            if (checkForCookingQuestSpecialCompletion(dish)) return Enums.QuestCompletionStatus.SpecialMissionCompleted;
            else if (checkForCookingQuestCompletion(dish) == true) return Enums.QuestCompletionStatus.Completed;
            else return Enums.QuestCompletionStatus.NotCompleted;
        }

        /// <summary>
        /// Checks to see if a cooking quest has been completed.
        /// </summary>
        /// <param name="dish">The dish to pass in to see if it fufills a quest requirement.</param>
        /// <returns></returns>
        private bool checkForCookingQuestCompletion(Dish dish)
        {
            foreach (Quest q in quests)
            {
                if (q is CookingQuest)
                {
                    if (q.IsCompleted) continue; //Don't want to throw away dishes at completed quests.
                    (q as CookingQuest).checkForCompletion(dish);
                    return (q as CookingQuest).IsCompleted;
                }
            }
            return false;
        }

        public List<CookingQuest> getCookingQuests()
        {
            List<CookingQuest> cookingQuests = new List<CookingQuest>();
            foreach (Quest q in quests)
            {
                if (q is CookingQuest)
                {
                    cookingQuests.Add((CookingQuest)q);
                }
            }
            return cookingQuests;
        }

        /// <summary>
        /// Checks to see if a cooking quest's special mission has been completed.
        /// </summary>
        /// <param name="dish">The dish to pass in to see if it fufills a quest's special requirement.</param>
        /// <returns></returns>
        private bool checkForCookingQuestSpecialCompletion(Dish dish)
        {
            foreach (Quest q in quests)
            {
                if (q is CookingQuest)
                {
                    if (q.IsCompleted) continue; //Don't want to throw away dishes at completed quests.
                    (q as CookingQuest).checkForCompletion(dish);
                    return (q as CookingQuest).specialMissionCompleted();
                }
            }
            return false;
        }

        /// <summary>
        /// Checks for the completion of a delivery quest.
        /// </summary>
        /// <param name="Dish">The dish to be delivered.</param>
        /// <param name="Zone">The drop off zone acript which contains all of the npc names.</param>
        /// <returns>If the dish has been sucessfully delivered or not.</returns>
        public bool checkForDeliveryQuestCompletion(Dish Dish, DeliveryDropOffZone Zone)
        {
            foreach (Quest q in quests)
            {
                if (q is CookingQuest)
                {
                    //Debug.Log("Found a delivery quest!");
                    if (q.IsCompleted) continue; //Don't want to throw away dishes at completed quests.
                    bool delivered = (q as CookingQuest).deliveryQuestPart.deliverDish(Dish, Zone);
                    q.IsCompleted = true;
                    (q as CookingQuest).deliveryQuestPart.IsCompleted = true;
                    return delivered; //If the dish was accepted, return true, otherwise return false;
                }
                else
                {
                    //Debug.Log("Found a different quest:" + q.GetType().ToString());
                }
            }
            return false;
        }

        /// <summary>
        /// Clears all of the quests from the quest manager.
        /// </summary>
        public void Clear()
        {
            this.quests.Clear();
        }

        /// <summary>
        /// Allows the quest manager to be used in foreach loops.
        /// </summary>
        /// <returns></returns>
        public List<Quest>.Enumerator GetEnumerator()
        {
            return this.quests.GetEnumerator();
        }

    }
}
