using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class Dish:Item
    {
        public List<Item> ingredients;

        public Dish() : base()
        {

        }

        public Dish(string DishName): base(DishName)
        {

        }

        public override GameObject loadFromPrefab()
        {
            string path =Path.Combine(Path.Combine("Prefabs", "Dishes"), this.Name);
            return (GameObject)Resources.Load(path, typeof(GameObject));
        }

        public static GameObject LoadDishFromPrefab(string ItemName)
        {
            string path = Path.Combine(Path.Combine("Prefabs", "Dishes"), ItemName);
            return (GameObject)Resources.Load(path, typeof(GameObject));
        }
    }
}
