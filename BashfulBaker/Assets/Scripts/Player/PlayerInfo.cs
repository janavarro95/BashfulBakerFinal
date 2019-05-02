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
        /// Adds a specific amount of special ingredients to a special ingredient stack.
        /// </summary>
        /// <param name="ingredient">The ingredient to add to.</param>
        /// <param name="amount">The amount to add.</param>
        public void addSpecialIngredientForPlayer(Enums.SpecialIngredients ingredient,int amount=1)
        {
            Game.Player.specialIngredientsInventory.actualItems.Find(i => (i as SpecialIngredient).ingredientType == ingredient).stack+=amount;
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

        /// <summary>
        /// Removes the active item that the player is holding.
        /// </summary>
        /// <returns></returns>
        public Item removeActiveItem()
        {
            Item I = this.activeItem;
            this.activeItem = null;
            updateHeldItemSprite();
            return I;
        }
        
        /// <summary>
        /// Resets the visual for the player's held item.
        /// </summary>
        public void resetActiveDishFromMenu()
        {
            Game.HUD.InventoryHUD.resetActiveDish();
        }

        /// <summary>
        /// Update the currently held player animation to reflect the actual item the playe is holding.
        /// </summary>
        public void updateHeldItemSprite()
        {
            if (activeItem != null)
            {
                //Debug.Log("NEW SPRITE");
                GameObject obj = this.gameObject;
                if (this._heldItemGameObject == null) //Debug.Log("NANI???");
                if (activeItem.Sprite == null)
                {
                    //Debug.Log("Active item has no sprite");
                    //Debug.Log("Get the active item sprite");
                    activeItem.loadSprite();
                }

                this._heldItemGameObject.GetComponent<SpriteRenderer>().sprite = Content.ContentManager.Instance.loadSprite(activeItem.Sprite);
                this._heldItemGameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                this._heldItemGameObject.SetActive(true);

                this._heldItemGameObject.GetComponent<HeldObjectAnimator>().loadAnimatorFromPrefab(Game.Player.activeItem.itemName);
            }
            else
            {
                if (this._heldItemGameObject == null)
                {
                    // if it's null....
                    //... why are we accessing it?

                    //this._heldItemGameObject.GetComponent<HeldObjectAnimator>().clearAnimationController();

                    return;

                }
                this._heldItemGameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                this._heldItemGameObject.SetActive(true);
            }
        }

        /// <summary>
        /// Plays the held object animation for Dane when he is walking around.
        /// </summary>
        /// <param name="moving"></param>
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
