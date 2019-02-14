﻿using Assets.Scripts.Menus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus {

    public class GameMenu : Menu
    {
        MenuComponent save;
        MenuComponent load;
        MenuComponent toTitle;
        MenuComponent closeGame;
        MenuComponent closeMenu;

        // Start is called before the first frame update
        public override void Start()
        {
            GameObject canvas = this.gameObject.transform.Find("Canvas").gameObject;
            save =new MenuComponent(canvas.transform.Find("Save").GetComponent<Button>());
            load =new MenuComponent(canvas.transform.Find("Load").GetComponent<Button>());
            toTitle =new MenuComponent(canvas.transform.Find("ExitToTitle").GetComponent<Button>());
            closeGame =new MenuComponent(canvas.transform.Find("ExitGame").GetComponent<Button>());
            closeMenu = new MenuComponent(canvas.transform.Find("CloseMenu").GetComponent<Button>());

            this.menuCursor = canvas.transform.Find("MenuMouseCursor").gameObject.GetComponent<GameInput.GameCursorMenu>();
            setUpForSnapping();
        }

        public override void setUpForSnapping()
        {
            closeMenu.setNeighbors(null, null, null, save);
            save.setNeighbors(null, null, closeMenu, load);
            load.setNeighbors(null, null, save, toTitle);
            toTitle.setNeighbors(null, null, load, closeGame);
            closeGame.setNeighbors(null, null, toTitle, null);

            selectedComponent = closeMenu;
            this.menuCursor.snapToCurrentComponent();
        }

        public override bool snapCompatible()
        {
            return true;
        }

        // Update is called once per frame
        public override void Update()
        {
            if (GameInput.GameCursorMenu.SimulateMousePress(save))
            {
                openSaveMenu();
                return;
            }
            else if (GameInput.GameCursorMenu.SimulateMousePress(load))
            {
                openLoadMenu();
                return;
            }
            else if (GameInput.GameCursorMenu.SimulateMousePress(toTitle))
            {
                exitToTitle();
                return;
            }
            else if (GameInput.GameCursorMenu.SimulateMousePress(closeGame))
            {
                exitGame();
                return;
            }
            else if (GameInput.GameCursorMenu.SimulateMousePress(closeMenu))
            {
                exitMenu();
                return;
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
