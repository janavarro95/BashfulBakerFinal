using Assets.Scripts.Items;
using Newtonsoft.Json;
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
    [Serializable,SerializeField]
    public class PlayerInfo
    {
        /// <summary>
        /// The player's inventory.
        /// </summary>
        public Inventory inventory;

        public Enums.FacingDirection facingDirection;

        [JsonIgnore]
        private GameObject _gameObject;
        [JsonIgnore]
        public GameObject gameObject
        {
            get
            {
                if (_gameObject == null)
                {
                    _gameObject = GameObject.FindWithTag("Player");
                    GameObject.DontDestroyOnLoad(_gameObject);
                    return _gameObject;
                }
                else
                {
                    return _gameObject;
                }
            }
            set
            {
                if (value.tag == "Player")
                {
                    _gameObject = value;
                }
            }
        }
        public Vector3 position
        {
            get
            {
                return this.gameObject.transform.position;
            }
            set
            {
                this.gameObject.transform.position = value;
            }
        }


        /// <summary>
        /// Constructor.
        /// </summary>
        public PlayerInfo()
        {
            this.inventory = new Inventory();
            this.facingDirection = Enums.FacingDirection.Down;
        }

        public void setVisibility(Enums.Visibility visibility)
        {
            if (visibility == Enums.Visibility.Invisible)
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}
