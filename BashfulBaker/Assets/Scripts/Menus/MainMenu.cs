using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;
using Assets.Scripts.Menus.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    /// <summary>
    /// Deals with the main menu.
    /// </summary>
    public class MainMenu:Menu
    {
        [SerializeField]
        MenuComponent startButton;
        [SerializeField]
        MenuComponent quitButton;
        [SerializeField]
        MenuComponent optionsButton;

        GameObject canvas;

        /// <summary>
        /// Instantiate all menu logic here.
        /// </summary>
        public override void Start()
        {
            canvas=this.transform.Find("Canvas").gameObject;
            startButton = new MenuComponent(canvas.transform.Find("StartButton").gameObject.GetComponent<Button>());
            quitButton = new MenuComponent(canvas.transform.Find("QuitButton").gameObject.GetComponent<Button>());
            optionsButton =new MenuComponent(canvas.transform.Find("OptionsButton").gameObject.GetComponent<Button>());


            menuCursor = canvas.transform.Find("MenuMouseCursor").GetComponent<GameCursorMenu>();
            Game.Menu = this;

            //canvas.transform.Find("Image").GetComponent<Image>().rectTransform.sizeDelta = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);

            setUpForSnapping();

        }       

        public override void setUpForSnapping()
        {
            startButton.setNeighbors(null, optionsButton, null, null);
            quitButton.setNeighbors(optionsButton, null, null, null);
            optionsButton.setNeighbors(startButton, quitButton, null, null);
            this.selectedComponent = startButton;
            menuCursor.snapToCurrentComponent();

        }

        public override bool snapCompatible()
        {
            return true;
        }

        /// <summary>
        /// Runs ~60 times a second.
        /// </summary>
        public override void Update()
        {
            if (menuCursor == null)
            {
                //Debug.Log("Cursor is null");
            }

            if (GameCursorMenu.SimulateMousePress(startButton))
            {
                this.startButtonClick();
                Game.SoundEffects.playMainMenuStartButton();
                return;
            }

            if (GameCursorMenu.SimulateMousePress(quitButton))
            {
                this.exitButtonClick();
                return;
            }

            if (GameCursorMenu.SimulateMousePress(optionsButton))
            {
                this.optionsButtonClick();
                return;
            }
        }
        /// <summary>
        /// Close the active menu.
        /// </summary>
        public override void exitMenu()
        {
            base.exitMenu();
            Game.Menu = null;
        }   

        /// <summary>
        /// What happens when the start button is clicked.
        /// </summary>
        public void startButtonClick()
        {
            SceneManager.LoadScene("preloadScene");
        }

        /// <summary>
        /// What happens when the quit button is clicked.
        /// </summary>
        public void exitButtonClick()
        {
            Application.Quit();
        }

        /// <summary>
        /// What happens when the option button is clicked.
        /// </summary>
        public void optionsButtonClick()
        {
            Menu.Instantiate<OptionsMenu>(true);
            //Destroy(this.gameObject); //necessary to remove the main menu from the screen.
        }

        public void creditsButtonClick()
        {
            //Debug.Log("ADD IN CREDITS!");
        }

        public void openSaveLoadSelectMenu()
        {
            //Debug.Log("ADD IN Save/Load system!");
        }
    }
}
