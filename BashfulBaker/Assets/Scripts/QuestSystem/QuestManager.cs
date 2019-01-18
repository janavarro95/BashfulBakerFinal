using Assets.Scripts.Items;
using Assets.Scripts.QuestSystem.Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.QuestSystem
{
    /// <summary>
    /// The quest manager which manages all of the quests.
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
                if (q is DeliveryQuest)
                {
                    if (q.IsCompleted) continue; //Don't want to throw away dishes at completed quests.
                    bool delivered=(q as DeliveryQuest).deliverDish(Dish, Zone);
                    return delivered; //If the dish was accepted, return true, otherwise return false;
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
