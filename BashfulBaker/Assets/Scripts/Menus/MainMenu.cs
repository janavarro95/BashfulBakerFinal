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

namespace Assets.Scripts.Menus
{
    /// <summary>
    /// Deals with the main menu.
    /// </summary>
    public class MainMenu:Menu
    {

        /// <summary>
        /// Instantiate all menu logic here.
        /// </summary>
        public override void Start()
        {
            
        }


        /// <summary>
        /// Runs ~60 times a second.
        /// </summary>
        public override void Update()
        {
            if (GameCursor.SimulateMousePress(this))
            {
                this.startButtonClick();
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
    }
}
