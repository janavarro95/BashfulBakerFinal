using Assets.Scripts.GameInformation;
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
    [Serializable, SerializeField]
    public class Inventory
    {
        /// <summary>
        /// All of the items 
        /// </summary>
        private List<Item> items;

        public int maxCapaxity;

        public List<Item> actualItems
        {
            get
            {
                return items;
            }
        }

        /// <summary>
        /// Checks if the inventory is empty or not.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return this.Count == 0;
            }
        }

        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Inventory(int Capacity)
        {
            this.items = new List<Item>();
            maxCapaxity = Capacity;
        }

        /// <summary>
        /// Checks if the player's inventory contains a said item.
        /// </summary>
        /// <param name="I"></param>
        /// <returns></returns>
        public bool Contains(Item I)
        {
            return items.FindAll(i => i.Name == I.Name).Count > 0;
        }

        /// <summary>
        /// Checks if the inventory contains this item.
        /// </summary>
        /// <param name="ItemName"></param>
        /// <returns></returns>
        public bool Contains(string ItemName)
        {
            return items.FindAll(i => i.Name == ItemName).Count > 0;
        }

        /// <summary>
        /// Gets the item from the inventory with thisname.
        /// </summary>
        /// <param name="ItemName"></param>
        /// <returns></returns>
        public Item getItem(string ItemName)
        {
            if (Contains(ItemName))
            {
                return items.Find(i => i.Name == ItemName);
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
        public List<Item>.Enumerator GetEnumerator()
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
            bool removed = this.items.Remove(I);
            Game.HUD.updateInventoryHUD();
            return removed;
        }


        /// <summary>
        /// Add an item to the player's inventory.
        /// </summary>
        /// <param name="I"></param>
        public bool Add(Item I)
        {
            I.initializeSprite();
            if (this.items.Count == maxCapaxity)
            {
                //Debug.Log("Inventory is full!");
                return false;
            }
            this.items.Add(I);
            if (Game.HUD != null)
            {
                Game.HUD.updateInventoryHUD();
            }
            return true;
        }

        /// <summary>
        /// Get all the dishes from the player's dish inventory.
        /// </summary>
        /// <returns></returns>
        public List<Dish> getAllDishes()
        {
            List<Dish> dishList = new List<Dish>();
            foreach (Item I in this.items)
            {
                if (I.GetType() == typeof(Dish))
                {
                    dishList.Add((Dish)I);
                }
            }
            return dishList;
        }

        /// <summary>
        /// Gets all of the special ingredients fom the player's special ingredients inventory.
        /// </summary>
        /// <returns></returns>
        public List<SpecialIngredient> getAllSpecialIngredients()
        {
            List<SpecialIngredient> list = new List<SpecialIngredient>();
            foreach (Item I in this.items)
            {
                if (I.GetType() == typeof(SpecialIngredient))
                {
                    list.Add((SpecialIngredient)I);
                }
            }
            return list;
        }

        /// <summary>
        /// Gets all the items from a specific inventory.
        /// </summary>
        /// <returns></returns>
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
            if (this.items.Count == 0) return null;

            List<Item> items = new List<Item>();
            foreach (Item item in this.items)
            {
                items.Add(item);
            }
            int rando = UnityEngine.Random.Range(0, items.Count);

            return items[rando];
            //this.Remove(items[rando]);
        }

        /// <summary>
        /// Gets a random dish.
        /// </summary>
        /// <returns></returns>
        public Dish getRandomDish()
        {
            if (this.items.Count == 0) return null;

            List<Dish> dishes = getAllDishes();
            if (dishes.Count == 0) return null;
            int rando = UnityEngine.Random.Range(0, items.Count);

            return dishes[rando];
            //this.Remove(items[rando]);
        }
        
        /// <summary>
        /// Gets a random boxed dish.
        /// </summary>
        /// <returns></returns>
        public Dish getRandomBoxedDish()
        {
            if (this.items.Count == 0) return null;

            List<Dish> dishes = getAllDishes();
            if (dishes.Count == 0) return null;

            foreach (Dish d in dishes)
            {
                if (d.currentDishState != Enums.DishState.Packaged)
                {
                    dishes.Remove(d);
                }
            }

            int rando = UnityEngine.Random.Range(0, items.Count);
            return dishes[rando];
        }

        /// <summary>
        /// Removes a random item from the inventory.
        /// </summary>
        /// <returns></returns>
        public bool removeRandomItem()
        {
            if (this.items.Count == 0) return false;
            bool f = this.Remove(getRandomItem());
            Game.HUD.updateInventoryHUD();
            return f;
        }
    }
}
