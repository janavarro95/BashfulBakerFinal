using Assets.Scripts.Menus.Components;
using Assets.Scripts.Utilities.Delegates;
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

        private VoidDelegate altNoSelect;

        public override void Start()
        {
            GameInformation.Game.Menu = this;
            GameObject canvas = this.gameObject.transform.Find("Canvas").gameObject;
            yes=new MenuComponent(canvas.transform.Find("YesButton").GetComponent<Button>());
            no =new MenuComponent(canvas.transform.Find("NoButton").GetComponent<Button>());

            this.menuCursor = canvas.transform.Find("MenuMouseCursor").gameObject.GetComponent<GameInput.GameCursorMenu>();
            setUpForSnapping();
        }

        public void setNoFunctionality(VoidDelegate del)
        {
            this.altNoSelect = del;
        }

        public override void setUpForSnapping()
        {
            yes.setNeighbors(null, no, null, null);
            no.setNeighbors(yes, null, null, null);

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

        /// <summary>
        /// What happens when the no button is clicked.
        /// </summary>
        public void noButtonClick()
        {
            if (altNoSelect == null)
            {
                Menu.Instantiate<GameMenu>(true);
            }
            else
            {
                this.altNoSelect.Invoke();
            }
        }

    }
}
