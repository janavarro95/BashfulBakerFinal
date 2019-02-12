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
        public Ingredient()
        {

        }

        public Ingredient(string ItemName) : base(ItemName)
        {

        }

        public override Item clone()
        {
            return base.clone();
        }

    }
}
