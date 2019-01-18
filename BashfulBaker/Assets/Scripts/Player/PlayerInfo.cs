using Assets.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Player
{
    /// <summary>
    /// All of the player's info.
    /// </summary>
    /// 
    [Serializable,SerializeField]
    public class PlayerInfo
    {
        /// <summary>
        /// The player's inventory.
        /// </summary>
        public Inventory inventory;
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public PlayerInfo()
        {
            this.inventory = new Inventory();
        }
    }
}
