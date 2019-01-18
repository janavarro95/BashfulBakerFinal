﻿using Assets.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.QuestSystem.Quests
{
    /// <summary>
    /// A cooking quest. This is what the player has to do to make the thing!
    /// </summary>
    public class CookingQuest:Quest
    {
        /// <summary>
        /// The name of the dish that the player is required to make.
        /// </summary>
        private string requiredDishName;
        /// <summary>
        /// The name of the person the dish should be delivered to.
        /// </summary>
        private string personToDeliverTo;

        /// <summary>
        /// A list of all of the wanted ingredients. This is a list of all of the ingredients to make the dish plush any extra goodies such as raspberries.
        /// </summary>
        private List<string> wantedIngredients;
        /// <summary>
        /// A list of all of the unwanted ingredients. This is for NPCs that are picky or have alergies.
        /// </summary>
        private List<string> unwantedIngredients;

        /// <summary>
        /// The name of the required dish to make.
        /// </summary>
        public string RequiredDish
        {
            get
            {
                return requiredDishName;
            }
        }

        /// <summary>
        /// The name of the person to deliver to. This will match up to a delivery zone somewhere in the world.
        /// </summary>
        public string PersonToDeliverTo
        {
            get
            {
                return personToDeliverTo;
            }
        }

        /// <summary>
        /// Base constructor.
        /// </summary>
        public CookingQuest():this("","",null,null)
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="RequiredDishName">The name of the wanted dish.</param>
        /// <param name="PersonToDeliverTo">The name of the person to deliver to. I.E Mr. Jenkins</param>
        /// <param name="WantedIngredients">All of the wanted ingredients in the list.</param>
        /// <param name="UnWantedIngredients">All of the wanted ingredients in the list.</param>
        public CookingQuest(string RequiredDishName, string PersonToDeliverTo, List<string> WantedIngredients):this(RequiredDishName,PersonToDeliverTo,WantedIngredients,null)
        {
            
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="RequiredDishName">The name of the wanted dish.</param>
        /// <param name="PersonToDeliverTo">The name of the person to deliver to. I.E Mr. Jenkins</param>
        /// <param name="WantedIngredients">All of the wanted ingredients in the list.</param>
        /// <param name="UnWantedIngredients">All of the wanted ingredients in the list.</param>
        public CookingQuest(string RequiredDishName, string PersonToDeliverTo, List<string> WantedIngredients, List<string> UnWantedIngredients) :base()
        {
            this.requiredDishName = RequiredDishName ?? "";
            this.personToDeliverTo = PersonToDeliverTo ?? "";
            this.wantedIngredients = WantedIngredients ?? new List<string>();
            this.unwantedIngredients = UnWantedIngredients ?? new List<string>();
        }

        /// <summary>
        /// Checks if the quest is completed or not.
        /// </summary>
        public override void checkForCompletion()
        {
            throw new NotImplementedException("NEED TO IMPLEMENT COMPLETION FOR COOKING QUEST!!!");
            //get list of all dishes in player's inventory and check if they have a correct dish.
            //If so set the IsComplete variable to true.
        }

        /// <summary>
        /// Checks to see if the player has the required dish in their inventory by comparing a dish to the required.
        /// </summary>
        /// <param name="DishToCheck"></param>
        private void checkForCompletion(Dish DishToCheck)
        {
            if (DishToCheck.Name != this.RequiredDish) return;

           
            //Look through wanted ingredients to make sure they are all there with no extra garbage.
            foreach(Ingredient I in DishToCheck.ingredients)
            {
                if (this.wantedIngredients.Contains(I.Name)) continue;
                else return; //If the dish contains an ingredient not in the wanted list return and the quest doesn't check out.
            }
            //Look though unwanted ingredients to make sure none of them are there.
            foreach(Ingredient I in DishToCheck.ingredients)
            {
                if (this.unwantedIngredients.Contains(I.Name)) return; //In case we are doing something like alergies later down the line. We don't want to include something the patron might not like!
            }

            //If you pass all of this then I guess you win!
            this.IsCompleted = true;
        }

        /// <summary>
        /// Generate a delivery quest based off of the information from this cooking quest, aka the client's order.
        /// </summary>
        /// <returns></returns>
        public DeliveryQuest generateDeliveryQuest()
        {
            if (this.IsCompleted)
            {
                return new DeliveryQuest(this.requiredDishName, this.personToDeliverTo);
            }
            else return null;
        }

        /// <summary>
        /// Gets a clone of the quest and sets the IsComplete bool to false. Good for making quick copies of quests.
        /// </summary>
        /// <returns></returns>
        public override Quest Clone()
        {
            return new CookingQuest(this.requiredDishName, this.personToDeliverTo, this.wantedIngredients, this.unwantedIngredients);
        }
    }
}
