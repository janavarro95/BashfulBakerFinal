using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class Ingredient:Item
    {

        /// <summary>
        /// Loads an asset from the list of prefabs.
        /// </summary>
        /// <returns></returns>
        public override GameObject loadFromPrefab()
        {
            string path = Path.Combine(Path.Combine("Prefabs", "Ingredients"), this.Name);
            return (GameObject)Resources.Load(path, typeof(GameObject));
        }

        /// <summary>
        /// Loads an ingredient from it's prefab.
        /// </summary>
        /// <param name="ItemName"></param>
        /// <returns></returns>
        public static new GameObject LoadItemFromPrefab(string ItemName)
        {
            string path = Path.Combine(Path.Combine("Prefabs", "Ingredients"), ItemName);
            return (GameObject)Resources.Load(path, typeof(GameObject));
        }

        /// <summary>
        /// Loads an ingredient from prefab.
        /// </summary>
        /// <param name="ItemName"></param>
        /// <returns></returns>
        public static Ingredient LoadIngredientFromPrefab(string ItemName)
        {
            return LoadItemFromPrefab(ItemName).GetComponent<Ingredient>();
        }

        /// <summary>
        /// Loads an ingredient from prefab and gives it a specific stack size.
        /// </summary>
        /// <param name="ItemName">The item to load.</param>
        /// <param name="StackSize">The amount of items to load in.</param>
        /// <returns></returns>
        public static Ingredient LoadIngredientFromPrefab(string ItemName,int StackSize)
        {
            Ingredient i=LoadItemFromPrefab(ItemName).GetComponent<Ingredient>();
            i.stack = StackSize;
            return i;
        }

        public override Item clone()
        {
            //Implement this.
            return loadFromPrefab().GetComponent<Ingredient>();
        }
    }
}
