using Assets.Scripts.GameInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Menus.HUDS
{
    public class GameHUD:HUD
    {
        [SerializeField]
        public InventoryHUD InventoryHUD;
        public TimerHUD TimerHUD;
        public HUD QuestHUD;

        public bool showHUD=true;
        public bool showInventory=true;
        public bool showTimer=true;
        public bool showQuests=true;


        /// <summary>
        /// Initializes HUD.
        /// </summary>
        public void Awake()
        {
            Game.HUD = this;
            DontDestroyOnLoad(this.gameObject);
            Game.HUD.showHUD = true;
            Game.HUD.showInventory = true;
            Game.HUD.showTimer = true;
            Game.HUD.showQuests = false;

        }

        /// <summary>
        /// Runs after scene loaded.
        /// </summary>
        public override void Start()
        {
            InventoryHUD = this.gameObject.transform.Find("InventoryHUD").gameObject.GetComponent<InventoryHUD>();
            TimerHUD = this.gameObject.transform.Find("TimerHUD").gameObject.GetComponent<TimerHUD>();
            QuestHUD = this.gameObject.transform.Find("QuestHUD").gameObject.GetComponent<HUD>();
        }

        /// <summary>
        /// Runs ~60 fps.
        /// </summary>
        public override void Update()
        {
            checkForVisibilityUpdates();
            checkForInput();
        }

        /// <summary>
        /// Checks for input to change 
        /// </summary>
        protected void checkForInput()
        {
            if (GameInput.InputControls.RightBumperPressed)
            {
                if (showQuests)
                {
                    Menu.Instantiate<Menus.QuestMenu>();
                }
            }
            if (GameInput.InputControls.LeftBumperPressed)
            {
                if (showInventory)
                {
                    Menu.Instantiate<InventoryMenu>();
                }
            }
        }

        /// <summary>
        /// Checks every tick if the visibility of the game's HUD has changed.
        /// </summary>
        public void checkForVisibilityUpdates()
        {
            if (showHUD)
            {
                setVisibility(Enums.Visibility.Visible);

                if (showInventory)
                {
                    toggleInventoryVisibility(Enums.Visibility.Visible);
                }
                else
                {
                    toggleInventoryVisibility(Enums.Visibility.Invisible);
                }
                if (showTimer)
                {
                    toggleTimerVisibility(Enums.Visibility.Visible);
                }
                else
                {
                    toggleTimerVisibility(Enums.Visibility.Invisible);
                }
                if (showQuests)
                {
                    toggleQuestVisibility(Enums.Visibility.Visible);
                }
                else
                {
                    toggleQuestVisibility(Enums.Visibility.Invisible);
                }

            }
            else
            {
                setVisibility(Enums.Visibility.Invisible);
            }
        }

        /// <summary>
        /// Updates the visual representation of the game's inventory HUD;
        /// </summary>
        public virtual void updateInventoryHUD()
        {
            try
            {
                this.InventoryHUD.setUpComponents();
            }
            catch(Exception err)
            {

            }
        }

        /// <summary>
        /// Sets the visibility for the HUD as a whole.
        /// </summary>
        /// <param name="visibility"></param>
        public override void setVisibility(Enums.Visibility visibility)
        {
            toggleInventoryVisibility(visibility);
            toggleTimerVisibility(visibility);
            toggleQuestVisibility(visibility);
        }

        /// <summary>
        /// Toggles the visibility for the Inventory HUD component.
        /// </summary>
        /// <param name="Visibility"></param>
        public void toggleInventoryVisibility(Enums.Visibility Visibility)
        {
            InventoryHUD.setVisibility(Visibility);
        }


        /// <summary>
        /// Toggles the visibility for the Timer HUD component.
        /// </summary>
        /// <param name="Visibility"></param>
        public void toggleTimerVisibility(Enums.Visibility Visibility)
        {
            TimerHUD.setVisibility(Visibility);
        }


        /// <summary>
        /// Toggles the visibility for the Quest HUD component.
        /// </summary>
        /// <param name="Visibility"></param>
        public void toggleQuestVisibility(Enums.Visibility Visibility)
        {
            QuestHUD.setVisibility(Visibility);
        }

    }
}
