using Assets.Scripts.Menus.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    public class ReturnToDailySelectMenu:Menu
    {

        MenuComponent yes;
        MenuComponent no;

        public override void Start()
        {
            GameObject canvas = this.gameObject.transform.Find("Canvas").gameObject;
            yes = new MenuComponent(canvas.transform.Find("YesButton").GetComponent<Button>());
            no = new MenuComponent(canvas.transform.Find("NoButton").GetComponent<Button>());

            this.menuCursor = canvas.transform.Find("MenuMouseCursor").gameObject.GetComponent<GameInput.GameCursorMenu>();
            setUpForSnapping();
        }

        public override void setUpForSnapping()
        {
            yes.setNeighbors(null, null, null, no);
            no.setNeighbors(null, null, yes, null);

            selectedComponent = no;
            menuCursor.snapToCurrentComponent();
        }

        public override bool snapCompatible()
        {
            return true;
        }

        public override void Update()
        {
            if (GameInput.GameCursorMenu.SimulateMousePress(yes))
            {
                yesButtonClick();
            }
            else if (GameInput.GameCursorMenu.SimulateMousePress(no))
            {
                noButtonClick();
            }
        }

        public void yesButtonClick()
        {
            Debug.Log("Current Day is: " + GameInformation.Game.CurrentDayNumber);
            GameInformation.Game.TutorialCompleted = true;

            GameInformation.Game.returnToDailySelectMenu();
        }

        public void noButtonClick()
        {
            Menu.Instantiate<EndofDayMenu>(true);
        }
    }
}
