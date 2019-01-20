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
        public List<Item> items;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Inventory()
        {
            this.items = new List<Item>();
        }

        /// <summary>
        /// Checks if the player's inventory contains a said item.
        /// </summary>
        /// <param name="I"></param>
        /// <returns></returns>
        public bool Contains(Item I)
        {
            return items.Contains(I);
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
            return this.items.Remove(I);
        }

        /// <summary>
        /// Add an item to the player's inventory.
        /// </summary>
        /// <param name="I"></param>
        public void Add(Item I)
        {
            this.items.Add(I);
        }
    }
}
