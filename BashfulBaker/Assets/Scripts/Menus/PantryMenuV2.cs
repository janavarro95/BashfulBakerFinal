using Assets.Scripts.GameInformation;
using Assets.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Menus
{
    public class PantryMenuV2:Menu
    {

        public override void Start()
        {
            this.menuCursor = this.gameObject.transform.Find("Canvas").Find("MenuMouseCursor").gameObject.GetComponent<GameInput.GameCursorMenu>();
        }

        public override bool snapCompatible()
        {
            return false;
        }

        public override void setUpForSnapping()
        {
           
        }

        public override void exitMenu()
        {
            GameInformation.Game.Menu = null;
            base.exitMenu();
        }
        public override void Update()
        {
            checkForInput();
        }

        private void checkForInput()
        {
            if (GameInput.InputControls.APressed)
            {
                Dish d = new Dish("Cookie Ingredients");
                Game.Player.dishesInventory.Add(d);
                Game.Player.activeItem = d;
                exitMenu();
            }
            else if (GameInput.InputControls.BPressed)
            {
                Dish d = new Dish("Cake Ingredients");
                Game.Player.dishesInventory.Add(d);
                Game.Player.activeItem = d;
                exitMenu();
            }
            else if (GameInput.InputControls.XPressed)
            {
                Dish d = new Dish("Pie Ingredients");
                Game.Player.dishesInventory.Add(d);
                Game.Player.activeItem = d;
                exitMenu();
            }
            else if (GameInput.InputControls.YPressed)
            {
                Dish d = new Dish("Bread Ingredients");
                Game.Player.dishesInventory.Add(d);
                Game.Player.activeItem = d;
                exitMenu();
            }
            else if(GameInput.InputControls.LeftBumperPressed || GameInput.InputControls.StartPressed)
            {
                exitMenu();
            }
        }
    }
}
