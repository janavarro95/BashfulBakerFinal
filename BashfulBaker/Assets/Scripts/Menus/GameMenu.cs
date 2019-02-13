using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus {

    public class GameMenu : Menu
    {
        Button save;
        Button load;
        Button toTitle;
        Button closeGame;
        Button closeMenu;

        // Start is called before the first frame update
        public override void Start()
        {
            GameObject canvas = this.gameObject.transform.Find("Canvas").gameObject;
            save = canvas.transform.Find("Save").GetComponent<Button>();
            load = canvas.transform.Find("Load").GetComponent<Button>();
            toTitle = canvas.transform.Find("ExitToTitle").GetComponent<Button>();
            closeGame = canvas.transform.Find("ExitGame").GetComponent<Button>();
            closeMenu = canvas.transform.Find("CloseMenu").GetComponent<Button>();

            this.menuCursor = canvas.transform.Find("MenuMouseCursor").gameObject.GetComponent<GameInput.GameCursorMenu>();
        }

        // Update is called once per frame
        public override void Update()
        {
            if (GameInput.GameCursorMenu.SimulateMousePress(save))
            {
                openSaveMenu();
            }
            else if (GameInput.GameCursorMenu.SimulateMousePress(load))
            {
                openLoadMenu();
            }
            else if (GameInput.GameCursorMenu.SimulateMousePress(toTitle))
            {
                exitToTitle();
            }
            else if (GameInput.GameCursorMenu.SimulateMousePress(closeGame))
            {
                exitGame();
            }
            else if (GameInput.GameCursorMenu.SimulateMousePress(closeMenu))
            {
                exitMenu();
            }
        }

        public void exitToTitle()
        {
            Menu.Instantiate<ReturnToTitleConfirmationMenu>(true);
        }

        public void exitGame()
        {
            GameInformation.Game.QuitGame();
        }

        public void openSaveMenu()
        {
            Debug.Log("Saving not implemented...yet");
        }

        public void openLoadMenu()
        {
            Debug.Log("Loading not implemented...yet");
        }

        public override void exitMenu()
        {
            base.exitMenu();
            GameInformation.Game.Menu = null;
        }
    }
}
