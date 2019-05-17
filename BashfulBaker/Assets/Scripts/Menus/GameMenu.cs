using Assets.Scripts.Menus.Components;
using Assets.Scripts.Menus.MenuTypes;
using Assets.Scripts.Utilities.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{

    public class GameMenu : Menu
    {
        MenuComponent save;
        MenuComponent load;
        MenuComponent toTitle;
        MenuComponent closeGame;
        MenuComponent closeMenu;
        MenuComponent resume;

        GameObject canvas;
        Canvas actualCanvas;

        DeltaTimer breathIn;

        MenuBreath breath;
        SlideIn slide;

        public void Awake()
        {
            canvas = this.gameObject.transform.Find("Canvas").gameObject;
            actualCanvas = canvas.gameObject.GetComponent<Canvas>();
            //actualCanvas.scaleFactor = 0f;

            //breath = new MenuBreath(actualCanvas, 1, finishIntro, actualyExit);
        }

        // Start is called before the first frame update
        public override void Start()
        {
            //slide = new SlideIn(canvas, 4f, finishIntro);
            GameInformation.Game.Menu = this;
            //breath.breathIn();
            //actualCanvas.scaleFactor = 0f;
            finishIntro();
        }

        public void finishIntro()
        {


            resume = new MenuComponent(canvas.transform.Find("Resume").GetComponent<Button>());

            save = new MenuComponent(canvas.transform.Find("Save").GetComponent<Button>());
            load = new MenuComponent(canvas.transform.Find("Load").GetComponent<Button>());
            toTitle = new MenuComponent(canvas.transform.Find("ExitToTitle").GetComponent<Button>());
            closeGame = new MenuComponent(canvas.transform.Find("ExitGame").GetComponent<Button>());
            closeMenu = new MenuComponent(canvas.transform.Find("CloseMenu").GetComponent<Button>());

            this.menuCursor = canvas.transform.Find("MenuMouseCursor").gameObject.GetComponent<GameInput.GameCursorMenu>();
            setUpForSnapping();
        }

        /// <summary>
        /// Sets up all of the snapping for the menus.
        /// </summary>
        public override void setUpForSnapping()
        {
            resume.setNeighbors(null, null, closeMenu, save);
            closeMenu.setNeighbors(null, null, null, resume);
            save.setNeighbors(null, null, resume, load);
            load.setNeighbors(null, null, save, toTitle);
            toTitle.setNeighbors(null, null, load, closeGame);
            closeGame.setNeighbors(null, null, toTitle, null);

            selectedComponent = resume;
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
            else if (GameInput.GameCursorMenu.SimulateMousePress(resume))
            {
                exitMenu();
                return;
            }

            if (GameInput.InputControls.BPressed)
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
            //Debug.Log("Saving not implemented...yet");
        }

        public void openLoadMenu()
        {
            //Debug.Log("Loading not implemented...yet");
        }

        public override void exitMenu()
        {
            //breath.breathOut();
            actualyExit();
        }

        public void actualyExit()
        {
            base.exitMenu();
            GameInformation.Game.Menu = null;
        }
    }
}
