using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Items
{
    /// <summary>
    /// Manages a given inventory.
    /// </summary>
    [Serializable,SerializeField]
    public class Inventory
    {
        /// <summary>
        /// All of the items 
        /// </summary>
        public Dictionary<string, Item> items;

        public int maxCapaxity;

        public List<Item> actualItems
        {
            get
            {
                List<Item> localItems = new List<Item>();
                foreach (Item I in items.Values)
                {
                    localItems.Add(I);
                }
                return localItems;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Inventory(int Capacity)
        {
            this.items = new Dictionary<string, Item>();
            maxCapaxity = Capacity;
        }

        /// <summary>
        /// Checks if the player's inventory contains a said item.
        /// </summary>
        /// <param name="I"></param>
        /// <returns></returns>
        public bool Contains(Item I)
        {
            return items.ContainsKey(I.Name);
        }

        public bool Contains(string ItemName)
        {
            return items.ContainsKey(ItemName);
        }

        public Item getItem(string ItemName)
        {
            if (Contains(ItemName))
            {
                return items[ItemName];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Alows us to use foreach loops on the items in the inventory.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Item>.Enumerator GetEnumerator()
        {
            return items.GetEnumerator();
        }

        /// <summary>
        /// Removes a given item from the player's inventory.
        /// </summary>
        /// <param name="I"></param>
        /// <returns></returns>
        public bool Remove(Item I)
        {
            bool removed = this.items.Remove(I.Name);

            return removed;
        }


        public bool containsEnoughOf(Item I, int Amount)
        {
            return containsEnoughOf(I.Name, Amount);
        }

        public bool containsEnoughOf(string ItemName, int Amount)
        {
            if (this.items.ContainsKey(ItemName))
            {
                if (this.items[ItemName].stack >= Amount) return true;
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Removes a specific amount of items from a stack if possible.
        /// </summary>
        /// <param name="I"></param>
        /// <param name="Amount"></param>
        /// <returns></returns>
        public bool removeAmount(Item I, int Amount)
        {
            return removeAmount(I.Name, Amount);
        }

        /// <summary>
        /// Removes a specific amount of items from a stack if possible.
        /// </summary>
        /// <param name="ItemName"></param>
        /// <param name="Amount"></param>
        /// <returns></returns>
        public bool removeAmount(string ItemName, int amount)
        {
            if (containsEnoughOf(ItemName, amount))
            {
                this.items[ItemName].removeFromStack(amount);

                if (this.items[ItemName].stack <= 0)
                {
                    this.items.Remove(ItemName);
                }

                return true;
            }
            return false;
        }

        /// <summary>
        /// Add an item to the player's inventory.
        /// </summary>
        /// <param name="I"></param>
        public bool Add(Item I)
        {
            I.initializeSprite();
            if (this.items.Keys.Count == maxCapaxity)
            {
                Debug.Log("Inventory is full!");
                return false;
            }

            if (!this.items.ContainsKey(I.Name))
            {
                this.items.Add(I.Name, I);
                return true;
            }
            else
            {
                this.items[I.Name].addToStack(I.stack);
                return true;
            }
        }

        public bool Add(Item I, int amount)
        {
            I.initializeSprite();
            if (this.items.Keys.Count == maxCapaxity)
            {
                Debug.Log("Inventory is full!");
                return false;
            }

            if (!this.items.ContainsKey(I.Name))
            {
                Item i = (Item)I.clone();
                i.stack = amount;
                this.items.Add(i.Name, i);
                return true;
            }
            else
            {
                this.items[I.Name].addToStack(amount);
                return true;
            }
        }

        public List<Dish> getAllDishes()
        {
            List<Dish> dishList = new List<Dish>();
            foreach (KeyValuePair<string, Item> pair in this.items)
            {
                if (pair.Value.GetType() == typeof(Dish))
                {
                    dishList.Add((Dish)pair.Value);
                }
            }
            return dishList;
        }

        public List<Item> getAllItems()
        {
            return actualItems;
        }

        /// <summary>
        /// Gets a random item from the inventory.
        /// </summary>
        /// <returns></returns>
        public Item getRandomItem()
        {
            if (this.items.Keys.Count == 0) return null;

            List<Item> items = new List<Item>();
            foreach (Item item in this.items.Values)
            {
                items.Add(item);
            }
            int rando = UnityEngine.Random.Range(0, items.Count);

            return items[rando];
            //this.Remove(items[rando]);
        }

        /// <summary>
        /// Removes a random item from the inventory.
        /// </summary>
        /// <returns></returns>
        public bool removeRandomItem()
        {
            if (this.items.Keys.Count == 0) return false;
            return this.Remove(getRandomItem());
        }
    }
}
