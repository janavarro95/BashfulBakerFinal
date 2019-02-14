using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Characters.NPCS
{
    /// <summary>
    /// Deals with all the preferences in food and ingredient an NPC has.
    /// </summary>
    [Serializable,SerializeField]
    public class NPCPreferences
    {
        /// <summary>
        /// A list of all of the dishes an npc likes.
        /// </summary>
        public List<string> likedDishes;
        /// <summary>
        /// A list of all of the special ingredients an npc likes.
        /// </summary>
        public List<string> likedSpecialIngredients;
        /// <summary>
        /// A list of all of the special ingredients a npc is alergic to/dislikes.
        /// </summary>
        public List<string> dislikedIngredients;

        /// <summary>
        /// Generates a cooking quest from an npc's preferences.
        /// </summary>
        /// <param name="npcName"></param>
        /// <returns></returns>
        public QuestSystem.Quests.CookingQuest generateCookingQuest(string npcName)
        {
            string dishName = likedDishes[UnityEngine.Random.Range(0, likedDishes.Count - 1)];
            return GameInformation.Game.QuestManager.generateCookingQuest(dishName,npcName);
        }

        /// <summary>
        /// Generates a cooking quest from an npc's preferences.
        /// </summary>
        /// <param name="npcName"></param>
        /// <param name="minWantedIngredients">The min range of special ingredients wanted.</param>
        /// <param name="maxIngredientsWanted">The max range of special ingredients wanted.</param>
        /// <returns></returns>
        public QuestSystem.Quests.CookingQuest generateCookingQuest(string npcName,int minWantedIngredients,int maxIngredientsWanted)
        {
            List<string> wantedIngredients = new List<string>();
            List<string> copyOfLikedIngredients = this.likedSpecialIngredients.ToList();
            int specialIngredients = 0;
            if (minWantedIngredients > maxIngredientsWanted) minWantedIngredients = maxIngredientsWanted;
            int range = 0;
            if (maxIngredientsWanted == 0) range = 0;
            else if (minWantedIngredients == maxIngredientsWanted) range = maxIngredientsWanted;
            else range = UnityEngine.Random.Range(minWantedIngredients, maxIngredientsWanted);
            while (specialIngredients <= range)
            {
                if (copyOfLikedIngredients.Count==0) break;
                specialIngredients++;
                string ingredient = copyOfLikedIngredients[UnityEngine.Random.Range(0, copyOfLikedIngredients.Count - 1)];
                wantedIngredients.Add(ingredient);
                copyOfLikedIngredients.Remove(ingredient);             
            }

            string dishName = likedDishes[UnityEngine.Random.Range(0,likedDishes.Count-1)];
            return GameInformation.Game.QuestManager.generateCookingQuest(dishName,npcName, wantedIngredients);
        }

    }
}
