using Assets.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.QuestSystem.Quests
{
    /// <summary>
    /// A delivery quest! Use these to keep track if the player has successfully given the dish to the correct person!
    /// </summary>
    public class DeliveryQuest :Quest
    {
        /// <summary>
        /// The name of the NPC to deliver the dish to.
        /// </summary>
        private string personToDeliverTo;
        /// <summary>
        /// The dish to deliver.
        /// </summary>
        private string dishToDeliver;

        /// <summary>
        /// The name of the NPC to deliver the dish to.
        /// </summary>
        public string PersonToDeliverTo
        {
            get
            {
                return personToDeliverTo;
            }
        }
        /// <summary>
        /// The name of the dish to deliver.
        /// </summary>
        public string DishToDeliver
        {
            get
            {
                return dishToDeliver;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public DeliveryQuest():this("","")
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="DishName">The name of the dish to deliver.</param>
        /// <param name="ClientName">The name of the client whom requested the dish.</param>
        public DeliveryQuest(string DishName, string ClientName)
        {
            this.personToDeliverTo = ClientName;
            this.dishToDeliver = DishName;
        }

        /// <summary>
        /// Checks to see if the quest has been completed.
        /// </summary>
        public override void checkForCompletion()
        {
            //throw new NotImplementedException("Need to check for DeliveryQuestCompletion");
        }

        /// <summary>
        /// Gets a clone of the delivery quest.
        /// </summary>
        /// <returns></returns>
        public override Quest Clone()
        {
            return new DeliveryQuest(this.dishToDeliver, this.personToDeliverTo);
        }

        /// <summary>
        /// Delivers a dish to the completed quest. 
        /// </summary>
        /// <param name="Dish">The dish to deliver</param>
        /// <param name="DropOffZone">The drop off zone which contains the list of npc names.</param>
        /// <returns></returns>
        public bool deliverDish(Dish Dish,DeliveryDropOffZone DropOffZone)
        {
            if (Dish.Name == this.dishToDeliver && DropOffZone.npcNamesWhoLiveHere.Contains(this.personToDeliverTo))
            {
                //Debug.Log("Dish: " + Dish.Name + " has been delivered to: " + this.personToDeliverTo);
                this.IsCompleted = true;
                return true; 
            }
            return false;
        }

    }
}
