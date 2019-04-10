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
        public Inventory dishesInventory;
        public Inventory specialIngredientsInventory;


        private Item _activeItem;

        public Item activeItem
        {
            get
            {
                return _activeItem;
            }
            set
            {
                _activeItem = value;
                updateHeldItemSprite();
            }
        }

        private GameObject _heldItemGameObject;

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
                    _heldItemGameObject = _gameObject.transform.Find("HeldItem").gameObject;
                    GameObject.DontDestroyOnLoad(_gameObject);
                    arrowDirection = _gameObject.transform.Find("Rotational").Find("Arrow").gameObject.GetComponent<PlayerArrowDirection>();
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
                    _heldItemGameObject = _gameObject.transform.Find("HeldItem").gameObject;
                    arrowDirection = _gameObject.transform.Find("Rotational").Find("Arrow").gameObject.GetComponent<PlayerArrowDirection>();
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


        public PlayerArrowDirection arrowDirection;

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlayerInfo()
        {
            this.dishesInventory = new Inventory(4);
            this.specialIngredientsInventory = new Inventory(6);
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
                if (_heldItemGameObject != null)
                {
                    _heldItemGameObject.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
            else
            {
                Renderer.enabled = true;
                if (_heldItemGameObject != null)
                {
                    _heldItemGameObject.GetComponent<SpriteRenderer>().enabled = true;
                }
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
                int value = this.dishesInventory.Contains(item) ? this.dishesInventory.getItem(item).stack : 0;
                ingredientsNumberList.Add(value);
            }
            int min = Convert.ToInt32(ingredientsNumberList.Min());
            return min;
        }

        public void updateHeldItemSprite()
        {
            if (activeItem != null)
            {
                Debug.Log("NEW SPRITE");
                GameObject obj = this.gameObject;
                if (this._heldItemGameObject == null) Debug.Log("NANI???");
                if (activeItem.Sprite == null)
                {
                    Debug.Log("Active item has no sprite");
                    Debug.Log("Get the active item sprite");
                    activeItem.loadSprite();
                }

                this._heldItemGameObject.GetComponent<SpriteRenderer>().sprite = Content.ContentManager.Instance.loadSprite(activeItem.Sprite);
                this._heldItemGameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                this._heldItemGameObject.SetActive(true);
            }
            else
            {
                Debug.Log("NO SPRITE");
                if (this._heldItemGameObject == null) return;
                this._heldItemGameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                this._heldItemGameObject.SetActive(true);
            }
        }

        public void playHeldObjectAnimation(bool moving)
        {
            if (Game.Player.activeItem == null) return;
            if (Game.Player.activeItem is Dish)
            {
                this._heldItemGameObject.GetComponent<HeldObjectAnimator>().playAnimation(this.facingDirection, moving, (Dish)Game.Player.activeItem);
            }
        }

    }
}
