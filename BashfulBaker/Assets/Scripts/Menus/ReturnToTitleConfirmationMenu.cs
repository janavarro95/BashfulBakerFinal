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

        Button yes;
        Button no;

        public override void Start()
        {
            GameObject canvas = this.gameObject.transform.Find("Canvas").gameObject;
            yes= canvas.transform.Find("YesButton").GetComponent<Button>();
            no = canvas.transform.Find("NoButton").GetComponent<Button>();

            this.menuCursor = canvas.transform.Find("MenuMouseCursor").gameObject.GetComponent<GameInput.GameCursorMenu>();
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
            Menu.Instantiate<GameMenu>(true);
        }

    }
}
