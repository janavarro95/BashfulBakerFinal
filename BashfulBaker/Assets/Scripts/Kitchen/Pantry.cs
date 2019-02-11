using Assets.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Kitchen
{
    public class Pantry
    {
        public Inventory inventory;


        public Pantry()
        {
            inventory = new Inventory();
        }

        public void storeItem()
        {

        }

        /// <summary>
        /// Takes an item from the pantry and adds it to the player's inventory.
        /// </summary>
        /// <param name="I"></param>
        public void takeItem(Item I,int amount=1)
        {
                if (inventory.Contains(I))
                {
                    if (inventory.containsEnoughOf(I, amount))
                    {
                        GameInformation.Game.Player.inventory.Add(I,amount);
                        inventory.removeAmount(I,amount);
                    }
                }
        }

        public void takeOne(Item I)
        {
            takeItem(I, 1);
        }

        public void storeItem(Item I, int Amount = 1)
        {
            if (GameInformation.Game.Player.inventory.Contains(I))
            {
                if (GameInformation.Game.Player.inventory.containsEnoughOf(I, Amount))
                {
                    GameInformation.Game.Player.inventory.removeAmount(I, Amount);
                    inventory.Add(I, Amount);
                }
            }
        }

        public void storeOne(Item I)
        {
            storeItem(I, 1);
        }

    }
}
