using Assets.Scripts.Menus.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    public class ReturnToTitleConfirmationMenu:Menu
    {

        MenuComponent yes;
        MenuComponent no;

        public override void Start()
        {
            GameInformation.Game.Menu = this;
            GameObject canvas = this.gameObject.transform.Find("Canvas").gameObject;
            yes=new MenuComponent(canvas.transform.Find("YesButton").GetComponent<Button>());
            no =new MenuComponent(canvas.transform.Find("NoButton").GetComponent<Button>());

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
            GameInformation.Game.returnToMainMenu();
            
        }

        public void noButtonClick()
        {
            Menu.Instantiate<DaySelectMenu>(true);
        }

    }
}
