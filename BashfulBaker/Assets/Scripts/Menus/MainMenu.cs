using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;
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
        Button startButton;
        [SerializeField]
        Button quitButton;
        [SerializeField]
        Button optionsButton;

        /// <summary>
        /// Instantiate all menu logic here.
        /// </summary>
        public override void Start()
        {
            GameObject canvas=this.transform.Find("Canvas").gameObject;
            startButton = canvas.transform.Find("StartButton").gameObject.GetComponent<Button>();
            quitButton = canvas.transform.Find("QuitButton").gameObject.GetComponent<Button>();
            optionsButton = canvas.transform.Find("OptionsButton").gameObject.GetComponent<Button>();

            menuCursor = canvas.transform.Find("MenuMouseCursor").GetComponent<GameCursorMenu>();
            Game.Menu = this;
        }


        /// <summary>
        /// Runs ~60 times a second.
        /// </summary>
        public override void Update()
        {
            if (menuCursor == null)
            {
                Debug.Log("Cursor is null");
            }

            if (GameCursorMenu.SimulateMousePress(startButton))
            {
                this.startButtonClick();
            }

            if (GameCursorMenu.SimulateMousePress(quitButton))
            {
                this.exitButtonClick();
            }

            if (GameCursorMenu.SimulateMousePress(optionsButton))
            {
                this.optionsButtonClick();
            }
        }
        /// <summary>
        /// Close the active menu.
        /// </summary>
        public override void exitMenu()
        {
            Destroy(this.gameObject);
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
            Debug.Log("ADD IN CREDITS!");
        }
    }
}
