using Assets.Scripts.Cooking.Recipes;
using Assets.Scripts.GameInformation;
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
        public bool hidden;

        private SpriteRenderer renderer;
        public SpriteRenderer Renderer
        {
            get
            {
                if (renderer == null)
                {
                    renderer = this.gameObject.GetComponent<SpriteRenderer>();
                    return renderer;
                }
                else
                {
                    return renderer;
                }

            }
        }

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
            this.inventory = new Inventory(4);
            this.facingDirection = Enums.FacingDirection.Down;
            this.hidden = false;
        }


        /// <summary>
        /// This makes the player's sprite invisible but DOESN'T make the player "hidden"
        /// </summary>
        /// <param name="visibility"></param>
        public void setSpriteVisibility(Enums.Visibility visibility)
        {
            if (visibility == Enums.Visibility.Invisible)
            {
                Renderer.enabled = false;
            }
            else
            {
                Renderer.enabled = true;
            }
        }

        /// <summary>
        /// Sets if the player is hidden or not.
        /// </summary>
        /// <param name="visibility"></param>
        public void setPlayerHidden(Enums.Visibility visibility)
        {
            if(visibility== Enums.Visibility.Invisible)
            {
                Color c = Renderer.color;
                hidden = true;
                Renderer.color = new Color(c.r, c.g, c.b, 0.5f);
            }
            else
            {
                Color c = Renderer.color;
                hidden = false;
                Renderer.color = new Color(c.r, c.g, c.b, 1.0f);
            }
        }


        public int getIngredientsCountForRecipes(string recipeName)
        {
            Dictionary<string, Recipe> recipes = Game.CookBook.getAllRecipes();

            List<string> items = recipes[recipeName].itemsNeeded;
            List<int> ingredientsNumberList = new List<int>();

            foreach (string item in items)
            {
                int value = this.inventory.Contains(item) ? this.inventory.getItem(item).stack : 0;
                ingredientsNumberList.Add(value);
            }
            int min = Convert.ToInt32(ingredientsNumberList.Min());
            return min;
        }

    }
}
