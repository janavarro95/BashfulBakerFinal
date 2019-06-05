using Assets.Scripts.GameInformation;
using Assets.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.QuestSystem.Quests
{
    public class DeliveryDropOffZone : MonoBehaviour
    {
        /// <summary>
        /// A list of all of the npcs who live here. This is to be check to a delivery quest.
        /// </summary>
        public List<string> npcNamesWhoLiveHere;

        public bool shouldFinishDay;

        public void Start()
        {

        }

        /// <summary>
        /// If player is in the drop off zone and they interact with it, try to deliver the dish.
        /// </summary>
        /// <param name="collision"></param>
        public void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                //Debug.Log("HELLO!");
                if (GameInput.InputControls.APressed) //If player presses A
                {
                    List<Item> removalList = new List<Item>();
                    if (Game.Player.activeItem is Dish) //Send that dish to the quest manager....
                    {
                        bool delivered = Game.QuestManager.checkForDeliveryQuestCompletion((Game.Player.activeItem as Dish), this);
                        if (delivered == true)
                        {
                            removalList.Add(Game.Player.activeItem);
                            Game.Player.activeItem = null;
                            Game.HUD.QuestHUD.setUpMenuForDisplay();
                        }
                    }
                    else
                    {
                        //Debug.Log("Held item is not a dish!!");
                    }

                    foreach (Item I in removalList)
                    {
                        Game.Player.dishesInventory.Remove(I);
                    }
                }
            }
        }
    }
}
