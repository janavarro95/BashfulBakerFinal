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
        HUD InventoryHUD;
        TimerHUD TimerHUD;


        public bool showHUD=true;

        public bool showInventory=true;
        public bool showTimer=true;
        public bool showQuests=true;

        public void Awake()
        {
            Game.HUD = this;
            DontDestroyOnLoad(this.gameObject);
        }

        public override void Start()
        {

            Game.HUD.showHUD = true;
            Game.HUD.showInventory = true;
            Game.HUD.showTimer = true;
            InventoryHUD = this.gameObject.transform.Find("InventoryHUD").gameObject.GetComponent<HUD>();
            TimerHUD = this.gameObject.transform.Find("TimerHUD").gameObject.GetComponent<TimerHUD>();

            
        }

        public override void Update()
        {
            if (showHUD)
            {
                setVisibility(Enums.Visibility.Visible);
            }
            else
            {
                setVisibility(Enums.Visibility.Invisible);
            }

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

        public override void setVisibility(Enums.Visibility visibility)
        {
            toggleInventoryVisibility(visibility);
            toggleTimerVisibility(visibility);
            toggleQuestVisibility(visibility);
        }

        public void toggleInventoryVisibility(Enums.Visibility Visibility)
        {
            InventoryHUD.setVisibility(Visibility);
        }

        public void toggleTimerVisibility(Enums.Visibility Visibility)
        {
            TimerHUD.setVisibility(Visibility);
        }

        public void toggleQuestVisibility(Enums.Visibility Visibility)
        {
            
        }

    }
}
