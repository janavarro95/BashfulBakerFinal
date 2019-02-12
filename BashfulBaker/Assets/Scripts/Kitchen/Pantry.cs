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

            inventory.Add(Ingredient.LoadIngredientFromPrefab("Dark Chocolate Chip"), 10);
            Debug.Log(inventory.getItem("Dark Chocolate Chip").stack);
        }

        /// <summary>
        /// Takes an item from the pantry and adds it to the player's inventory.
        /// </summary>
        /// <param name="I"></param>
        public bool takeItem(Item I,int amount=1)
        {
                if (inventory.Contains(I))
                {
                    if (inventory.containsEnoughOf(I, amount))
                    {
                        GameInformation.Game.Player.inventory.Add(I,amount);
                        inventory.removeAmount(I,amount);
                        return true;
                    }
                }
            return false;
        }

        public bool takeItem(string I, int amount = 1)
        {
            if (inventory.Contains(I))
            {
                if (inventory.containsEnoughOf(I, amount))
                {

                    Debug.Log("BEFORE:" + this.inventory.getItem(I).stack);

                    Debug.Log("CULPRATE HERE");

                    Item clone = this.inventory.getItem(I).clone();
                    clone.stack = amount;

                    GameInformation.Game.Player.inventory.Add(clone, amount);

                    Debug.Log("After:" + this.inventory.getItem(I).stack);
                    inventory.removeAmount(I, amount);
                    return true;
                }
            }
            return false;
        }

        public bool takeOne(Item I)
        {
            return takeItem(I, 1);
        }

        public bool takeOne(string ItemName)
        {
            return takeItem(ItemName, 1);
        }

        public bool storeItem(Item I, int Amount = 1)
        {
            if (GameInformation.Game.Player.inventory.Contains(I))
            {
                if (GameInformation.Game.Player.inventory.containsEnoughOf(I, Amount))
                {
                    GameInformation.Game.Player.inventory.removeAmount(I, Amount);
                    inventory.Add(I, Amount);
                    return true;
                }
            }
            return false;
        }

        public bool storeItem(string ItemName, int Amount = 1)
        {
            if (GameInformation.Game.Player.inventory.Contains(ItemName))
            {
                if (GameInformation.Game.Player.inventory.containsEnoughOf(ItemName, Amount))
                {
                    GameInformation.Game.Player.inventory.removeAmount(ItemName, Amount);
                    inventory.Add(GameInformation.Game.Player.inventory.getItem(ItemName), Amount);
                    return true;
                }
            }
            return false;
        }

        public bool storeOne(Item I)
        {
            return storeItem(I, 1);
        }

        public bool storeOne(string ItemName)
        {
            return storeItem(ItemName, 1);
        }
    }
}
